using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject HorizontalLinePrefab, VerticalLinePrefab;
    public Vector3 LineStartOffset = new Vector3(0.1f, 0, 0.1f);
    public float LineSpacing = 0.2f;
    public float BoardScaleFactor = 10;

    // Start is called before the first frame update
    void Start()
    {
        setupBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }

        //place Vertical lines
        float currentX = boardOrigin.x + LineStartOffset.x * BoardScaleFactor;
        while (currentX < boardEnd.x)
        {
            GameObject line = Instantiate(VerticalLinePrefab);
            line.transform.position = new Vector3(currentX, 0, transform.position.z);
            currentX += LineSpacing * BoardScaleFactor;
        }
    }
}
