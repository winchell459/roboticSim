using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRobotController : MonoBehaviour
{
    public float Speed = 3f;
    public float Torque = 5.0f; //float - > real numbers
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Left") + "  " + Input.GetAxis("Right"));
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

        //if(left > right) 

    }
}
