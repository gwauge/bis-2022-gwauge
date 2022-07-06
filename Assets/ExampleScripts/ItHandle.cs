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

    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        ballRb = GetComponent<Rigidbody>();
        speechOut = new SpeechOut();
    }

    public async Task ActivateBall() {
        // gets called in levelscript
        
        await lowerHandle.SwitchTo(gameObject);
        
        setInitialVelocity();
    }

    public void setInitialVelocity() {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = -1; 
        ballRb.velocity = new Vector3(sx * speed, 0, sy * speed);
        // Debug.Log("Velocity: " + ballRb.velocity + "Sxy: " + sx + " " + sy);
    }

    async void FixedUpdate()
    {
        // transform.position = lowerHandle.HandlePosition(transform.position);
        if (transform.position.z < -17f) {
            speechOut.Speak("You have completed the level.");
            Object.Destroy(gameObject);
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
            setInitialVelocity();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position = new Vector3(0, 0.2f, -10);
            setInitialVelocity();
        }
    }
}
