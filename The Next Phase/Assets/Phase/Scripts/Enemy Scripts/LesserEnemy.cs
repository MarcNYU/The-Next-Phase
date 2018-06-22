using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemy : MonoBehaviour, IPooledEnemy
{
	public enum State { Idle, Search, Chase, Attack, Dead }
	public State currentState;

	public int NumberOfTargets;

	protected GameObject Prefab { get; set; }

	protected string Type { get; set; }
	protected int ID { get; set; }

	protected Queue<Transform> POI { get; set; }
	protected List<Transform> Targets { get; set; }

	protected EnemyMovement E_Move;
	protected EnemyHealth E_Health;
	protected EnemyAttack E_Attack;

	protected FieldOfView FOV;
	protected Pathfinding PathFinder;


	protected LesserEnemy()
	{
		NumberOfTargets = 1;
		Type = "test";
		ID = 0;
		POI = new Queue<Transform>();
		Targets = new List<Transform>();
	}

	protected LesserEnemy(string lable,
				 int num,
				 Queue<Transform> pointsOfInterest,
				 int sizeOfTargetsArr,
				 List<Transform> targets)
	{
		Type = lable;
		ID = num;
		POI = pointsOfInterest;
		NumberOfTargets = sizeOfTargetsArr;
		Targets = targets;
	}

	protected LesserEnemy(LesserEnemy other)
		: this(other.Type, other.ID, other.POI, other.NumberOfTargets, other.Targets)
	{ }

	~LesserEnemy()
	{
		Type = "";
		ID = -1;
		POI = null;
		E_Move = null;
		E_Health = null;
		E_Attack = null;
		FOV = null;
	}

	public override string ToString()
	{
		return string.Format("{0} - {1}", this.ID, this.Type);
	}

	public override bool Equals(object other)
	{
		return this.Equals(other);
	}

	public override int GetHashCode()
	{
		return this.ID.GetHashCode();
	}

	public static bool operator ==(LesserEnemy a, LesserEnemy b)
	{
		return a.Type == b.Type;
	}

	public static bool operator !=(LesserEnemy a, LesserEnemy b)
	{
		return a.Type != b.Type;
	}


	public virtual void Awake()
    {
        E_Health = this.gameObject.AddComponent<EnemyHealth>();
        FOV = this.gameObject.AddComponent<FieldOfView>();
    }

    public virtual void Start()
    {
        E_Move = this.gameObject.AddComponent<EnemyMovement>();
        E_Attack = this.gameObject.AddComponent<EnemyAttack>();
    }

    public void OnEnemySpawn()
    {
        currentState = State.Idle;
    }

    public int PrioritizeTarget()
    {
        if (Targets.Count == 0) return -1;
        if (Targets.Count == 1) return 0;

        int index = -1;
        float minDistance = SearchFieldRadius(2F);
        for (int i = 0; i < Targets.Count; i++)
        {
            if (Distance(transform, Targets[i]) < minDistance)
            {
                minDistance = Distance(transform, Targets[i]);
                index = i;
            }
        }
        return index;
    }

    float Distance(Transform t1, Transform t2)
    {
        return Mathf.Sqrt(Mathf.Pow(t2.position.x - t1.position.x, 2) + Mathf.Pow(t2.position.y - t1.position.y, 2) + Mathf.Pow(t2.position.z - t1.position.z, 2));
    }

    float SearchFieldRadius(float multiplyer)
    {
        return FOV.viewRadius * multiplyer;
    }

	IEnumerator Search()
    {
        float refreshRate = .25f;

        while (POI.Peek() != null)
        {
            if (POI.Peek().GetComponent<GameObject>().tag == "Player")
            {
                currentState = State.Chase;
                Targets.Add(POI.Dequeue());
            }

            if (currentState == State.Search)
            {
                if (E_Move.enemyHealth.currentHealth > 0)
                {
                    // ... set the destination of the nav mesh agent to the top of queue.
                    E_Move.nav.SetDestination(POI.Dequeue().position);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }

    }

    IEnumerator Chase()
    {
        float refreshRate = .25f;

        while (Targets[PrioritizeTarget()] != null)
        {
            if (currentState == State.Chase)
            {
                if (E_Move.enemyHealth.currentHealth > 0 && E_Move.playerHealth.currentHealth > 0)
                {
                    // ... set the destination of the nav mesh agent to the player.
                    E_Move.nav.SetDestination(Targets[0].position);
                }
                // Otherwise...
                else
                {
                    // ... disable the nav mesh agent.
                    E_Move.enabled = false;
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

	IEnumerator Dead()
    {
        // If the enemy should be sinking...
		while (E_Health.isDead)
        {

			if (E_Health.isCoruptable)
			{
				E_Health.StartSinking();
				if (E_Health.isSinking)
				{
					// ... move the enemy down by the sinkSpeed per second.
					transform.Translate(-Vector3.up * E_Health.sinkSpeed * Time.deltaTime);
				}
			}
			else
			{
				E_Health.Explode();
			}
            yield return null;
        }
    }
}
