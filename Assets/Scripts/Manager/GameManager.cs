using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGameOver;
    private bool isGameClear;
    private float timer;
    private float gameCountdown;
    private float givenTime;

    public GameObject stageRule;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameClear == false)
        {
            if (isGameOver == false)
            {
                timer += Time.deltaTime;
                gameCountdown = givenTime - timer;

                if (gameCountdown <= 0)
                {
                    GameOver();
                }
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("게임오버");
        isGameOver = true;
    }

    public void GameClear()
    {

        if (stageRule != null)
        {
            if (!stageRule.GetComponent<NewStageRule>().IsFinish())
            {
                Debug.Log("스테이지의 클리어 조건이 아직 만족되지 않았습니다.");
                return;
            }
        }
        Time.timeScale = 0;
        Debug.Log("클리어");
        isGameClear = true;

    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public bool GetIsGameClear()
    {
        return isGameClear;
    }

    public float GetGameCountdown()
    {
        return gameCountdown;
    }

    public string GetStageRule()
    {
        
        return stageRule != null ? stageRule.GetComponent<NewStageRule>().GetRuleMessage() : "";
    }

    public void InitializeStage()
    {
        Time.timeScale = 1;
        isGameClear = false;
        isGameOver = false;
        timer = 0f;
        givenTime = 180.0f;
    }
}
