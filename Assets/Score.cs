using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameController gameController;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        int zombieCount = gameController.zombieCount;
        string fillerText = " zombie";
        string scoreText = zombieCount + fillerText;
        if (zombieCount > 1)
        {
            scoreText += "s";
        }
        text.text = scoreText;
    }
}