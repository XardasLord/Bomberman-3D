using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public GameObject explosionEffect;

    void Start ()
    {
        Invoke("Explode", 3f);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        GetComponent<MeshRenderer>().enabled = false;
        //transform.FindChild("Collider").gameObject.SetActive(false);
        Destroy(gameObject, .3f);
    }
}
