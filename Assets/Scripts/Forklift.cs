using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : MonoBehaviour
{
    public Rigidbody rb;

    public LayerMask Mask;
    public float RayDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkFork();
    }

    private void checkFork()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * RayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RayDistance, Mask))
        {
            Transform riser = getParent(hit.collider.transform);
            Debug.Log(transform.name + " hit " + riser.name);

            Vector3 displacement = riser.position - transform.position;
            if (Vector3.Dot(displacement, rb.velocity) < 0) //less than zero means release the riser
            {
                riser.parent = null;
                riser.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (riser.parent != transform)
            {

                Rigidbody rrb = riser.GetComponent<Rigidbody>();
                //rrb.velocity = Vector3.zero;
                //rrb.angularVelocity = Vector3.zero;
                riser.parent = transform;
                rrb.isKinematic = true;
            }

            //if (Vector3.Distance(pos, transform.position) > 0 && rb.velocity == Vector3.zero)
            //{
            //    riser.GetComponent<Rigidbody>().isKinematic = false;
            //}
        }
    }

    private Transform getParent(Transform riser)
    {

        if (riser.parent != null && riser.parent != transform) return getParent(riser.parent);
        else return riser;
    }
}
