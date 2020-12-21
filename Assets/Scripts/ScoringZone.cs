using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    //index 0 - bottom 
    //public Riser[] RiserStack = new Riser[3];

    public List<Riser> RiserStack {  get { return getRiserStack(); } }
    public List<Riser> riserStack;
    private bool riserStackInitialized = false;
    private Vector2 size {  get { return new Vector2(transform.localScale.x, transform.localScale.z); } }
    private float buffer = 0.5f;

    /* private void OnTriggerEnter(Collider other)
      {
          if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
          {

              Riser riser = other.transform.parent.GetComponent<Riser>();
              if (!RiserStack.Contains(riser)) addRiser(riser);
          }
      }
    */

    private List<Riser> getRiserStack()
    {    
        if (riserStackInitialized)
        {
            return riserStack;
        }
        else
        {
            riserStackInitialized = true;
            Debug.Log("Creating riserStack");
            riserStack = new List<Riser>();
            riserStack.Add(null);
            riserStack.Add(null);
            riserStack.Add(null);

            Riser[] allRisers = FindObjectsOfType<Riser>();
            foreach(Riser riser in allRisers)
            {
                if (checkRiserInBounds(riser, buffer))
                {
                    Debug.Log(riser.name + "in Zone");
                    addRiser(riser);
                }
            }
            if(riserStack[0] && !checkRiserInBounds(riserStack[0], 0))
            {
                riserStack[0] = null;
                riserStack[1] = null;
                riserStack[2] = null;
            }
        }
        Debug.Log(transform.name + " RiserStack: " + riserStack[0] + ", " + riserStack[1] + ", " + riserStack[2]);
        return riserStack;
    }
    private bool checkRiserInBounds(Riser riser, float buffer)
    {
        float riserRadius = Mathf.Sqrt(Mathf.Pow(riser.Size.x / 2, 2) + Mathf.Pow(riser.Size.y / 2, 2));
        float zoneRadius = buffer + size.x / 2;
        float distance = Mathf.Sqrt(Mathf.Pow(riser.transform.position.x - transform.position.x, 2) + Mathf.Pow(riser.transform.position.z - transform.position.z, 2));
        if((riserRadius + distance) * 0.9f < zoneRadius) Debug.Log(riser.name + " : " + riserRadius + " + " + distance + " <= " + zoneRadius);
        if (riserRadius + distance <= zoneRadius) return true;
        else return false;
    }
    private void addRiser(Riser riser)
    {
        for(int i = 0; i < 3; i += 1)
        {
            if (riserStack[i])
            {
                if(riserStack[i].transform.position.y > riser.transform.position.y)
                {
                    Riser temp = riserStack[i];
                    riserStack[i] = riser;
                    riser = temp;
                }
            }
            else
            {
                Debug.Log("Empty Slot");
                riserStack[i] = riser;
                riser = null;
                break;
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
        {
            Riser riser = other.transform.parent.GetComponent<Riser>();
            if(RiserStack.Contains(riser)) removeRiser(riser);
        }
    }
    */
    private void removeRiser(Riser riser)
    {
        for(int i = 0; i < 3; i += 1)
        {
            if (riser)
            {
                if(riserStack[i] == riser)
                {
                    riserStack[i] = null;
                    riser = null;
                }
            }
            else
            {
                riserStack[i - 1] = riserStack[i];
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
        {
            if (checkRiserInBounds(other.transform.parent.GetComponent<Riser>(), 0))
              other.transform.parent.GetComponent<Riser>().SetIsInScoringZone(true);
            else
                other.transform.parent.GetComponent<Riser>().SetIsInScoringZone(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
        {
            other.transform.parent.GetComponent<Riser>().SetIsInScoringZone(false);
        }
    }
}
