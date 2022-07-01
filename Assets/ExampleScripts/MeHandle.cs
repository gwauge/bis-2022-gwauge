using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class MeHandle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;
    new Collider collider;

    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        collider = GetComponent<Collider>();

        // collider.enabled = false;
        // transform.position = new Vector3(0, 0.2f, -13);
        Debug.Log("Pos1: " + transform.position + " Panto pos: " + upperHandle.transform.position);
        await upperHandle.MoveToPosition(transform.position);
        Debug.Log("Pos2: " + transform.position);
        // collider.enabled = true;
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
