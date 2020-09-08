using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRobotController : MonoBehaviour
{
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
        float left = Input.GetAxis("Left");
        float right = Input.GetAxis("Right");

        float rotation = left - right;

        //if(left > right) 

    }
}
