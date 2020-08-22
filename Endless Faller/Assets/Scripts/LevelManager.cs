using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    // Private Singleton-style instance. Accessed by static property Instance later in script
    static private LevelManager _Instance;

    public int Score { get; private set; }

    [Header("Set in inspector")]
    public Text scoreText;
    public Text gameOverScoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private bool canPause;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        canPause = true;
    }

    private void Update()
    {
        scoreText.text = "Score: " + Score.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseGame();
        }
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        SaveGameManager.CheckHighScore(Score);
        SaveGameManager.Save();
        Time.timeScale = 0;
        canPause = false;
        highScoreText.text = "HIGHSCORE: " + SaveGameManager.GetHighScore();
        gameOverScoreText.text = "SCORE: " + Score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void ResetScene()
    {
        MainCharacter.Instance.SetCharacterStartState();
        Score = 0;
        gameOverPanel.SetActive(false);
        PlatformsPooler.Instance.DespawnAll();
        GameManager.Instance.SpawnFirstMovingPlatform();
        canPause = true;
        Time.timeScale = 1;
    }

    // ---------------- Static Section ---------------- //

    /// <summary>
    /// <para>This static public property provides some protection for the Singleton _Instance.</para>
    /// <para>get {} does return null, but throws an error first.</para>
    /// <para>set {} allows overwrite of _Instance by a 2nd instance, but throws an error first.</para>
    /// </summary>
    static public LevelManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("LevelManager:Instance getter - Attempt to get value of Instance before it has been set.");
                return null;
            }
            return _Instance;
        }
        set
        {
            if (_Instance != null)
            {
                Debug.LogError("LevelManager:Instance setter - Attempt to set Instance when it has already been set.");
            }
            _Instance = value;
        }
    }
}
