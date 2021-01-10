using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class CameraController : MonoBehaviour
{
    public float MaxPan = 5;
    public float PanSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 look = TCKInput.GetAxis("Touchpad");
        float x = Mathf.Clamp(transform.position.x + look.x * PanSpeed * Time.deltaTime, -MaxPan, MaxPan);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
