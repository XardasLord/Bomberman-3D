using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 100f;

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = move * speed * Time.deltaTime;
    }
}