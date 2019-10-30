using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameState : MonoBehaviour
{
    // Comnfiguration variables
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int brickPoints = 5;
    [SerializeField] TextMeshProUGUI final_score;
    [SerializeField] bool autoPlayStatus;

    // state variables
    [SerializeField] int playerScore = 0;

    // awake method is called before the start method.
    private void Awake()
    {
        int gameStateCount = FindObjectsOfType<gameState>().Length;
        if (gameStateCount > 1)
        {
            gameObject.SetActive(false);
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
        final_score.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void addScore()
    {
        playerScore += brickPoints;
        final_score.text = playerScore.ToString();
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }

    public bool autoplaySwitch()
    {
        return autoPlayStatus;
    }
}
