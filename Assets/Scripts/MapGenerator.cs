using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject staticWall;
    public GameObject brick;
    public GameObject ground;
    public int bricksNumber;

    int possitionSeparator = 2;
    Renderer groundRendrerer;
    Vector3 leftBottomCorner;
    Vector3 rightUpperCorner;

    void Start ()
    {
        GetGroundCornerCoordinates();
        GenerateWalls();
        GenerateBricks();
    }

    void GetGroundCornerCoordinates()
    {
        groundRendrerer = ground.GetComponent<Renderer>();
        leftBottomCorner = groundRendrerer.bounds.min + new Vector3(1.5f, .5f, 1.5f);
        rightUpperCorner = groundRendrerer.bounds.max + new Vector3(-1.5f, .5f, -1.5f);
    }

    void GenerateWalls()
    {
        Vector3 currentWallPossition = leftBottomCorner;

        for (int x = 0; x <= Mathf.Abs(rightUpperCorner.x * 2); x += possitionSeparator)
        {
            for (int z = 0; z <= Mathf.Abs(leftBottomCorner.z * 2); z += possitionSeparator)
            {
                currentWallPossition = leftBottomCorner + new Vector3(x, 0, z);

                Instantiate(staticWall, currentWallPossition, Quaternion.identity);
            }
        }
    }

    void GenerateBricks()
    {
        //TODO: Set minimum distance from player to generate bricks.
        for (var i = 0; i < bricksNumber; i++)
        {
            var newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));

            while(Physics.CheckSphere(newPosition, 0))
            {
                newPosition = new Vector3(Mathf.Round(Random.Range(leftBottomCorner.x, rightUpperCorner.x)), 0, Mathf.Round(Random.Range(leftBottomCorner.z, rightUpperCorner.z)));
            }

            Instantiate(brick, newPosition, Quaternion.identity);
        }
    }
}
