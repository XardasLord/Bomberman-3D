using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManagerEngine : MonoBehaviour {

    public Text pointsText;
    public Text winText;
    public Text bombText;
    public Text extraItemText;
    public Text levelText;
    public GameObject[] extraItems;
    [Range(0, 100)]
    public int chanceForExtraItem;
    public float explosionRange = 1f;

    private MapGenerator mapGenerator;
    private EnemySpawner enemySpawner;
    private PlayerAction playerAction;
    private bool isAlive = true;
    private bool isWon = false;
    private int numberOfEnemies;
    private int score;
    private int level;

    void Start()
    {
        mapGenerator = gameObject.GetComponent<MapGenerator>();
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
        numberOfEnemies = enemySpawner.numberOfEnemies;

        playerAction = mapGenerator.GetPlayerObject().GetComponent<PlayerAction>();

        score = 0;
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
        var animator = collider.gameObject.GetComponent<Animator>();
        animator.SetBool("Dead", true);

        var enemyAction = collider.gameObject.GetComponent<EnemyAction>();
        enemyAction.enabled = false;

        Physics.IgnoreCollision(collider, playerAction.GetComponent<Collider>());

        //TODO: Get death animation lenght...
        Destroy(collider.gameObject, 3f);

        score++;

        numberOfEnemies--;
        if (numberOfEnemies == 0)
        {
            SetWinText();
            isWon = true;

            Invoke("PrepareNextLevel", 5f);
        }
    }

    public void DestroyBrick(Collider collider)
    {
        if(Random.Range(0, 101) < chanceForExtraItem)
        {
            // % propability to spawn an extra item.
            var randomItem = Random.Range(0, extraItems.Length);
            var newRandomItem = (GameObject)Instantiate(extraItems[randomItem], collider.transform.position, Quaternion.identity);
            newRandomItem.name = extraItems[randomItem].name;
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
        if(PlayerPrefs.GetInt("Highscore") < score)
            PlayerPrefs.SetInt("Highscore", score);

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
        SetLevelText();
    }

    public int CountBombsOnMap()
    {
        return GameObject.FindGameObjectsWithTag("Bomb").Length;
    }

    private void SetPointsText()
    {
        pointsText.text = "Score: " + score.ToString();
    }

    private void SetBombsText()
    {
        bombText.text = "Bombs planted: " + CountBombsOnMap().ToString() + "/" + playerAction.numberOfBombs.ToString();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + level.ToString();
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

        enemySpawner.numberOfEnemies += level;
        numberOfEnemies = enemySpawner.numberOfEnemies;
        mapGenerator.InitNewMap();
        enemySpawner.SpawnEnemies();

        playerAction = mapGenerator.GetPlayerObject().GetComponent<PlayerAction>();
        explosionRange = 1;
        level++;
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

        var items = GameObject.FindGameObjectsWithTag("ExtraItem");
        foreach (var item in items)
            Destroy(item);
    }

    public IEnumerator TakeExtraItem(GameObject item)
    {
        //TODO: Check taken item and add some feature depends on the item.
        extraItemText.text = item.name;
        extraItemText.enabled = true;

        switch (item.name)
        {
            case "Extra - Bomb Range!":
                explosionRange++;
                break;
            case "Extra - Bomb Limit!":
                playerAction.numberOfBombs++;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(2f);
        extraItemText.enabled = false;
    }
}
