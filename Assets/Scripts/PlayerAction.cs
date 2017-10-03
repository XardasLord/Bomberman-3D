using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameObject bomb;
    public int numberOfBombs;

    private GameManagerEngine gameManagerEngine;

    void Start()
    {
        gameManagerEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameManagerEngine>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManagerEngine.CountBombsOnMap() < numberOfBombs)
            PlantBomb();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.MovePosition(transform.position + move);
    }

    private void PlantBomb()
    {
        var position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.transform.position.y, Mathf.RoundToInt(transform.position.z));
        Instantiate(bomb, position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            gameManagerEngine.GetHit();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("ExtraItem"))
        {
            gameManagerEngine.TakeExtraItem(collider.gameObject);
            Destroy(collider.gameObject);
        }
    }
}