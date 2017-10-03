using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject staticWallPrefab;
    public GameObject brickPrefab;
    public GameObject enemyPrefab;
    public GameObject groundPrefab;
    public int numberOfBricks;
    public int numberOfEnemies;

    int possitionSeparator = 2;
    Renderer groundRendrerer;
    Vector3 leftBottomCorner;
    Vector3 rightUpperCorner;

    private GameObject playerGameObject;

    void Start()
    {
        InitNewMap();
    }

    public void InitNewMap()
    {
        GetGroundCornerCoordinates();
        SpawnPlayer();
        GenerateWalls();
        GenerateBricks();
        SpawnEnemies();
    }

    void GetGroundCornerCoordinates()
    {
        groundRendrerer = groundPrefab.GetComponent<Renderer>();
        leftBottomCorner = groundRendrerer.bounds.min + new Vector3(1.5f, .5f, 1.5f);
        rightUpperCorner = groundRendrerer.bounds.max + new Vector3(-1.5f, .5f, -1.5f);
    }

    private void SpawnPlayer()
    {
        var fixedLeftCorner = leftBottomCorner - new Vector3(1f, 0, 1f);

        playerGameObject = (GameObject)Instantiate(playerPrefab, fixedLeftCorner, Quaternion.identity);
    }

    void GenerateWalls()
    {
        var currentWallPossition = leftBottomCorner;

        for (int x = 0; x <= Mathf.Abs(rightUpperCorner.x * 2); x += possitionSeparator)
        {
            for (int z = 0; z <= Mathf.Abs(leftBottomCorner.z * 2); z += possitionSeparator)
            {
                currentWallPossition = leftBottomCorner + new Vector3(x, 0, z);

                Instantiate(staticWallPrefab, currentWallPossition, Quaternion.identity);
            }
        }
    }

    void GenerateBricks()
    {
        //TODO: Set minimum distance from player to generate bricks.
        for (var i = 0; i < numberOfBricks; i++)
        {
            var newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));

            while(Physics.CheckSphere(newPosition, 0))
            {
                newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));
            }

            Instantiate(brickPrefab, newPosition, Quaternion.identity);
        }
    }

    void SpawnEnemies()
    {
        for(var i = 0; i < numberOfEnemies; i++)
        {
            var newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));

            while (Physics.CheckSphere(newPosition, 0))
            {
                newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));
            }

            Instantiate(enemyPrefab, newPosition, Quaternion.identity);
        }
    }

    public GameObject GetPlayerObject()
    {
        return playerGameObject;
    }
}
