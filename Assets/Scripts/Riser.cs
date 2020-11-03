using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{
    private float mass = 1;
    public Vector2 Size { get { return getSize(); } }
    public RiserColors RiserColor;
    public enum RiserColors
    {
        Orange,
        Teal,
        Purple,
        None
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

    private Vector2 getSize()
    {
        //one of the cubes must be at the top of the hierarchy inside the Riser object
        float w = transform.localScale.x * transform.GetChild(0).localScale.x;
        float h = transform.localScale.z * transform.GetChild(0).localScale.z;
        return new Vector2(w, h);
    }
}
