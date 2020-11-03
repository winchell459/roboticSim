using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelection : MonoBehaviour
{
    public Image OrangeSelection, TealSelection, PurpleSelection;

    private void Awake()
    {
        ClearSelection();
    }

    public void ClearSelection()
    {
        OrangeSelection.gameObject.SetActive(false);
        TealSelection.gameObject.SetActive(false);
        PurpleSelection.gameObject.SetActive(false);
    }

    public void SetSelection(Riser.RiserColors riserColor)
    {
        if (riserColor == Riser.RiserColors.Orange) OrangeSelection.gameObject.SetActive(true);
        else if (riserColor == Riser.RiserColors.Teal) TealSelection.gameObject.SetActive(true);
        else if (riserColor == Riser.RiserColors.Purple) PurpleSelection.gameObject.SetActive(true);
    }
}
