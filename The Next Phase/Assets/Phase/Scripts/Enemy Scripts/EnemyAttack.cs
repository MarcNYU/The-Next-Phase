using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    public Animator anim;                              // Reference to the animator component.
    public GameObject player;                          // Reference to the player GameObject.
    public bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    public float timer;                                // Timer for counting up to the next attack.

    public float attackDistanceTnreshold = 1.5f;
    public float nextAttackTime;

    float myCollisionRadius;
    float targetCollisionRadius;

    public void Start()
    {
        // Setting up the references.      
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
 
}
