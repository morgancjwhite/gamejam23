using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool timerIsRunning;
    private TextMeshProUGUI textComponent;
    private GameController gameController;
    private float timeToNuke;

    private void Start()
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // Starts the timer automatically
        timerIsRunning = true;
        timeToNuke = gameController.timeToNuke;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeToNuke > 0)
            {
                timeToNuke -= Time.deltaTime;
                DisplayTime(timeToNuke);
            }
            else
            {
                print("Time has run out!");
                timeToNuke = 0;
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