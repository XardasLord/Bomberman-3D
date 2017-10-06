using UnityEngine;

public class EnemyAction : MonoBehaviour {

    public LayerMask layers;
    public float speed;
    public int points;
    
    private Vector3 positionBeforeRand;
    private int lastRandom;

    void Start()
    {
        RandomNewDirection();
    }
	
	void FixedUpdate()
    {
        if (lastRandom == 1)
        {
            if (Physics.Raycast(transform.position, Vector3.left, 0.7f, layers))
                RandomNewDirection();
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.x - positionBeforeRand.x) >= 1)
                {
                    RandomNewDirection();
                }
            }
        }
        else if (lastRandom == 2)
        {
            if (Physics.Raycast(transform.position, Vector3.right, 0.7f, layers))
                RandomNewDirection();
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.x - positionBeforeRand.x) >= 1)
                {
                    RandomNewDirection();
                }
            }
        }
        else if (lastRandom == 3)
        {
            if (Physics.Raycast(transform.position, Vector3.back, 0.7f, layers))
                RandomNewDirection();
            else
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.z - positionBeforeRand.z) >= 1)
                {
                    RandomNewDirection();
                }

            }
        }
        else if (lastRandom == 4)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, 0.7f, layers))
                RandomNewDirection();
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                if (Mathf.Abs(transform.position.z - positionBeforeRand.z) >= 1)
                {
                    RandomNewDirection();
                }

            }
        }
	}

    void RandomNewDirection()
    {
        lastRandom = Random.Range(1, 5);
        positionBeforeRand = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("StaticWall") || collision.collider.CompareTag("Brick") || collision.collider.CompareTag("Enemy"))
        {
            RandomNewDirection();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("StaticWall") || collision.collider.CompareTag("Brick") || collision.collider.CompareTag("Enemy"))
        {
            RandomNewDirection();
        }
    }
}
