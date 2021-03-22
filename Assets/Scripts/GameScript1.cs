using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript1 : MonoBehaviour
{
    
   //Spielfeld Definition
    public static int height = 20;
    public static int width = 10;

    //Grid Definition
    private static Transform[,] grid = new Transform[width, height];
    
    //Punkte
    public int scoreOneLine = 50;
    public int scoreTwoLine = 100;
    public int scoreThreeLine = 300;
    public int scoreFourLine = 1400;

    [SerializeField] private AudioClip clearLine;
    public AudioSource audioSource;

    public TMPro.TextMeshProUGUI hud_score_player1;

    private int currentScore = 0;

    private static int numberOfRowsThisTurn = 0;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    void PlayLinecleared()
    {
        audioSource.PlayOneShot(clearLine);
    }

    public void UpdateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLines();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLines();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLines();
            }
            numberOfRowsThisTurn = 0;
        }
    }

    public int ClearedOneLine()
    {
        currentScore += scoreOneLine;

        return currentScore;
    }
    public void ClearedTwoLines()
    {
        currentScore += scoreTwoLine;
    }
    public void ClearedThreeLines()
    {
        currentScore += scoreThreeLine;
    }
    public void ClearedFourLines()
    {
        currentScore += scoreFourLine;
    }


    public void UpdateUI()
    {
        hud_score_player1.text = currentScore.ToString();
    }

    public void CheckForLines()// checks if tetrimino Objects form a line
    {

        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);

            }
        }
    }

    bool HasLine(int i) // checks if tetrimino Objects form a line 
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        numberOfRowsThisTurn++;
        return true;
    }

    void DeleteLine(int i)// Deletes the current Tetrisline
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            
        }
        PlayLinecleared();
    }

    void RowDown(int i)//moves the Lines above the deleted Lines down to the next lines
    {

        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }

    }

   public void AddToGrid() //adds Tetriminos to the Grid
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;

        }

    }

    public bool ValidMove()
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }

}
