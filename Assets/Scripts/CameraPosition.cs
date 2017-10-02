using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private Transform ground;

    void Start()
    {
        if (!ground)
            ground = GameObject.FindGameObjectWithTag("Ground").transform;
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(ground.position.x, ground.position.y + height, ground.position.z - distance);
        transform.LookAt(ground);
    }
}
