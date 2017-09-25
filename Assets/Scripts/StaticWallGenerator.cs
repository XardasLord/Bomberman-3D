using UnityEngine;

public class StaticWallGenerator : MonoBehaviour {

    public GameObject staticWall;
    public GameObject ground;
    int possitionSeparator = 2;

	void Start ()
    {
        GenerateWalls();
    }

    void GenerateWalls()
    {
        Renderer groundRendrerer = ground.GetComponent<Renderer>();
        Vector3 leftBottomCorner = groundRendrerer.bounds.min + new Vector3(1.5f, .5f, 1.5f);
        Vector3 rightUpperCorner = groundRendrerer.bounds.max + new Vector3(-1.5f, .5f, -1.5f);
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
}
