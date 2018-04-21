using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {
    
    protected string name;
    protected int id;
    protected float movementSpeed;
    protected float turnRate;
    protected float lifeTime;
    protected bool isFreindly;


    public Enemy(string lable, int id, float ms, float tr, float lt, bool f)
    {
        name = lable;
        id = id;
        movementSpeed = ms;
        turnRate = tr;
        lifeTime = lt;
        isFreindly = f;
    }

    ~Enemy(){}

}
