using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	EnemyPooler spawnPooler;

	private void Start()
	{
        spawnPooler = EnemyPooler.Instance;
	}

	private void FixedUpdate()
	{
        spawnPooler.SpawnFromPool("LesserEnemy", transform.position, Quaternion.identity);
	}
}
