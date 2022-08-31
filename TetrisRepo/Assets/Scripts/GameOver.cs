using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Group group;
    Playfield playField;
    PauseMenu pauseMenu;

    public Text finalScore;
    public  GameObject gameOverUi;
   

    void Start()
    {
        playField = GetComponent<Playfield>();
        pauseMenu = GetComponent<PauseMenu>();
    }

    public void GameOverScreen()
    {
        gameOverUi.SetActive(true);
        pauseMenu.enabled = false;
        finalScore.text = playField.hud_Score.text.ToString();
        playField.enabled = false;

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
