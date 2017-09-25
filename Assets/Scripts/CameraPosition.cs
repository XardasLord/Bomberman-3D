using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    public float distance;
    public float height;
        
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z - distance);
        transform.LookAt(player);
    }
}
