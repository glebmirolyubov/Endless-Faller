﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject startPanel;

    private bool canPause;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PrepareSceneForPlay();
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + Score.ToString();

        if (Input.anyKeyDown && startPanel.activeInHierarchy)
        {
            StartGameAfterAnyKeyPressed();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseGame();
        }
    }

    public void IncrementScore()
    {
        Score++;
    }

    /// <summary>
    /// <para>This method pauses and unpauses the game when ESC is pressed down.</para>
    /// </summary>
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

    /// <summary>
    /// <para>This method is called when the MainCharacter leaves the play area, i.e. loosing.</para>
    /// <para>Different operations are done to go into the game over state.</para>
    /// </summary>
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

    /// <summary>
    /// <para>This method resets the scene as it was when it first loaded.</para>
    /// <para>It is used to not reload the scene to minimize loading time.</para>
    /// </summary>
    public void ResetScene()
    {
        MainCharacter.Instance.SetCharacterStartState();
        Score = 0;
        gameOverPanel.SetActive(false);
        PlatformsPooler.Instance.DespawnAll();
        GameManager.Instance.ResetValues();
        GameManager.Instance.SpawnFirstMovingPlatform();
        MainCharacter.Instance.highscoreEffectPrefab.SetActive(false);
        startPanel.SetActive(true);
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void StartGameAfterAnyKeyPressed()
    {
        startPanel.SetActive(false);
        canPause = true;
        Time.timeScale = 1;
    }

    void PrepareSceneForPlay()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        startPanel.SetActive(true);
        canPause = false;
        Time.timeScale = 0;
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
