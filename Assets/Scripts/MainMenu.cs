using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highscoreText;

    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void StartNewGameBtn()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
