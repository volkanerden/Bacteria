using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public bool isGameOver = false;
    void Start()
    {
        gameOverText = GetComponent<TextMeshProUGUI>();
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isGameOver == true)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
}
