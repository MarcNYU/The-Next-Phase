﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.


    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
	ParticleSystem deathParticles;
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    public bool isDead;                                // Whether the enemy is dead.
    public bool isSinking;                             // Whether the enemy has started sinking through the floor.

	public bool isCoruptable;
	public bool isFriendly;

	private System.Random random = new System.Random();

	public void Awake()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		deathParticles = gameObject.transform.Find("DeathEffect").GetComponent<ParticleSystem>();
		hitParticles = gameObject.transform.Find("DamageEffect").GetComponent<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();

		// Setting the current health when the enemy first spawns.
		if (isFriendly == true)
			startingHealth /= 2;
		currentHealth = startingHealth;
	}

	public void DeterminCoruptability()
	{
		int randomNum = random.Next(100);
		isCoruptable = randomNum < 25;
	}
    
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        //Destroy(gameObject, 2f);
    }

	public void Explode()
	{
		// Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

		// Set the position of the particle system to where the hit was sustained.
		deathParticles.transform.position = transform.position;

        // And play the particles.
		deathParticles.Play();

		// Increase the score by the enemy's score value.
        ScoreManager.score += scoreValue;
	}
}
