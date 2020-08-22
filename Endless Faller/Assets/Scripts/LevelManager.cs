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
    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = "Score: " + Score.ToString();
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverScoreText.text = "SCORE: " + Score.ToString();

        gameOverPanel.SetActive(true);
    }

    public void ResetScene()
    {
        MainCharacter.Instance.SetCharacterStartingPosition();
        Score = 0;
        gameOverPanel.SetActive(false);
        PlatformsPooler.Instance.DespawnAll();
        GameManager.Instance.SpawnFirstMovingPlatform();

        Time.timeScale = 1;
    }

    // ---------------- Static Section ---------------- //

    /// <summary>
    /// <para>This static public property provides some protection for the Singleton _Instance.</para>
    /// <para>get {} does return null, but throws an error first.</para>
    /// <para>set {} allows overwrite of _Instance by a 2nd instance, but throws an error first.</para>
    /// <para>Another advantage of using a property here is that it allows you to place
    /// a breakpoint in the set clause and then look at the call stack if you fear that 
    /// something random is setting your _Instance value.</para>
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
