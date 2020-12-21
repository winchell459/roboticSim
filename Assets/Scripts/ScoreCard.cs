using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
    public PanelSelection Panel0_0, Panel0_1, Panel0_2, Panel1_0, Panel1_1, Panel1_2, Panel2_0, Panel2_1, Panel2_2;
    public Text NumOfRiser, NumOfRiserStacks;

    public void SelectRisers(Riser.RiserColors[] selection)
    {
        SelectRiser(Panel0_0, selection[0]);
        SelectRiser(Panel0_1, selection[1]);
        SelectRiser(Panel0_2, selection[2]);
        SelectRiser(Panel1_0, selection[3]);
        SelectRiser(Panel1_1, selection[4]);
        SelectRiser(Panel1_2, selection[5]);
        SelectRiser(Panel2_0, selection[6]);
        SelectRiser(Panel2_1, selection[7]);
        SelectRiser(Panel2_2, selection[8]);
    }    

    void SelectRiser(PanelSelection panel, Riser.RiserColors selection)
    {
        panel.SetSelection(selection);

    }

    public void SetNumOfRiser(int count)
    {
        NumOfRiser.text = count.ToString();
    }

    public void SetNumOfRiserStacks(int count)
    {
        NumOfRiserStacks.text = count.ToString();
    }
}
