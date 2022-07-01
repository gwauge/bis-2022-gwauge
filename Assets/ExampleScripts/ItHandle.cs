using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class ItHandle : MonoBehaviour
{
    PantoHandle lowerHandle;
    bool free = true;
    public float speed = 0.1f;
    Rigidbody ballRb;

    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        ballRb = GetComponent<Rigidbody>();
    }

    public async Task ActivateBall() {
        // gets called in levelscript
        
        await lowerHandle.SwitchTo(gameObject);
        
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sy = 1; 
        ballRb.velocity = new Vector3(sx * speed, 0, sy * speed);
        // Debug.Log("Velocity: " + ballRb.velocity + "Sxy: " + sx + " " + sy);
    }

    void FixedUpdate()
    {
        // transform.position = lowerHandle.HandlePosition(transform.position);
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
            float sx = Random.Range(0, 2) == 0 ? -1 : 1;
            float sy = 1; 
            ballRb.velocity = new Vector3(sx * speed, 0, sy * speed);
            Debug.Log("Velocity: " + ballRb.velocity);
        }
    }
}
