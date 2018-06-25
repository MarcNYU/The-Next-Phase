using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	public Transform player;               // Reference to the player's position.
    public PlayerHealth playerHealth;      // Reference to the player's health.
    public EnemyHealth enemyHealth;        // Reference to this enemy's health.
    public NavMeshAgent nav;               // Reference to the nav mesh agent.


    public void Start()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

}

