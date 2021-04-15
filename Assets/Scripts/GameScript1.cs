using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameScript1 : MonoBehaviour
{
    
   //Spielfeld Definition
    public static int height = 20;
    public static int width = 10;

    //Grid Definition
    private static Transform[,] grid = new Transform[width, height];
    private static Transform[,] grid2 = new Transform[width, height];
    
    //Punkte
    private int scoreOneLine = 50;
    private int scoreTwoLine = 100;
    private int scoreThreeLine = 300;
    private int scoreFourLine = 1400;

    //Audio
    [SerializeField] private AudioClip clearLine;
    public AudioSource audioSource;

    //HUD- Score
    public TMPro.TextMeshProUGUI hud_score_player1;
    public TMPro.TextMeshProUGUI hud_score_player2;
    private int currentScoreP1 = 0;
    private int currentScoreP2 = 0;
    private static int numberOfRowsThisTurnP1 = 0;
    private static int numberOfRowsThisTurnP2 = 0;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateScoreP1();
        UpdateUIP1();
        UpdateScoreP2();
        UpdateUIP2();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    void PlayLinecleared()
    {
        audioSource.PlayOneShot(clearLine);
    }



    //Player1 -- Start
    public void UpdateScoreP1()
    {
        if (numberOfRowsThisTurnP1 > 0)
        {
            if (numberOfRowsThisTurnP1 == 1)
            {
                ClearedOneLineP1();
            }
            else if (numberOfRowsThisTurnP1 == 2)
            {
                ClearedTwoLinesP1();
            }
            else if (numberOfRowsThisTurnP1 == 3)
            {
                ClearedThreeLinesP1();
            }
            else if (numberOfRowsThisTurnP1 == 4)
            {
                ClearedFourLinesP1();
            }
            numberOfRowsThisTurnP1 = 0;
        }
    }
    public int ClearedOneLineP1()
    {
        currentScoreP1 += scoreOneLine;

        return currentScoreP1;
    }
    public void ClearedTwoLinesP1()
    {
        currentScoreP1 += scoreTwoLine;
    }
    public void ClearedThreeLinesP1()
    {
        currentScoreP1 += scoreThreeLine;
    }
    public void ClearedFourLinesP1()
    {
        currentScoreP1 += scoreFourLine;
    }
    public void IncreaseDifficultyP1()
    {
        if (currentScoreP1 > 500 && currentScoreP1! > 1000)
        {
            FindObjectOfType<TetrisBlockPlayer1>().fallTime = 0.7f;
        }
        if (currentScoreP1 > 1000 && currentScoreP1 > 500)
        {
            FindObjectOfType<TetrisBlockPlayer1>().fallTime = 0.5f;
        }
    }
    public void UpdateUIP1()
    {
        hud_score_player1.text = currentScoreP1.ToString();

        IncreaseDifficultyP1();
    }
    public bool P1CheckIsAboveGrid(TetrisBlockPlayer1 Tetrimino)
    {

        for (int i = 0; i < width; ++i)
        {
            foreach (Transform mino in Tetrimino.transform)
            {
                Vector2 pos = mino.position;

                if (pos.y > height - 2)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void P1CheckForLines()// checks if tetrimino Objects form a line
    {

        for (int i = height - 1; i >= 0; i--)
        {
            if (P1HasLine(i))
            {
                P1DeleteLine(i);
                P1RowDown(i);

            }
        }
    }
    void P1DeleteLine(int i)// Deletes the current Tetrisline
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;

        }
        PlayLinecleared();
    }
    void P1RowDown(int i)//moves the Lines above the deleted Lines down to the next lines
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
    public void P1AddToGrid() //adds Tetriminos to the Grid
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;


        }

    }
    bool P1HasLine(int i) // checks if tetrimino Objects form a line 
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        numberOfRowsThisTurnP1++;
        return true;
    }
    public bool ValidMoveP1()
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

    //Player1 -- End

    //Player2 -- Start
    public void UpdateScoreP2()
    {
        if (numberOfRowsThisTurnP2 > 0)
        {
            if (numberOfRowsThisTurnP2 == 1)
            {
                ClearedOneLineP2();
            }
            else if (numberOfRowsThisTurnP2 == 2)
            {
                ClearedTwoLinesP2();
            }
            else if (numberOfRowsThisTurnP2 == 3)
            {
                ClearedThreeLinesP2();
            }
            else if (numberOfRowsThisTurnP2 == 4)
            {
                ClearedFourLinesP2();
            }
            numberOfRowsThisTurnP2 = 0;
        }
    }
    public int ClearedOneLineP2()
    {
        currentScoreP2 += scoreOneLine;

        return currentScoreP2;
    }
    public void ClearedTwoLinesP2()
    {
        currentScoreP2 += scoreTwoLine;
    }
    public void ClearedThreeLinesP2()
    {
        currentScoreP2 += scoreThreeLine;
    }
    public void ClearedFourLinesP2()
    {
        currentScoreP2 += scoreFourLine;
    }
    public void IncreaseDifficultyP2()
    {
        if (currentScoreP2 > 500 && currentScoreP2! > 1000)
        {
            FindObjectOfType<TetrisBlockPlayer2>().fallTime = 0.7f;
        }
        if (currentScoreP2 > 1000 && currentScoreP2 > 500)
        {
            FindObjectOfType<TetrisBlockPlayer2>().fallTime = 0.5f;
        }
    }
    public void UpdateUIP2()
    {
        hud_score_player2.text = currentScoreP2.ToString();

        IncreaseDifficultyP2();
    }
    public bool P2CheckIsAboveGrid(TetrisBlockPlayer2 Tetrimino)
    {

        for (int i = 0; i < width; ++i)
        {
            foreach (Transform mino in Tetrimino.transform)
            {
                Vector2 pos = mino.position;

                if (pos.y > height - 2)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void P2CheckForLines()// checks if tetrimino Objects form a line
    {

        for (int i = height - 1; i >= 0; i--)
        {
            if (P2HasLine(i))
            {
                P2DeleteLine(i);
                P2RowDown(i);

            }
        }
    }
    void P2DeleteLine(int i)// Deletes the current Tetrisline
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid2[j, i].gameObject);
            grid2[j, i] = null;

        }
        PlayLinecleared();
    }
    void P2RowDown(int i)//moves the Lines above the deleted Lines down to the next lines
    {

        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid2[j, y] != null)
                {
                    grid2[j, y - 1] = grid2[j, y];
                    grid2[j, y] = null;
                    grid2[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }

    }
    public void P2AddToGrid() //adds Tetriminos to the Grid
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid2[roundedX, roundedY] = children;


        }

    }
    bool P2HasLine(int i) // checks if tetrimino Objects form a line 
    {
        for (int j = 0; j < width; j++)
        {
            if (grid2[j, i] == null)
                return false;
        }

        numberOfRowsThisTurnP2++;
        return true;
    }
    public bool ValidMoveP2()
    {

        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid2[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }

    //Player2 -- End
}
