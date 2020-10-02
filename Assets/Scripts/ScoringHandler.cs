using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringHandler : MonoBehaviour
{
    public ScoringZone G0_0, G1_0, G2_0;
    public ScoringZone G0_1, G1_1, G2_1;
    public ScoringZone G0_2, G1_2, G2_2;
    

    public int ScoreBoard()
    {
        int score = 0;
        //check rows
        if (validSequence(G0_0.RiserStack[0], G1_0.RiserStack[0], G2_0.RiserStack[0])) score += 3;
        if (validSequence(G0_1.RiserStack[0], G1_1.RiserStack[0], G2_1.RiserStack[0])) score += 3;
        if (validSequence(G0_2.RiserStack[0], G1_2.RiserStack[0], G2_2.RiserStack[0])) score += 3;
        //check cols
        if (validSequence(G0_0.RiserStack[0], G0_1.RiserStack[0], G0_2.RiserStack[0])) score += 3;
        if (validSequence(G1_0.RiserStack[0], G1_1.RiserStack[0], G1_2.RiserStack[0])) score += 3;
        if (validSequence(G2_0.RiserStack[0], G2_1.RiserStack[0], G2_2.RiserStack[0])) score += 3;
        //check diagonals
        if (validSequence(G0_0.RiserStack[0], G1_1.RiserStack[0], G2_2.RiserStack[0])) score += 3;
        if (validSequence(G2_0.RiserStack[0], G1_1.RiserStack[0], G0_2.RiserStack[0])) score += 3;


        return score;
    }

    private bool validSequence(Riser riser1, Riser riser2, Riser riser3)
    {
        if(validPlacement(riser1) && validPlacement(riser2) && validPlacement(riser3))
        {
            if(riser1.RiserColor == riser2.RiserColor && riser2.RiserColor == riser3.RiserColor)
            {
                return true;
            }
            
        }

        return false;
    }

    private bool validPlacement(Riser riser)
    {
        if (!riser) return false;
        float test = Vector3.Dot(riser.transform.up, Vector3.up);
        if (test > 0.2f || test < -0.2f) return true;
        else return false;
    }

}
