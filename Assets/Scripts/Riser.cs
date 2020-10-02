using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{
    private float mass = 1;
    public RiserColors RiserColor;
    public enum RiserColors
    {
        Orange,
        Teal,
        Purple
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>() != null) mass = GetComponent<Rigidbody>().mass;
    }

    public void Pickup(Transform forklift)
    {
        transform.parent = forklift;
        if (GetComponent<Rigidbody>()) Destroy(GetComponent<Rigidbody>());
    }

    public void Drop()
    {
        transform.parent = null;
        if (!GetComponent<Rigidbody>()) gameObject.AddComponent<Rigidbody>().mass = mass;
    }
}
