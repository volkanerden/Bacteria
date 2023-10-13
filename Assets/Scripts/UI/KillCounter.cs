using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public TextMeshProUGUI killCounterText;
    public TextMeshProUGUI rageCountdownText;
    private RageManager rageManager;
    private float currentTime = 5f;

    void Awake()
    {
        rageManager = FindObjectOfType<RageManager>();
        rageManager.OnExitRage += RageManager_OnExitRage;
    }

    private void OnDestroy()
    {
        rageManager.OnExitRage -= RageManager_OnExitRage;
    }

    private void RageManager_OnExitRage()
    {
        rageCountdownText.text = "";
        currentTime = 5f;
    }

    void Update()
    {
        killCounterText.text = rageManager.killCount.ToString();

        if(rageManager.isRageMode)
        {
            currentTime -= Time.deltaTime;
            rageCountdownText.text = currentTime.ToString("0");
        }
    }
}