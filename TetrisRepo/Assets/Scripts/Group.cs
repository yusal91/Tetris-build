using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    //public AudioSource movingSound;
    //public AudioSource rotateSound;     

    float lastFall = 0;                                              // Time since last gravity tick

    // Use this for initialization
    void Start()
    {       
        if (!isValidGridPos())                                       // Default position not valid? Then it's game over
        {
            GameOver gameOverUI = FindObjectOfType<GameOver>();
            gameOverUI.GameOverScreen();
            Debug.Log("GAME OVER");            
            Destroy(gameObject);  
        }
    }

    // Update is called once per frame
    void Update()                                                      // Making block move left or right
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))                       // move to left               
        {           
            transform.position += new Vector3(-1, 0, 0);               // Modify position
            //movingSound.Play();

            if (isValidGridPos())                                       // See if it's valid
                updateGrid();                                           // It's valid. Update grid.
            else
                transform.position += new Vector3(1, 0, 0);             // Its not valid. revert.
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))                  // move to right                   
        {            
            transform.position += new Vector3(1, 0, 0);
            //movingSound.Play();

            if (isValidGridPos())                                      // See if valid
                updateGrid();                                          // It's valid. Update grid.
            else                
                transform.position += new Vector3(-1, 0, 0);           // It's not valid. revert.
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))                   // Rotate
        {            
            transform.Rotate(0, 0, -90);
            //rotateSound.Play();
            
            if (isValidGridPos())                                    // See if valid
                
                updateGrid();                                        // It's valid. Update grid.
            else                
                transform.Rotate(0, 0, 90);                          // It's not valid. revert.
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))                // Fall
        {
            //movingSound.Play();
            transform.position += new Vector3(0, -1, 0);            // Modify position

            // See if valid
            if (isValidGridPos())                                   // See if valid
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);         // It's not valid. revert.

                Playfield.deleteFullRows();                         // Clear filled horizontal lines

                FindObjectOfType<Spawner>().spawnNext();            // Spawn next Group

                enabled = false;                                    // Disable script
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) ||             // Move Downwards and Fall
                 Time.time - lastFall >= 1)
        {
            //movingSound.Play();
            transform.position += new Vector3(0, -1, 0);            // Modify position

            if (isValidGridPos())                                   // See if valid
            {
                updateGrid();                                       // It's valid. Update grid.
            }
            else
            {                
                transform.position += new Vector3(0, 1, 0);         // It's not valid. revert.
                                
                Playfield.deleteFullRows();                         // Clear filled horizontal lines

                FindObjectOfType<Spawner>().spawnNext();            // Spawn next Group
                                
                enabled = false;                                    // Disable script
            }

            lastFall = Time.time;
        }

    }

    void updateGrid()                                                  // Remove old children from grid
    {                                                                  // If a group changed its position, then it has to remove all the old block positions 
        for (int y = 0; y < Playfield.h; ++y)                          // from the grid and add all the new block positions to the grid:
            for (int x = 0; x < Playfield.w; ++x)                      //  block's parent Transform with the current transform like we did above.
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        foreach (Transform child in transform)                           // Add new children to grid
        {                
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    bool isValidGridPos ()                                              // put several blocks into one GameObject and called it Tetro?
    {                                                                   // We will need a function that helps us to verify each child block's position:
        foreach (Transform child in transform)                          
        {                                                               
            Vector2 v = Playfield.roundVec2(child.position);            
                                                                        
            if (!Playfield.insideBorder(v))                              // Not inside Border?
                return false;                                           
                                                                        
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&           // Block in grid cell (and not part of same group)?
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

            
}
