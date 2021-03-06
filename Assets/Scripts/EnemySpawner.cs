﻿using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyEasy;
    public GameObject enemyMedium;
    public GameObject enemyHard;

    public int numberOfEnemies;

    Renderer groundRenderer;
    Vector3 leftBottomCorner;
    Vector3 rightUpperCorner;


    void Start ()
    {
        GetGroundCornerCoordinates();
        SpawnEnemies();
    }

    void GetGroundCornerCoordinates()
    {
        groundRenderer = GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>();
        leftBottomCorner = groundRenderer.bounds.min + new Vector3(1.5f, .5f, 1.5f);
        rightUpperCorner = groundRenderer.bounds.max + new Vector3(-1.5f, .5f, -1.5f);
    }

    public void SpawnEnemies()
    {
        for (var i = 0; i < numberOfEnemies; i++)
        {
            var newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));

            while (Physics.CheckSphere(newPosition, 0))
                newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));

            var newEnemy = enemyEasy;
            if (i / 5 >= 1)
                newEnemy = enemyMedium;
            else if (i / 15 >= 2)
                newEnemy = enemyHard;

            Instantiate(newEnemy, newPosition, Quaternion.identity);
        }
    }
}
