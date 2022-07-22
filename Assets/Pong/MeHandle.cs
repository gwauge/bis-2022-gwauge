using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;
using SpeechIO;

public class MeHandle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;
    LevelManager levelManager; 
    SpeechOut speechOut;
    private Vector3 startPosition = new Vector3(0, 0.2f, -5f);

    void Start()
    {
        speechOut = new SpeechOut();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        ActivatePaddle();    
    }

    public async Task ActivatePaddle()
    {
        // upperHandle.FreezeRotation();    

        transform.position = startPosition;
        await upperHandle.MoveToPosition(transform.position);
    }

    void FixedUpdate()
    {
        transform.position = (upperHandle.HandlePosition(transform.position));
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Collision with " + other.transform.gameObject.name);

        if (other.transform.gameObject.name == "Ball") {
            // collision sound

            if (levelManager.levelNumber == 0) levelManager.NextLevel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                upperHandle.Freeze();
            }
            else
            {
                upperHandle.Free();
            }
            free = !free;
        }
    }
    
    void OnApplicationQuit() {
        speechOut.Stop();
    }
}
