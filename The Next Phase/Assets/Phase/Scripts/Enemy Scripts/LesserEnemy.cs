using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemy : Enemy 
{
    public enum State { Idle, Chasing, Attacking }

    public bool isFriendly;

    public Pathfinding PathFinder;

}
