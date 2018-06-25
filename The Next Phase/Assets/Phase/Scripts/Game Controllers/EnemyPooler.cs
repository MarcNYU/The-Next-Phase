using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {

        public string tag;
        public GameObject prefab;
        public int size;

    }

    #region Singleton
	public static EnemyPooler Instance;

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
            Queue<GameObject> enemyPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
				GameObject enemy = Instantiate(pool.prefab);
                enemy.SetActive(false);
				enemyPool.Enqueue(enemy);
            }
            poolDictionary.Add(pool.tag, enemyPool);


        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "doesn't exist.");
            return null;
        }

		GameObject enemyToSpawn = poolDictionary[tag].Dequeue();

        enemyToSpawn.SetActive(true);
        enemyToSpawn.transform.position = position;
        enemyToSpawn.transform.rotation = rotation;

		IPooledEnemy pooledEnemy = enemyToSpawn.GetComponent<IPooledEnemy>();

        if (pooledEnemy != null)
        {
            pooledEnemy.OnEnemySpawn();
        }

		if (enemyToSpawn.GetComponent<EnemyHealth>().isDead)
		{
			if (enemyToSpawn.GetComponent<EnemyHealth>().isSinking)
				StartCoroutine(ExecuteAfterTime(enemyToSpawn, 2F));
			else
				poolDictionary[tag].Enqueue(enemyToSpawn);
		}
            
        return enemyToSpawn;
    }

	IEnumerator ExecuteAfterTime(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		poolDictionary[tag].Enqueue(obj);
	}

}
