using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    // configuration variable
    gameState loseStae;
    [SerializeField] float delayInSeconds = 2f;
    public void sceneLoader()
    {
        int currentSIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSIndex + 1);
    }

    public void goToStartMenu()
    {
        //loseStae.resetGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void playAgain()
    {
        //loseStae.resetGame();
        SceneManager.LoadScene("core_game");
    }

    public void LoadEndMenu()
    {
        StartCoroutine(delayGameOver());
    }

    IEnumerator delayGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("EndMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
