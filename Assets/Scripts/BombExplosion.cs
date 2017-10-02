using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public GameObject explosionEffect;
    public float explosionRange;

    private GameManagerEngine gameManagerEngine;

    void Start ()
    {
        Invoke("Explode", 3f);
        gameManagerEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameManagerEngine>();
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        CheckHits();

        GetComponent<MeshRenderer>().enabled = false;
        //transform.FindChild("Collider").gameObject.SetActive(false);
        Destroy(gameObject, .3f);
    }

    public void CheckHits()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.forward, out hit, explosionRange))
        {

            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.back, out hit, explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * explosionRange;
        Vector3 back = transform.TransformDirection(Vector3.back) * explosionRange;
        Vector3 left = transform.TransformDirection(Vector3.left) * explosionRange;
        Vector3 right = transform.TransformDirection(Vector3.right) * explosionRange;

        //TODO: Change DrawRay to explosion effect in the future.
        Debug.DrawRay(transform.position, forward, Color.red, 1f);
        Debug.DrawRay(transform.position, back, Color.red, 1f);
        Debug.DrawRay(transform.position, left, Color.red, 1f);
        Debug.DrawRay(transform.position, right, Color.red, 1f);
    }
}
