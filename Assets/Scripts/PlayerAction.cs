using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameObject bomb;
    public int numberOfBombs;

    private GameManagerEngine gameManagerEngine;
    private Animator anim;

    void Start()
    {
        gameManagerEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameManagerEngine>();
        anim = gameObject.GetComponent<Animator>();
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

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            anim.SetInteger("Speed", 2);

            if (Input.GetAxis("Horizontal") > 0)
                rigidbody.MoveRotation(Quaternion.Euler(0, 90, 0));
            else if (Input.GetAxis("Horizontal") < 0)
                rigidbody.MoveRotation(Quaternion.Euler(0, 270, 0));
            else if (Input.GetAxis("Vertical") > 0)
                rigidbody.MoveRotation(Quaternion.Euler(0, 0, 0));
            else if (Input.GetAxis("Vertical") < 0)
                rigidbody.MoveRotation(Quaternion.Euler(0, 180, 0));
        }
        else
            anim.SetInteger("Speed", 0);
    }

    private void PlantBomb()
    {
        var position = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
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
            StartCoroutine(gameManagerEngine.TakeExtraItem(collider.gameObject));
            Destroy(collider.gameObject);
        }
    }
}