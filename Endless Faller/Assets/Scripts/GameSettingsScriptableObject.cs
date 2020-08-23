using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/GameSettingsSO", fileName = "GameSettingsSO.asset")]
[System.Serializable]
public class GameSettingsScriptableObject : ScriptableObject
{
    static public GameSettingsScriptableObject Instance; // This Scriptable Object is an unprotected Singleton

    public GameSettingsScriptableObject()
    {
        Instance = this; // Assign the Singleton as part of the constructor.
    }

    [Header("Set in inspector")]
    public Vector3 playerStartPosition;
    public float initialSpawnRate = 2f;
    [Range(0.001f, 0.05f)]
    public float spawnRateDecreasePerFrame;
    [Range(1.5f, 4f)]
    public float initialPlatformGap = 2f;
    public float initialPlatformSpeed;
    [Range(0f, 0.001f)]
    public float platformSpeedIncreasePerFrame;
}