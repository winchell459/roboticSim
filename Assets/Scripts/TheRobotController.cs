using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class TheRobotController : MonoBehaviour
{
    public float Speed = 3f;
    public float Torque = 5.0f; //float - > real numbers
    private Rigidbody rb;

    private List<Riser> risers = new List<Riser>();

    public Forklift UpperForklift, LowerForklift, BottomForklift;
    public float LiftSpeed = 1;
    public float LiftDistance = 1;
    private float liftDefaultHeight, liftSpacing;

    private bool clawActive = false;

    bool matchStarted;

    public bool TouchControls;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        liftDefaultHeight = LowerForklift.transform.localPosition.y;
        liftSpacing = UpperForklift.transform.position.y - LowerForklift.transform.position.y;
        BottomForklift.gameObject.SetActive(false);

        //pos = transfsorm.position;
    }

    //Vector3 pos;
    // Update is called once per frame
    void Update()
    {
        if (matchStarted)
        {
            handleLift();
            handleClaw();
            handleMovement();
        }
        

    }
    public void MatchStarted()
    {
        matchStarted = true;
    }
    void handleMovement()
    {
        float moving = Input.GetAxis("Vertical");
        float turning = Input.GetAxis("Horizontal");

        if (TouchControls)
        {
            Vector2 move = TCKInput.GetAxis("Joystick");
            moving = move.y;
            turning = move.x;
        }
        

        turning += 1;
        turning /= 2;

        float left = turning;
        float right = (1 - turning);

        float rotation = left - right;
        float velocity = moving * Speed;

        rb.velocity = velocity * transform.forward;

        transform.Rotate(rotation * transform.up * Torque * Time.deltaTime);
        //Debug.Log(rotation * transform.up * Torque);
        //rb.angularVelocity = rotation * transform.up * Torque;
        //Debug.Log(rb.angularVelocity + " " + rotation * transform.up * Torque);
    }

    void handleClaw()
    {
        bool clawRelease = Input.GetKeyDown(KeyCode.LeftShift) || TCKInput.GetAction("clawDisengageBtn", EActionEvent.Down);
        bool clawEngage = Input.GetKeyDown(KeyCode.Space) || TCKInput.GetAction("clawEngageBtn", EActionEvent.Down);

        //if both claw buttons are pressed, have no change to clawActive
        if (clawEngage != clawRelease)
        {
            if (clawEngage) clawActive = true;
            else clawActive = false;

            UpperForklift.ClawActive = clawActive;
            LowerForklift.ClawActive = clawActive;
            Debug.Log("clawActive: " + (clawActive ? "engaged" : "disengaged"));
        }
    }

    private void handleLift()
    {
        float lift = Input.GetAxis("Right");
        if (TouchControls)
        {
            lift = 0;
            lift += TCKInput.GetAction("liftUpBtn", EActionEvent.Press) ? 1 : 0;
            lift += TCKInput.GetAction("liftDownBtn", EActionEvent.Press) ? -1 : 0;
        }

        if (BottomForklift.transform.GetComponentInChildren<Riser>()) lift = Mathf.Clamp(lift, 0, 1); //if bottom forklift has a rise, do not move down
        float liftPos = lift * Time.deltaTime * LiftSpeed + LowerForklift.transform.localPosition.y; //lift displacement plus Lowerlift local y position
        liftPos = Mathf.Clamp(liftPos, liftDefaultHeight, liftDefaultHeight + LiftDistance); //clamp lower lift between default height and max height

        //set lower and upper forklifts
        Vector3 lowerLiftPos = LowerForklift.transform.localPosition;
        LowerForklift.transform.localPosition = new Vector3(lowerLiftPos.x, liftPos, lowerLiftPos.z);
        UpperForklift.transform.localPosition = LowerForklift.transform.localPosition + new Vector3(0, liftSpacing, 0);

        if(liftPos >= liftDefaultHeight + LiftDistance)
        {
            BottomForklift.gameObject.SetActive(true);
        }
        else
        {
            BottomForklift.gameObject.SetActive(false);
        }
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
