using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public float distance;
    public float height;

    private Transform player;

    void Start()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z - distance);
        transform.LookAt(player);
    }
}
