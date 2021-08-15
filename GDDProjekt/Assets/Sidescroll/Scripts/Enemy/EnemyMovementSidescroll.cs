using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSidescroll : MonoBehaviour
{
    //public
    public Rigidbody2D rb2D;
    public float speed;
    public float gameEndSpeed;
    public Transform cityHall;
    public Transform wallTarget;
    public bool comeFromLeft = false;
    public bool move = true;
    public bool gameEnd = false;

    //private
    public Vector3 target;

    void Awake(){
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void moveFunction()
    {
        if (move)
        {
            Vector2 direction = (target - transform.position).normalized;
            rb2D.velocity = direction * speed;
        }
    }
}
