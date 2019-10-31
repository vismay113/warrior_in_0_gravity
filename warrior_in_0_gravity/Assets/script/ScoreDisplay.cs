using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // configuration variables
    TextMeshProUGUI final_score;

    gameState state;

    // Start is called before the first frame update
    void Start()
    {
        final_score = GetComponent<TextMeshProUGUI>();
        state = FindObjectOfType<gameState>();
    }

    // Update is called once per frame
    void Update()
    {
        final_score.text = state.getScore().ToString(); 
    }
}
