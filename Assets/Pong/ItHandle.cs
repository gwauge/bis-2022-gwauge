using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;
using SpeechIO;

public class ItHandle : MonoBehaviour
{
    PantoHandle lowerHandle;
    bool free = true;
    public float speed;
    Rigidbody ballRb;
    SpeechOut speechOut;
    LevelManager levelManager; 
    public bool playing = false;
    private Vector3 startPosition = new Vector3(0, 0.35f, -12.5f);

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        ballRb = GetComponent<Rigidbody>();
        speechOut = new SpeechOut();
    }

    public async Task ResetBall() {
        ballRb.velocity = Vector3.zero;

        lowerHandle.Free();
        await lowerHandle.MoveToPosition(startPosition);
        transform.position = startPosition;
    }

    public async Task ActivateBall() {
        await lowerHandle.SwitchTo(gameObject,20.0f);
        
        SetInitialVelocity();
    }

    // void OnTriggerEnter(Collider other) {
    //     Debug.Log("Collision with " + other.name);

    //     if (other.CompareTag("Paddle")) {
    //         // collision sound

    //         if (levelManager.levelNumber == 0) {
    //             speechOut.Speak("Gut gemacht! Weiter gehts.");
    //             levelManager.NextLevel();
    //         }
    //     }
    // }

    public void SetInitialVelocity() {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = -1; 
        ballRb.velocity = new Vector3(sx * speed, 0, sy * speed);
        // Debug.Log("Velocity: " + ballRb.velocity + "Sxy: " + sx + " " + sy);
    }

    async void FixedUpdate()
    {
        // transform.position = lowerHandle.HandlePosition(transform.position);
        if (transform.position.z < -17f 
            && levelManager.levelNumber == 1) levelManager.NextLevel();
        
        if (transform.position.z > -1f) {
            speechOut.Speak("You lost.");
            transform.position = startPosition;
            levelManager.RestartLevel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                lowerHandle.Freeze();
            }
            else
            {
                lowerHandle.Free();
            }
            free = !free;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetInitialVelocity();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position = startPosition;
            SetInitialVelocity();
        }
    }
    
    void OnApplicationQuit() {
        speechOut.Stop();
    }
}
