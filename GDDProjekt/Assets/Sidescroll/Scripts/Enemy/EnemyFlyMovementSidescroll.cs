using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyMovementSidescroll : EnemyMovementSidescroll
{
    //public
    public Vector3 left;
    public Vector3 right;

    void Start()
    {
        target = wallTarget.position;

        left = new Vector3(target.x - 10, transform.position.y, target.z);
        right = new Vector3(target.x + 10, transform.position.y, target.z);
        if (comeFromLeft) target = right;
        if (!comeFromLeft) target = left;
    }

    void FixedUpdate()
    {
        if (aiManager.gameEndEnemy)
        {
            target = cityHall.position;
            speed = gameEndSpeed;
            move = true;
        }

        moveFunction();

        if (Vector2.Distance(transform.position, target) <= 1)
        {
            if (aiManager.gameEndEnemy)
            {
                move = false;
                rb2D.velocity = Vector3.zero;
            }

            if (target == right) target = left;
            else target = right;
        }
    }
}
