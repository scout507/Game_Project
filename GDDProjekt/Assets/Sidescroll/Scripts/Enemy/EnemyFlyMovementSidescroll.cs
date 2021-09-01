using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyMovementSidescroll : EnemyMovementSidescroll
{
    //public
    public Vector3 left;
    public Vector3 right;
    bool lookingToMid = true;

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
            if (!lookingToMid && comeFromLeft) transform.rotation = Quaternion.Euler(0, 0, 0);
            if (!lookingToMid && !comeFromLeft) transform.rotation = Quaternion.Euler(0, 180, 0);
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

            if (target == right)
            {
                lookingToMid = !lookingToMid;
                target = left;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                lookingToMid = !lookingToMid;
                target = right;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
