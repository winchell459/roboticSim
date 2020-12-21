using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : MonoBehaviour
{
    public Rigidbody rb;

    public LayerMask Mask;
    public float RayDistance = 1f;
    public bool ClawActive;
    public bool LiftEngaged;

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
        if (!ClawActive && Physics.Raycast(ray, out hit, RayDistance, Mask))
        {
            Transform riser = getParent(hit.collider.transform);
            //Debug.Log(transform.name + " hit " + riser.name);

            Vector3 displacement = riser.position - transform.position;
            if (!ClawActive && Vector3.Dot(displacement, rb.velocity) < -0.1f) //less than zero means release the riser
            {
                riser.GetComponent<Riser>().Drop();
            }
            else if (riser.parent != transform) //pick up Riser
            {

                riser.GetComponent<Riser>().Pickup(transform);
            }

            
        }
    }

    private Transform getParent(Transform riser)
    {

        if (riser.parent != null && riser.parent != transform) return getParent(riser.parent);
        else return riser;
    }
}
