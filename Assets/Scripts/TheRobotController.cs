using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRobotController : MonoBehaviour
{
    public float Speed = 3f;
    public float Torque = 5.0f; //float - > real numbers
    private Rigidbody rb;

    private List<Riser> risers = new List<Riser>();

    public Riser UpperRiser, LowerRiser, BottomRiser;
    public float LiftSpeed = 1;
    public float LiftDistance = 1;
    private float liftDefaultHeight;
    //public LayerMask Mask;
    //public float RayDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        liftDefaultHeight = LowerRiser.transform.position.y;
        BottomRiser.gameObject.SetActive(false);
        //pos = transform.position;
    }

    //Vector3 pos;
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

        
        Debug.Log(rb.velocity);

        //pos = transform.position;
    }

    public bool AddRiser(Riser riser)
    {
        if (risers.Contains(riser)) return false;
        else risers.Add(riser);
        return true;
    }
    
    public bool RemoveRiser(Riser riser)
    {
        if (!risers.Contains(riser)) return false;
        else risers.Remove(riser);
        return true;
    }
   
    public bool HasRiser(Riser riser)
    {
        return risers.Contains(riser);
    }

}
