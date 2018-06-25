using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class BossEnemy : MonoBehaviour {

	public enum State { 
		Start, 
		Idle, 
		Search, 
		Follow, 
		Attack, 
		Gimmick, 
		Weak, 
		Enrage, 
		Desperate, 
		Dead 
	}

    protected GameObject Prefab { get; set; }

    protected string Type { get; set; }
    protected int ID { get; set; }

    protected Transform[] Targets { get; set; }

    protected EnemyMovement Move { get; set; }
    protected EnemyHealth Health { get; set; }
    protected EnemyAttack Attack { get; set; }

    protected FieldOfView FOV { get; set; }
    protected Pathfinding PathFinder { get; set; }


	protected BossEnemy()
    {
        Type = "test";
        ID = 0;
        Targets = new Transform[1];
        Move = new EnemyMovement();
        Health = new EnemyHealth();
        Attack = new EnemyAttack();
        FOV = new FieldOfView();
    }

	protected BossEnemy(string lable,
                 int num,
                 Transform[] transforms,
                 EnemyMovement enemyMovement,
                 EnemyHealth enemyHealth,
                 EnemyAttack enemyAttack,
                 FieldOfView fieldOfView)
    {
        Type = lable;
        ID = num;
        Targets = transforms;
        Move = enemyMovement;
        Health = enemyHealth;
        Attack = enemyAttack;
        FOV = fieldOfView;

    }

    ~BossEnemy()
    {
        Type = "";
        ID = -1;
        Targets = null;
        Move = null;
        Health = null;
        Attack = null;
        FOV = null;
        gameObject.SetActive(false);
        Destroy(this);
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

    public static bool operator ==(BossEnemy a, BossEnemy b)
    {
        return a.Type == b.Type;
    }

    public static bool operator !=(BossEnemy a, BossEnemy b)
    {
        return a.Type != b.Type;
    }

}
