using System;
using System.Collections;
using UnityEngine;

public class RageManager : MonoBehaviour
{
    [SerializeField] 
    private Fire fireScript;

    public bool isRageMode = false;
    private float rageDuration = 5f;

    public int killCount = 0;
    private int killThreshold = 3;

    private Coroutine rageModeCO;

    public event Action OnExitRage;

    public void IncrementKillCount()
    {
        killCount++;

        if (killCount % killThreshold == 0)
        {
            EnterRageMode();
        }
    }

    public void EnterRageMode()
    {
        if (!isRageMode)
        {
            isRageMode = true;
            fireScript.InvokeRageModeChanged(isRageMode);

            if(rageModeCO != null)
            {
                StopCoroutine(rageModeCO);
            }

            rageModeCO = StartCoroutine(ExitRageModeAfterDelay());
        }
    }

    public void ExitRageMode()
    {
        if (isRageMode)
        {
            isRageMode = false;
            fireScript.InvokeRageModeChanged(isRageMode);
            OnExitRage?.Invoke();
        }
    }

    private IEnumerator ExitRageModeAfterDelay()
    {
        yield return new WaitForSeconds(rageDuration);
        ExitRageMode();
    }
}
