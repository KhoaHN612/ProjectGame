using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandSlimeBehavior : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

 /*   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            body.velocity = new Vector2(body.velocity.x, 10);
        }
    }*/
}
