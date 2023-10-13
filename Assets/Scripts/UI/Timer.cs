using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float currentTime;
   // public GameOver gameOver;

    void Update()
    {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("0.0");
    }
}