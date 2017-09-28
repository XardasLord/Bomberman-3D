using UnityEngine;
using UnityEngine.SceneManagement;

public class BombExplosion : MonoBehaviour {

    public GameObject explosionEffect;

    void Start ()
    {
        Invoke("Explode", 3f);
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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2;
        Vector3 back = transform.TransformDirection(Vector3.back) * 2;
        Vector3 left = transform.TransformDirection(Vector3.left) * 2;
        Vector3 right = transform.TransformDirection(Vector3.right) * 2;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Vector3.forward, 2f);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GameOver();
            }
            else if(hit.collider.CompareTag("Brick"))
            {
                DestroyBrick(hit.collider);
            }
        }

        hits = Physics.RaycastAll(transform.position, Vector3.left, 2f);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GameOver();
            }
            else if (hit.collider.CompareTag("Brick"))
            {
                DestroyBrick(hit.collider);
            }
        }

        hits = Physics.RaycastAll(transform.position, Vector3.right, 2f);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GameOver();
            }
            else if (hit.collider.CompareTag("Brick"))
            {
                DestroyBrick(hit.collider);
            }
        }

        hits = Physics.RaycastAll(transform.position, Vector3.back, 2f);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GameOver();
            }
            else if (hit.collider.CompareTag("Brick"))
            {
                DestroyBrick(hit.collider);
            }
        }

        //TODO: Change DrawRay to explosion effect in the future.
        Debug.DrawRay(transform.position, forward, Color.red, 1f);
        Debug.DrawRay(transform.position, back, Color.red, 1f);
        Debug.DrawRay(transform.position, left, Color.red, 1f);
        Debug.DrawRay(transform.position, right, Color.red, 1f);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void DestroyBrick(Collider collider)
    {
        //TODO: Chance to get some extra item.
        Destroy(collider.gameObject);
    }
}
