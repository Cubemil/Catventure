using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicSkript : MonoBehaviour
{

    public int playerScore=0;
    public Text scoreText;
    public GameObject gameOverScreen;

    //this does activate the function over the 3 dots in the Skript menu
    [ContextMenu("increase Score")]
    public void AddScore(int addScore)
    {
        if (!gameOverScreen.activeSelf)
        {
            Debug.Log("+1");
            playerScore ++;
            scoreText.text=playerScore.ToString(); 
        }
        
    }

    public void RestartGame()
    {
        Debug.Log("reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeaveGame()
    {
        Debug.Log("Loading Garden...");
        SceneManager.LoadScene("Garden");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
