using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    MeHandle paddle;
    ItHandle ball;
    GameObject panto;
    Level pantoLevel;
    SpeechOut speechOut;
    public int levelNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
        panto = GameObject.Find("Panto");
        pantoLevel = panto.GetComponent<Level>();
        paddle = GameObject.FindObjectOfType<MeHandle>();
        ball = GameObject.FindObjectOfType<ItHandle>();

        // paddle.SetActive(true);
        // panto.SetActive(true);

        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }

        LoadLevel(0);
        StartGame();
    }
    async void StartGame()
    {   
        await paddle.GetComponent<MeHandle>().ActivatePaddle();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextLevel()
    {
        speechOut.Speak("Good Job! Continue with next Level!");
        levels[levelNumber].SetActive(false);
        levelNumber++;
        LoadLevel(levelNumber);
    }

    public void RestartLevel() {
        speechOut.Speak("Restarting level " + (levelNumber + 1));
        levels[levelNumber].SetActive(false);
        LoadLevel(levelNumber);
    }

    async void LoadLevel(int n)
    {
        if (n < 0 || n >= levels.Length)
        {
            return;
        }
        Debug.Log("Loading level " + levelNumber);
        
        levels[n].SetActive(true);
        
        // ball.SetActive(false);
        // paddle.SetActive(false);
        await ball.ResetBall();
        await paddle.ActivatePaddle();

        await pantoLevel.PlayIntroduction();

        await ball.ActivateBall();
        // paddle.transform.position = panto.GetComponent<UpperHandle>().GetPosition();
        // paddle.SetActive(true);
        // ball.SetActive(true);
        
    }
    
    void OnApplicationQuit() {
        speechOut.Stop();
    }
}
