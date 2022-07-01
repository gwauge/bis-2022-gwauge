using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class MeHandle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;

    async void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(0, 0.2f, -13);
        Debug.Log("Pos1: " + transform.position);
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.MoveToPosition(transform.position);
        GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Pos2: " + transform.position);
    }

    async Task ActivatePaddle() {
        await upperHandle.SwitchTo(gameObject);
        // upperHandle.FreeRotation();
    }

    void FixedUpdate()
    {
        transform.position = (upperHandle.HandlePosition(transform.position));
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
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
}
