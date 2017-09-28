using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    float timer;
	
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer > 3)
        {
            SceneManager.LoadScene("Level1");
        }
	}
}
