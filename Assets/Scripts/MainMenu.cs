using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGameBtn()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
