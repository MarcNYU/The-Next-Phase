using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Runner : LesserEnemy 
{
	public static Runner Instance;
    static bool initialized = false;

	float myCollisionRadius;
    float targetCollisionRadius;

	public override void Awake()
	{
		base.Awake();
	}

	public override void Start()
    {
		base.Start();

        if (!initialized)
        {
            initialized = true;
            Instance = this;
        }

        currentState = State.Search;
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = E_Attack.player.GetComponent<CapsuleCollider>().radius;
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
		E_Attack.timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (E_Attack.timer > E_Attack.nextAttackTime && E_Move.enemyHealth.currentHealth > 0)
        {
			float sqrDstToTarget = (E_Attack.player.transform.position - transform.position).sqrMagnitude;
			if (sqrDstToTarget < Mathf.Pow(E_Attack.attackDistanceTnreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
				E_Attack.nextAttackTime = Time.time + E_Attack.timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }

        // If the player has zero or less health...
		if (E_Move.playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
			E_Attack.anim.SetTrigger("PlayerDead");
        }
    }

    IEnumerator Attack()
    {
		currentState = State.Attack;
		E_Move.nav.enabled = false;

        Vector3 orignalPosition = transform.position;
		Vector3 dstToTarget = (E_Attack.player.transform.position - transform.position).normalized;
		Vector3 attackPosition = E_Attack.player.transform.position - dstToTarget * (myCollisionRadius + targetCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(orignalPosition, attackPosition, interpolation);

            yield return null;
        }

        // Reset the timer.
		E_Attack.timer = 0f;

        // If the player has health to lose...
		if (E_Move.playerHealth.currentHealth > 0)
        {
            // ... damage the player.
			E_Move.playerHealth.TakeDamage(E_Attack.attackDamage);
        }

		currentState = State.Chase;
		E_Move.nav.enabled = true;
    }
    
}
