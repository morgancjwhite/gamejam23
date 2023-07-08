using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning;
    private TextMeshProUGUI textComponent;
    private GameController gameController;

    private void Start()
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                print("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                gameController.Nuke();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string timeLeft = string.Format("{0:00}:{1:00}", minutes, seconds);
        textComponent.text = "Time left: " + timeLeft;
    }
}