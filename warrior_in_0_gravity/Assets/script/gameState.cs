using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameState : MonoBehaviour
{
    // Comnfiguration variables

    // state variables
    int playerScore = 0;
    int healthScore = 0;

    // awake method is called before the start method.
    private void Awake()
    {
        GameSessionSingleton();
    }

    private void GameSessionSingleton()
    {
        int gameStateCount = FindObjectsOfType<gameState>().Length;
        if (gameStateCount > 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int getScore() { return playerScore; }

    public void addScore(int killPoints)
    {
        playerScore += killPoints;
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }
}
