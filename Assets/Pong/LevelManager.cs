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
    PantoHandle lowerHandle;
    PantoHandle upperHandle;
    public int levelNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
        panto = GameObject.Find("Panto");
        pantoLevel = panto.GetComponent<Level>();
        paddle = GameObject.FindObjectOfType<MeHandle>();
        ball = GameObject.FindObjectOfType<ItHandle>();
        upperHandle = panto.GetComponent<UpperHandle>();

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
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
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

        float startRotation = upperHandle.GetRotation();
        // Debug.Log("rotation:" + upperHandle.transform.rotation);
        
        await ball.ResetBall();
        await paddle.ActivatePaddle();
        lowerHandle.SetMaxMovementSpeed(1.5f);
        await pantoLevel.PlayIntroduction();
        lowerHandle.SetMaxMovementSpeed(99f);

        // upperHandle.Rotate(upperHandle.GetRotation() - startRotation + 0.125f);
        // Quaternion target = Quaternion.Euler(0, 0, 0);
        // upperHandle.transform.rotation = target;

        await ball.ActivateBall();
        // paddle.transform.position = panto.GetComponent<UpperHandle>().GetPosition();
        // paddle.SetActive(true);
        // ball.SetActive(true);
        
    }
    
    void OnApplicationQuit() {
        speechOut.Stop();
    }
}
