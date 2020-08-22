using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    // Private Singleton-style instance. Accessed by static property Instance later in script
    static private GameManager _Instance;

    public GameSettingsScriptableObject gameSettingsSO;

    private PlatformsPooler platformsPooler;
    private float spawnRate;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        platformsPooler = PlatformsPooler.Instance;

        spawnRate = gameSettingsSO.initialSpawnRate;

        StartCoroutine("LateStart");

        SaveGameManager.Load();
    }

    private void Update()
    {
        spawnRate -= Time.deltaTime;
        if (spawnRate < 0)
        {
            platformsPooler.SpawnFromPool("Platform", new Vector3(0f, -7.5f, 0f), Quaternion.identity);
            spawnRate = gameSettingsSO.initialSpawnRate;
        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnFirstMovingPlatform();
    }

    public void SpawnFirstMovingPlatform()
    {
        platformsPooler.SpawnFromPool("Platform", new Vector3(0f, -5f, 0f), Quaternion.identity);
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
    static public GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("GameManager:Instance getter - Attempt to get value of Instance before it has been set.");
                return null;
            }
            return _Instance;
        }
        set
        {
            if (_Instance != null)
            {
                Debug.LogError("GameManager:Instance setter - Attempt to set Instance when it has already been set.");
            }
            _Instance = value;
        }
    }

    static public GameSettingsScriptableObject GameSettingsSO
    {
        get
        {
            if (Instance != null)
            {
                return Instance.gameSettingsSO;
            }
            return null;
        }
    }
}