using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    //index 0 - bottom 
    //public Riser[] RiserStack = new Riser[3];
    public List<Riser> RiserStack = new List<Riser>(3);

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
        {
            
            Riser riser = other.transform.parent.GetComponent<Riser>();
            if (!RiserStack.Contains(riser)) addRiser(riser);
        }
    }

    private void addRiser(Riser riser)
    {
        for(int i = 0; i < 3; i += 1)
        {
            if (RiserStack[i])
            {
                if(RiserStack[i].transform.position.y > riser.transform.position.y)
                {
                    Riser temp = RiserStack[i];
                    RiserStack[i] = riser;
                    riser = temp;
                }
            }
            else
            {
                Debug.Log("Empty Slot");
                RiserStack[i] = riser;
                riser = null;
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.GetComponent<Riser>())
        {
            Riser riser = other.transform.parent.GetComponent<Riser>();
            if(RiserStack.Contains(riser)) removeRiser(riser);
        }
    }

    private void removeRiser(Riser riser)
    {
        for(int i = 0; i < 3; i += 1)
        {
            if (riser)
            {
                if(RiserStack[i] == riser)
                {
                    RiserStack[i] = null;
                    riser = null;
                }
            }
            else
            {
                RiserStack[i - 1] = RiserStack[i];
            }
        }
    }

}
