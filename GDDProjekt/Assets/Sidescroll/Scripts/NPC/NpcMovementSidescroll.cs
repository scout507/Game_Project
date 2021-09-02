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
    public bool mortar;

    //private
    Vector3 target;
    float randomHold;
    Animator animator;

    void Start()
    {
        randomHold = Random.Range(1.5f, 2);
        rb2D = GetComponent<Rigidbody2D>();
        target = targetToMove.position;
        animator = GetComponent<Animator>();
        animator.SetBool("Back", false);
    }

    void Update()
    {
        if (aiManager.gameEndNpc)
        {
            animator.SetBool("Back", true);
            target = cityHall.position;
            move = true;
        }

        if (move)
        {
            animator.SetBool("Walking", true);
            Vector2 direction = (target - transform.position).normalized;
            rb2D.velocity = direction * speed;
        }
        else animator.SetBool("Walking", false);

        if (Vector2.Distance(transform.position, target) <= randomHold)
        {
            if (mortar)
            {
                animator.SetTrigger("Stop");
                mortar = false;
            }

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
