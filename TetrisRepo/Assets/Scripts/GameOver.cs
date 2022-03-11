using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Group group;
    //Playfield sharePoints;

    //public Text finalScore;
    public  GameObject gameOverUi;
   

    void Start()
    {
        
    }

    public void GameOverScreen()
    {
        gameOverUi.SetActive(true);

    }

    public void sharedScore()
    {
        //Playfield sharePoints = GetComponent<Playfield>();
        //sharePoints.updateUi();
        //finalScore.text = updateUi().ToString();            
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
   
    public void QuitButton()
    {
        Application.Quit();
    }
}
