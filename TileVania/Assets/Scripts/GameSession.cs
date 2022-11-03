using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int score=0;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreTxt;
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession>1)
        {
            Destroy(gameObject);
            
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    public void playerDeathProcess()
    {
        if(playerLives>1)
        {
            TakeLive();
        }

        else
        {
            ResetGameSession();
        }
    }
    private void Start()
    {
        
        liveText.text=playerLives.ToString();
        scoreTxt.text=score.ToString();
    }
    public void AddToScore(int scoreToAdd)
    {
        score+=scoreToAdd;
        scoreTxt.text = score.ToString();

    }

    private void TakeLive()
    {
        playerLives--;
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
        liveText.text = playerLives.ToString();

    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetGamepersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
