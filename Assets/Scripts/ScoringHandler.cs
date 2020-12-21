using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringHandler : MonoBehaviour
{
    public ScoringZone G0_0, G1_0, G2_0;
    public ScoringZone G0_1, G1_1, G2_1;
    public ScoringZone G0_2, G1_2, G2_2;

    private bool[,] inCompletedRow = new bool[,] { { false, false, false }, { false, false, false }, { false, false, false } };



    public ScoreCard Card;

    int numOfRisers, numOfRiserStacks;
    public int ScoreBoard()
    {
        int score = 0;
        numOfRisers = numOfRiserStacks = 0;

        //check rows
        if (validSequence(G0_0.RiserStack[0], G1_0.RiserStack[0], G2_0.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[0, 0] = true;
            inCompletedRow[1, 0] = true;
            inCompletedRow[2, 0] = true;
        }
        if (validSequence(G0_1.RiserStack[0], G1_1.RiserStack[0], G2_1.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[0, 1] = true;
            inCompletedRow[1, 1] = true;
            inCompletedRow[2, 1] = true;
        }
        if (validSequence(G0_2.RiserStack[0], G1_2.RiserStack[0], G2_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[0, 2] = true;
            inCompletedRow[1, 2] = true;
            inCompletedRow[2, 2] = true;
        }
        //check cols
        if (validSequence(G0_0.RiserStack[0], G0_1.RiserStack[0], G0_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[0, 0] = true;
            inCompletedRow[0, 1] = true;
            inCompletedRow[0, 2] = true;
        }
        if (validSequence(G1_0.RiserStack[0], G1_1.RiserStack[0], G1_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[1, 0] = true;
            inCompletedRow[1, 1] = true;
            inCompletedRow[1, 2] = true;
        }
        if (validSequence(G2_0.RiserStack[0], G2_1.RiserStack[0], G2_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[2, 0] = true;
            inCompletedRow[2, 1] = true;
            inCompletedRow[2, 1] = true;
        }
        //check diagonals
        if (validSequence(G0_0.RiserStack[0], G1_1.RiserStack[0], G2_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[0, 0] = true;
            inCompletedRow[1, 1] = true;
            inCompletedRow[2, 2] = true;
        }
        if (validSequence(G2_0.RiserStack[0], G1_1.RiserStack[0], G0_2.RiserStack[0]))
        {
            score += 3;
            inCompletedRow[2, 0] = true;
            inCompletedRow[1, 1] = true;
            inCompletedRow[0, 2] = true;
        }

        score += checkStack(G0_0.RiserStack, inCompletedRow[0, 0]);
        score += checkStack(G1_0.RiserStack, inCompletedRow[1, 0]);
        score += checkStack(G2_0.RiserStack, inCompletedRow[2, 0]);
        score += checkStack(G0_1.RiserStack, inCompletedRow[0, 1]);
        score += checkStack(G1_1.RiserStack, inCompletedRow[1, 1]);
        score += checkStack(G2_1.RiserStack, inCompletedRow[2, 1]);
        score += checkStack(G0_2.RiserStack, inCompletedRow[0, 2]);
        score += checkStack(G1_2.RiserStack, inCompletedRow[1, 2]);
        score += checkStack(G2_2.RiserStack, inCompletedRow[2, 2]);

        Riser.RiserColors[] selections = new Riser.RiserColors[9];
        selections[0] = validPlacement(G0_0.RiserStack[0]) ? G0_0.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[1] = validPlacement(G0_1.RiserStack[0]) ? G0_1.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[2] = validPlacement(G0_2.RiserStack[0]) ? G0_2.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[3] = validPlacement(G1_0.RiserStack[0]) ? G1_0.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[4] = validPlacement(G1_1.RiserStack[0]) ? G1_1.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[5] = validPlacement(G1_2.RiserStack[0]) ? G1_2.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[6] = validPlacement(G2_0.RiserStack[0]) ? G2_0.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[7] = validPlacement(G2_1.RiserStack[0]) ? G2_1.RiserStack[0].RiserColor : Riser.RiserColors.None;
        selections[8] = validPlacement(G2_2.RiserStack[0]) ? G2_2.RiserStack[0].RiserColor : Riser.RiserColors.None;

        foreach(Riser.RiserColors color in selections)
        {
            Debug.Log(color.ToString());
        }
        Card.gameObject.SetActive(true);
        Card.SelectRisers(selections);
        Card.SetNumOfRiser(numOfRisers);
        Card.SetNumOfRiserStacks(numOfRiserStacks);

        HighScoreHandler.SetHighScore(score);
        FindObjectOfType<HighScoreHandler>().DisplayHighScore();
        return score;
    }

    private bool validSequence(Riser riser1, Riser riser2, Riser riser3)
    {
        if(validPlacement(riser1) && validPlacement(riser2) && validPlacement(riser3))
        {
            if(riser1.RiserColor == riser2.RiserColor && riser2.RiserColor == riser3.RiserColor)
            {
                Debug.Log("+3 squence: " + riser1.name + ":" + riser2.name + ":" + riser3.name);

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

    private int checkStack(List<Riser> stack, bool hasCompletedRow)
    {
        int score = 0;
        if (validPlacement(stack[0]))
        {
            Debug.Log("+1 single stack");
            score += 1;  //one point for having a riser
            numOfRisers += 1;
            if(validPlacement(stack[1]))
            {
                Debug.Log("+1 double stack");
                score += 1; // one point for having a stacked riser
                numOfRisers += 1;
                if (validPlacement(stack[2]))
                {
                    Debug.Log("+1 triple stack");
                    score += 1; // another point for the top riser
                    numOfRisers += 1;
                    if (hasCompletedRow && stack[0].RiserColor == stack[1].RiserColor && stack[2].RiserColor == stack[1].RiserColor)
                    {
                        score += 30; //30 points for having a stack of 3 of the same color
                        numOfRiserStacks += 1;
                        Debug.Log("+30 " + stack[0].RiserColor + " stack.");
                    }
                }
            }
        }
        
        return score;
    }

}
