using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playfield : MonoBehaviour
{
    //the Grid itself
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    // Score Varialbles
    public int oneLine = 10;
    public int twoLine = 20;
    public int threeLine = 30;
    public int fourLine = 40;

    private static int numberOfRowsThisTurn;

    public  Text hud_Score;
    private  int currentScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        updateScore();
        updateUi();
    }

    public void updateUi()
    {
        hud_Score.text = currentScore.ToString();
    }

    public void updateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLine();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLine();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLine();
            }
            numberOfRowsThisTurn = 0;
        }
    }

    public void ClearedOneLine()
    {
        currentScore += oneLine;
    }
    public void ClearedTwoLine()
    {
        currentScore += twoLine;
    }
    public void ClearedThreeLine()
    {
        currentScore += threeLine;
    }
    public void ClearedFourLine()
    {
        currentScore += fourLine;
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------//
    public static Vector2 roundVec2(Vector2 v)          // public static function allows it to be accessed by other scripts as well
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&                   // What happens is that it first tests the x position which has to be between 0 and the grid width w
                (int)pos.x < w &&                    // and afterwards it finds out if the y position is still positive.
                (int)pos.y >= 0);
    }
                                                     // it doesn't check if pos.y < h because groups don't really move upwards, except for some rotations.
    
    public static void deleteRow(int y)             // This function deletes all Blocks in a certain row.
    {                                               // if the players fill every row.
        for (int x = 0; x < w; ++x)
        {                                           // The function takes the y parameter which is the row that is supposed to be deleted from the playfield.
            Destroy(grid[x, y].gameObject);         // Then it loops through every block in that row, Destroys it from the game and clears the reference 
            grid[x, y] = null;                      // to it by setting the grid entry to null.
        }
    }

    public static void decreaseRow (int y)         // The decreaseRow helper function
    {                                              // Whenever a row was deleted, the above rows should fall towards the bottom by one unit.
        for (int x = 0; x < w; ++x)
        {
            if (grid[x,y] != null)                   // move  one towards bottom
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);             // update block postion.
            }
        }
    }

    public static void decreaseRowsAbove(int y)       // The decreaseRowsAbove function
    {
        for (int i = y; i < h; ++i) decreaseRow(i);   // whenever a row was deleted, we want to decrease all rows above it, not just one:
    }

    public static bool isRowFull (int y)             // The IsRowFull Function
    {                                                // As mentioned before that a row should be deleted when it's full of blocks.
        for (int x = 0; x < w; ++x)

            if (grid[x, y] == null)
                return false;

        numberOfRowsThisTurn++;

        return true;
        
        
    }

    public static void deleteFullRows()           // The deletefullRows function
    {                                             // Now its time to put everything together and write a function that deletes all full rows and then
        for (int y = 0; y < h; ++y)               // always decreases the above row's y coordinate by one.
            if (isRowFull(y))
            { 
                deleteRow(y);
            decreaseRowsAbove(y + 1);
            --y;
            }
    }
}
