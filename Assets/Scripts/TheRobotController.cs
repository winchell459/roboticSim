using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRobotController : MonoBehaviour
{
    public float Speed = 3f;
    public float Torque = 5.0f; //float - > real numbers
    private Rigidbody rb;

    public LayerMask Mask;
    public float RayDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Left") + "  " + Input.GetAxis("Right"));
        float moving = Input.GetAxis("Left");
        float turning = Input.GetAxis("Right"); // right = -1 full power on right wheels  -1 = 100% right 0 = 50% right and 1 = 0% right

        turning += 1;
        turning /= 2;

        float left = turning;
        float right = (1 - turning);

        float rotation = left - right;
        float velocity = moving * Speed;

        rb.velocity = velocity * transform.forward;
        transform.Rotate(rotation * transform.up * Torque);

        checkFork();

    }

    private void checkFork()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * RayDistance, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, RayDistance))
        {
            Vector3 displacement = hit.collider.transform.position - transform.position;
            if(Vector3.Dot(displacement, rb.velocity) < 0) //less than zero means release the riser
            {
                hit.collider.transform.parent = null;
                hit.collider.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if(hit.collider.transform.parent != transform)
            {
                hit.collider.transform.parent = transform;
                hit.collider.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    
}
