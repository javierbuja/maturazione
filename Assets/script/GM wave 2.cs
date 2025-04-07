using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button restartButton;
    private Boolean gameRunning;

public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public bool isGameRunning()
    { 
         return gameRunning; 
    }
    public void GameOver()
    { 
        gameRunning = false;
        gameOverPanel.SetActive(true);
    }

}
