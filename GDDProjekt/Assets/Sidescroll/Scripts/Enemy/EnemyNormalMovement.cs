using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalMovement : EnemyMovementSidescroll
{
    //public
    public float minDistance;
    public float maxDistance;
    public float randomHold;

    void Start()
    {
        randomHold = Random.Range(minDistance, maxDistance);
        target = wallTarget.position;
    }


    void Update()
    {
        if (gameEnd)
        {
            target = cityHall.position;
            randomHold = 2;
            speed = gameEndSpeed;
            move = true;
        }

        moveFunction();

        if (Vector2.Distance(transform.position, target) <= randomHold)
        {
            rb2D.velocity = Vector3.zero;
            move = false;
        }
    }
}
