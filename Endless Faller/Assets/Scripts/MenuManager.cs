using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Text highscoreText;
    public Animation transitionAnimation;

    void Start()
    {
        LoadHighscore();
    }

    public void InitiateLoading()
    {
        StartCoroutine("LoadGameScene");
    }

    void LoadHighscore()
    {
        SaveGameManager.Load();
        highscoreText.text = "HIGHSCORE: " + SaveGameManager.GetHighScore();
    }

    IEnumerator LoadGameScene()
    {
        transitionAnimation.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}