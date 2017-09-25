using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed = 100f;
    public GameObject bomb;

    void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlantBomb();
        }
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = move * speed * Time.deltaTime;
    }

    void PlantBomb()
    {
        var position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.transform.position.y, Mathf.RoundToInt(transform.position.z));
        Instantiate(bomb, position, Quaternion.identity);
    }
}