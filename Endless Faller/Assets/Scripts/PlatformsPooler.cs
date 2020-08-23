using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>This class is using a Pooling technique to spawn moving platforms.</para>
/// <para>Pooling is more efficient that creating and destroying objects all the time.</para>
/// </summary>
public class PlatformsPooler : MonoBehaviour
{
    public Transform platformsParent;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    static private PlatformsPooler _Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, platformsParent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag " + tag + " does not exist!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void DespawnAll()
    {
        GameObject[] pooledObjects = GameObject.FindGameObjectsWithTag("Platform");

        foreach(GameObject objectToDisable in pooledObjects)
        {
            objectToDisable.SetActive(false);
        }
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
    static public PlatformsPooler Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("PlatformsPooler:Instance getter - Attempt to get value of Instance before it has been set.");
                return null;
            }
            return _Instance;
        }
        set
        {
            if (_Instance != null)
            {
                Debug.LogError("PlatformsPooler:Instance setter - Attempt to set Instance when it has already been set.");
            }
            _Instance = value;
        }
    }

}

[System.Serializable]
public class Pool
{
    public string tag;
    public UnityEngine.GameObject prefab;
    public int size;
}
