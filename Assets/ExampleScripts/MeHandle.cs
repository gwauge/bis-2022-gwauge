using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class MeHandle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;

    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        // upperHandle.FreezeRotation();
        await upperHandle.MoveToPosition(transform.position);
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
