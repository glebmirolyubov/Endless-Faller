using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsPooler : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

}

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}
