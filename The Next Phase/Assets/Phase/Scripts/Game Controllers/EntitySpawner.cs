using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {

    SpawnPooler spawnPooler;

	private void Awake()
	{
        spawnPooler = SpawnPooler.Instance;
	}

	private void FixedUpdate()
	{
        spawnPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
	}
}
