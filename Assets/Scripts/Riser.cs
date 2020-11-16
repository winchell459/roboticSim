using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{
    public Material m_isBaseMaterial;
    private Material m_default;

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
        m_default = transform.GetChild(0).GetComponent<Renderer>().material;
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

    public void SetIsInScoringZone(bool isInScoringZone)
    {
        if (isInScoringZone)
        {
            foreach(Renderer child in GetComponentsInChildren<Renderer>())
                child.material = m_isBaseMaterial;
        }
        else
        {
            foreach (Renderer child in GetComponentsInChildren<Renderer>())
                child.material = m_default;
        }
    }

}
