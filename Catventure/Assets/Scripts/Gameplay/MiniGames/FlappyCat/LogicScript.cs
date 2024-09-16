using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gameplay.MiniGames.FlappyCat
{
    public class LogicScript : MonoBehaviour
    {
        public int playerScore;
        public Text scoreText;
        public GameObject gameOverScreen;

        //this does activate the function over the 3 dots in the Skript menu
        [ContextMenu("increase Score")]
        
        public void AddScore(int addScore)
        {
            if (playerScore >= 9) WinGame();
            if (gameOverScreen.activeSelf) return;
            
            playerScore ++;
            scoreText.text=playerScore.ToString();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LeaveGame()
        {
            SceneManager.LoadScene("Garden");
        }

        public void GameOver()
        {
            if (gameOverScreen) gameOverScreen.SetActive(true);
        }
        
        private static void WinGame()
        {
            SceneManager.LoadScene("EndingApartment");
        }
    }
}
