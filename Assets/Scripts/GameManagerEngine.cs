﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerEngine : MonoBehaviour {

    public Text pointsText;
    public Text winText;
    public Text bombText;
    public GameObject[] extraItems;
    [Range(0, 100)]
    public int chanceForExtraItem;

    private MapGenerator mapGenerator;
    private PlayerAction playerAction;
    private bool isAlive = true;
    private bool isWon = false;
    private int numberOfEnemies;
    private int points;
    private int level;

    void Start()
    {
        mapGenerator = gameObject.GetComponent<MapGenerator>();
        numberOfEnemies = mapGenerator.numberOfEnemies;

        playerAction = mapGenerator.GetPlayerObject().GetComponent<PlayerAction>();

        points = 0;
        level = 1;
    }

    void FixedUpdate ()
    {
        if (!isAlive && !isWon)
            GameOver();
        
        UpdateGUI();
    }

    public void GetHit()
    {
        isAlive = false;
    }

    public void DestroyEnemy(Collider collider)
    {
        Destroy(collider.gameObject);

        points++;

        numberOfEnemies--;
        if (numberOfEnemies == 0)
        {
            SetWinText();
            isWon = true;

            Invoke("PrepareNextLevel", 3f);
        }
    }

    public void DestroyBrick(Collider collider)
    {
        //TODO: Chance to get some extra item.
        if(Random.Range(0, 101) < chanceForExtraItem)
        {
            // % propability to spawn an extra item.
            var randomItem = Random.Range(0, extraItems.Length);
            Instantiate(extraItems[randomItem], collider.transform.position, Quaternion.identity);
        }

        Destroy(collider.gameObject);
    }

    public void DestroyExtraItem(Collider collider)
    {
        //TODO: Some penalty maybe? Spawn some extra strong enemy, etc.
        Destroy(collider.gameObject);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private int CountEnemiesOnMap()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void UpdateGUI()
    {
        SetPointsText();
        SetBombsText();
    }

    public int CountBombsOnMap()
    {
        return GameObject.FindGameObjectsWithTag("Bomb").Length;
    }

    private void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    private void SetBombsText()
    {
        bombText.text = "Bombs planted: " + CountBombsOnMap().ToString() + "/" + playerAction.numberOfBombs.ToString();
    }

    private void SetWinText()
    {
        winText.text = "Prepare for the next level...";
    }

    private void PrepareNextLevel()
    {
        isWon = false;
        winText.text = "";

        DestroyAllObjects();

        mapGenerator.numberOfEnemies += level;
        numberOfEnemies = mapGenerator.numberOfEnemies;
        mapGenerator.InitNewMap();

        playerAction = mapGenerator.GetPlayerObject().GetComponent<PlayerAction>();
    }

    private void DestroyAllObjects()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        var staticWalls = GameObject.FindGameObjectsWithTag("StaticWall");
        foreach (var staticWall in staticWalls)
            Destroy(staticWall);

        var bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (var brick in bricks)
            Destroy(brick);

        var bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (var bomb in bombs)
            Destroy(bomb);
    }

    public void TakeExtraItem(GameObject item)
    {
        //TODO: Check taken item and add some feature depends on the item.
        Debug.Log("Took: " + item.name);
    }
}
