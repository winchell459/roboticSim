using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject RulesPanel, ScoringPanel, ControlsPanel, InformationPanel;
    // Start is called before the first frame update
    void Start()
    {
        RulesPanel.SetActive(false);
        ScoringPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        InformationPanel.SetActive(true);
    }

    public void RulesButton()
    {
        RulesPanel.SetActive(true);
        ScoringPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        InformationPanel.SetActive(false);
    }

    public void ScoringButton()
    {
        RulesPanel.SetActive(false);
        ScoringPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        InformationPanel.SetActive(false);
    }
    public void ControlsButton()
    {
        RulesPanel.SetActive(false);
        ScoringPanel.SetActive(false);
        ControlsPanel.SetActive(true);
        InformationPanel.SetActive(false);
    }

    public void StartMatchButton()
    {
        SceneManager.LoadScene(1);
    }
}
