using System.Collections;
using System.Collections.Generic;
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
    public float initialSpawnRate = 2f;
    [Range(1.5f, 4f)]
    public float platformGap = 2f;
    public Vector3 playerStartPosition;
}