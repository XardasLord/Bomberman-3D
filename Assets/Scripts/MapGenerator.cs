using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject staticWallPrefab;
    public GameObject brickPrefab;
    public GameObject groundPrefab;
    public int numberOfBricks;
    public Material[] staticWallMaterials;
    public Material[] brickMaterials;
    public Material[] groundMaterials;

    int possitionSeparator = 2;
    Renderer groundRenderer;
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

        GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>().material = groundMaterials[Random.Range(0, groundMaterials.Length)];
    }

    void GetGroundCornerCoordinates()
    {
        groundRenderer = groundPrefab.GetComponent<Renderer>();
        leftBottomCorner = groundRenderer.bounds.min + new Vector3(1.5f, .5f, 1.5f);
        rightUpperCorner = groundRenderer.bounds.max + new Vector3(-1.5f, .5f, -1.5f);
    }

    private void SpawnPlayer()
    {
        var fixedLeftCorner = leftBottomCorner - new Vector3(1f, 0, 1f);

        playerGameObject = (GameObject)Instantiate(playerPrefab, fixedLeftCorner, Quaternion.identity);
    }

    void GenerateWalls()
    {
        staticWallPrefab.GetComponent<Renderer>().material = staticWallMaterials[Random.Range(0, staticWallMaterials.Length)];

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
        brickPrefab.GetComponent<Renderer>().material = brickMaterials[Random.Range(0, brickMaterials.Length)];

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

    public GameObject GetPlayerObject()
    {
        return playerGameObject;
    }
}
