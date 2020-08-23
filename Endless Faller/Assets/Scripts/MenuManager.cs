using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text highscoreText;

    void Start()
    {
        LoadHighscore();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    void LoadHighscore()
    {
        SaveGameManager.Load();
        highscoreText.text = "HIGHSCORE: " + SaveGameManager.GetHighScore();
    }
}