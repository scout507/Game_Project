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
    public AiManager aiManager;

    //private
    Vector3 target;
    float randomHold;

    void Start()
    {
        randomHold = Random.Range(1.5f, 2);
        rb2D = GetComponent<Rigidbody2D>();
        target = targetToMove.position;
    }

    void Update()
    {
        if (aiManager.gameEndNpc)
        {
            target = cityHall.position;
            GetComponent<SpriteRenderer>().flipX = true;
            move = true;
        }

        if (move)
        {
            Vector2 direction = (target - transform.position).normalized;
            rb2D.velocity = direction * speed;
        }

        if (Vector2.Distance(transform.position, target) <= randomHold)
        {
            rb2D.velocity = Vector3.zero;
            move = false;
            if (aiManager.gameEndNpc)
            {
                aiManager.npcsLeft.Remove(gameObject);
                aiManager.npcsRight.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
