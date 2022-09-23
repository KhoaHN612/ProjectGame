using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int jumpTimeLimit;
    [SerializeField] private int jumpTime = 0;
    [SerializeField] private float LimitCooldownAttackTime;
    [SerializeField] private float CooldownAttackTime;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool attacking = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float bodyMove = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(bodyMove * speed, body.velocity.y);

        if (bodyMove > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (bodyMove < -0.1f)
            transform.localScale = new Vector3(-1,1,1);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K)) && (jumpTime < jumpTimeLimit))
            Jump();

        if (Input.GetKey(KeyCode.L))
            body.velocity = new Vector2(MathF.Sign(bodyMove) * speed * 3, body.velocity.y);

        if (Input.GetKey(KeyCode.J) && (attacking == false))
            Attack();

        if (attacking)
        {
            Debug.Log(attacking);
            CooldownAttackTime += Time.deltaTime;

            if (CooldownAttackTime >= LimitCooldownAttackTime)
            {
                CooldownAttackTime = 0;
                attacking = false;
            }
        }

        anim.SetBool("run", bodyMove != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        anim.SetTrigger("jump");
        jumpTime++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
        jumpTime = 0;
    }
    private void Attack()
    {
        attacking = true;
        anim.SetTrigger("attack");
    }
}
