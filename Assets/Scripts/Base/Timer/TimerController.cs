using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    private int stageTime = 20;
    private float timeSeconds;
    [SerializeField] private int timeMinutes;

    [SerializeField] private TMP_Text timeText;

    private bool onTimer = true;


    private void Start()
    {
        ResetTimer();
    }


    void Update()
    {
        if (onTimer)
        {
            if (timeSeconds <= 0)
            {
                timeSeconds += 60f;
                timeMinutes--;
            }
            else
            {
                timeSeconds -= Time.deltaTime;
            }
            SetTimer();
        }

        if (timeMinutes == 0 && timeSeconds <= 0)
            StopTimer();
    }

    private void SetTimer()
    {
        timeText.text = timeMinutes.ToString();
        timeText.text += " : ";
        if (((int)timeSeconds).ToString().Length == 1)
            timeText.text += "0";
        timeText.text += ((int)timeSeconds).ToString();
    }

    public void StopTimer()
    {
        onTimer = false;
    }

    public void TurnOnTimer()
    {
        onTimer = true;
    }

    public void ResetTimer()
    {
        timeMinutes = stageTime;
        timeSeconds = 0f;
    }
}
