using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovementSidescroll : MonoBehaviour
{
    //public
    public Rigidbody2D rb2D;
    public float speed;
    public Transform targetToMove;
    public Transform cityHall;
    public bool toLeft = false;
    public bool move = true;
    public bool gameEnd = false;

    //private
    Vector3 target;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        target = targetToMove.position;
    }

    void Update()
    {
        if (gameEnd)
        {
            target = cityHall.position;
            move = true;
        }

        if (move)
        {
            Vector2 direction = (target - transform.position).normalized;
            rb2D.velocity = direction * speed;
        }

        if (Vector2.Distance(transform.position, target) <= 1)
        {
            rb2D.velocity = Vector3.zero;
            move = false;
            if (gameEnd) Destroy(gameObject);
        }
    }
}
