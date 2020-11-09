using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    public GameObject HorizontalLinePrefab, VerticalLinePrefab;
    public Vector3 LineStartOffset = new Vector3(0.1f, 0, 0.1f);
    public float LineSpacing = 0.2f;
    public float BoardScaleFactor = 10;

    public Text ScoreText, TimeText;
    public ScoringHandler SH;

    public float CountdownTime = 5;
    private float countdownStartTime;
    public float MatchTime = 60;
    private float matchStartTime;
    private bool matchStarted = false;
    private bool matchEnded = false;

    public Text CountdownText;

    // Start is called before the first frame update
    void Start()
    {
        setupBoard();
        startCountdown();
        //startMatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (matchStarted && !matchEnded) handleMatch();
        else if(CountdownTime + countdownStartTime > Time.time)
        {
            CountdownText.text = ((int)(CountdownTime - (Time.time - countdownStartTime))).ToString();
        }
        else
        {
            FindObjectOfType<TheRobotController>().MatchStarted();
            startMatch();
        }
    }

    private void startCountdown()
    {
        countdownStartTime = Time.time;
    }

    private void startMatch()
    {
        CountdownText.transform.parent.gameObject.SetActive(false);
        matchStartTime = Time.fixedTime;
        matchStarted = true;
    }

    private void handleMatch()
    {
        if(Time.fixedTime > matchStartTime + MatchTime)
        {
            matchEnded = true;
            ScoreText.text = SH.ScoreBoard().ToString();
        }
        else
        {
            TimeText.text = ((int)(MatchTime - (Time.fixedTime - matchStartTime))).ToString();
        }
    }

    private void setupBoard()
    {
        Vector3 boardOrigin = transform.position;
        Vector3 boardScale = transform.localScale;

        boardOrigin -= BoardScaleFactor * boardScale / 2;
        Vector3 boardEnd = boardOrigin + BoardScaleFactor * boardScale;

        //place Horizontal lines
        float currentZ = boardOrigin.z + LineStartOffset.z * BoardScaleFactor;
        while(currentZ < boardEnd.z)
        {
            GameObject line = Instantiate(HorizontalLinePrefab);
            line.transform.position = new Vector3(transform.position.x, 0, currentZ);
            currentZ += LineSpacing * BoardScaleFactor;
            line.transform.parent = transform;
        }

        //place Vertical lines
        float currentX = boardOrigin.x + LineStartOffset.x * BoardScaleFactor;
        while (currentX < boardEnd.x)
        {
            GameObject line = Instantiate(VerticalLinePrefab);
            line.transform.position = new Vector3(currentX, 0, transform.position.z);
            currentX += LineSpacing * BoardScaleFactor;
            line.transform.parent = transform;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuReturn()
    {
        SceneManager.LoadScene(0);
    }
}
