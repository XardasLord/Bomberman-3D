using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed = 2f;
    public GameObject bomb;

    private GameManagerEngine gameManagerEngine;

    void Start()
    {
        gameManagerEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameManagerEngine>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlantBomb();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.MovePosition(transform.position + move);
    }

    void PlantBomb()
    {
        var position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.transform.position.y, Mathf.RoundToInt(transform.position.z));
        Instantiate(bomb, position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameManagerEngine.GetHit();
        }
    }
}