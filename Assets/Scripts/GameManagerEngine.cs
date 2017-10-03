using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerEngine : MonoBehaviour {

    public Text pointsText;
    public Text winText;

    private MapGenerator mapGenerator;
    private bool isAlive = true;
    private bool isWon = false;
    private int numberOfEnemies;
    private int points;
    private int level;

    void Start()
    {
        mapGenerator = gameObject.GetComponent<MapGenerator>();
        numberOfEnemies = mapGenerator.numberOfEnemies;
        points = 0;
        level = 1;

        SetPointsText();
    }

    void FixedUpdate ()
    {
        if (!isAlive && !isWon)
            GameOver();
	}

    public void GetHit()
    {
        isAlive = false;
    }

    public void DestroyEnemy(Collider collider)
    {
        Destroy(collider.gameObject);

        points++;
        SetPointsText();

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
        Destroy(collider.gameObject);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
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
    }
}
