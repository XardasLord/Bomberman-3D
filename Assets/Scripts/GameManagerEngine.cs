using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerEngine : MonoBehaviour {

    private bool isAlive = true;
	
	void FixedUpdate ()
    {
        if (!isAlive)
            GameOver();
	}

    public void GetHit()
    {
        isAlive = false;
    }

    public void DestroyEnemy(Collider collider)
    {
        //TODO: Add some points for destroying enemy, etc.
        Destroy(collider.gameObject);
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
}
