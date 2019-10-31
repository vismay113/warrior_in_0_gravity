using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthDisplay : MonoBehaviour
{
    // configuration variables
    TextMeshProUGUI health_score;

    playerShip playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        health_score = GetComponent<TextMeshProUGUI>();
        playerHealth = FindObjectOfType<playerShip>();
    }

    // Update is called once per frame
    void Update()
    {
        health_score.text = playerHealth.getHealthScore().ToString();
    }
}
