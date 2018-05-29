using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {

        public string tag;
        public GameObject prefab;
        public int size;

    }

    #region Singleton
    public static SpawnPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
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
            Debug.LogWarning("Pool with tag" + tag + "doesn't exist.");
            return null;
        }

        GameObject entityToSpawn = poolDictionary[tag].Dequeue();

        entityToSpawn.SetActive(true);
        entityToSpawn.transform.position = position;
        entityToSpawn.transform.rotation = rotation;

        IPooledEntity pooledEntity = entityToSpawn.GetComponent<IPooledEntity>();

        if (pooledEntity != null)
        {
            pooledEntity.OnEntitySpawn();
        }

        poolDictionary[tag].Enqueue(entityToSpawn);

        return entityToSpawn;
    }

}
