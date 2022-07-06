using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Level level = GameObject.Find("Panto").GetComponent<Level>();
        await level.PlayIntroduction();

        // await GameObject.FindObjectOfType<MeHandle>().ActivatePaddle();
        await GameObject.FindObjectOfType<ItHandle>().ActivateBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
