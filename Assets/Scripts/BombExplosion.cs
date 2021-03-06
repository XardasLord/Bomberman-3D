﻿using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public GameObject explosionEffect;

    private GameManagerEngine gameManagerEngine;

    void Start ()
    {
        Invoke("Explode", 3f);
        gameManagerEngine = GameObject.FindGameObjectWithTag("Engine").GetComponent<GameManagerEngine>();
    }

    void Explode()
    {
        if (explosionEffect != null)
            CreateExplosion();

        CheckHits();

        gameObject.GetComponent<AudioSource>().Play();
        transform.position += Vector3.down;
        Destroy(gameObject, 1f);
    }

    void CreateExplosion()
    {
        var forwardExplosion = true;
        var rightExplosion = true;
        var backExplosion = true;
        var leftExplosion = true;
        GameObject explosion;

        for (var i = 0; i <= gameManagerEngine.explosionRange; i++)
        {
            if (forwardExplosion)
            {
                explosion = (GameObject)Instantiate(explosionEffect, transform.position + Vector3.forward * i, Quaternion.identity);
                Destroy(explosion, 3f);
            }
            if(rightExplosion)
            {
                explosion = (GameObject)Instantiate(explosionEffect, transform.position + Vector3.right * i, Quaternion.identity);
                Destroy(explosion, 3f);
            }
            if (backExplosion)
            {
                explosion = (GameObject)Instantiate(explosionEffect, transform.position + Vector3.back * i, Quaternion.identity);
                Destroy(explosion, 3f);
            }
            if(leftExplosion)
            {
                explosion = (GameObject)Instantiate(explosionEffect, transform.position + Vector3.left * i, Quaternion.identity);
                Destroy(explosion, 3f);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward * (i + 1), out hit, 1f))
                forwardExplosion = false;
            if (Physics.Raycast(transform.position, Vector3.right * (i + 1), out hit, 1f))
                rightExplosion = false;
            if (Physics.Raycast(transform.position, Vector3.back * (i + 1), out hit, 1f))
                backExplosion = false;
            if (Physics.Raycast(transform.position, Vector3.left * (i + 1), out hit, 1f))
                leftExplosion = false;
        }
    }

    void CheckHits()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.forward, out hit, gameManagerEngine.explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
            else if (hit.collider.CompareTag("ExtraItem"))
                gameManagerEngine.DestroyExtraItem(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, gameManagerEngine.explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
            else if (hit.collider.CompareTag("ExtraItem"))
                gameManagerEngine.DestroyExtraItem(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.back, out hit, gameManagerEngine.explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
            else if (hit.collider.CompareTag("ExtraItem"))
                gameManagerEngine.DestroyExtraItem(hit.collider);
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, gameManagerEngine.explosionRange))
        {
            if (hit.collider.CompareTag("Player"))
                gameManagerEngine.GetHit();
            else if (hit.collider.CompareTag("Enemy"))
                gameManagerEngine.DestroyEnemy(hit.collider);
            else if (hit.collider.CompareTag("Brick"))
                gameManagerEngine.DestroyBrick(hit.collider);
            else if (hit.collider.CompareTag("ExtraItem"))
                gameManagerEngine.DestroyExtraItem(hit.collider);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * gameManagerEngine.explosionRange;
        Vector3 back = transform.TransformDirection(Vector3.back) * gameManagerEngine.explosionRange;
        Vector3 left = transform.TransformDirection(Vector3.left) * gameManagerEngine.explosionRange;
        Vector3 right = transform.TransformDirection(Vector3.right) * gameManagerEngine.explosionRange;

        //TODO: Change DrawRay to explosion effect in the future.
        Debug.DrawRay(transform.position, forward, Color.red, 1f);
        Debug.DrawRay(transform.position, back, Color.red, 1f);
        Debug.DrawRay(transform.position, left, Color.red, 1f);
        Debug.DrawRay(transform.position, right, Color.red, 1f);
    }
}
