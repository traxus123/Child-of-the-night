using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPNJMoves : MonoBehaviour
{
    public Rigidbody body;
    public Animator anim;
    public float speed = 5.0f;
    public float patrolSpeed = 3.0f;
    public float jumpForce = 18.0f;
    public bool isRight = true;
    void Start()
    {
        if (body == null) body = GetComponent<Rigidbody>();
        if (anim == null) anim = GetComponent<Animator>();
        xscale = transform.localScale.x;
    }

    private float xscale;
    public void Left(float speed)
    {
        body.MovePosition(new Vector2(body.position.x - speed * Time.fixedDeltaTime, body.position.y));
        Look(false);
    }

    public void Right(float speed)
    {
        body.MovePosition(new Vector2(body.position.x + speed * Time.fixedDeltaTime, body.position.y));
        Look(true);        
    }

    public void Move(float speed, bool right = true)
    {
        if (right) Right(speed);
        else Left(speed);
        anim.SetBool("isRunning", true);
    }

    public void Look(bool right = true)
    {
        if (right)
        {
            transform.localScale = new Vector3(xscale, transform.localScale.y, transform.localScale.z);
            isRight = true;
        }
        else
        {
            transform.localScale = new Vector3(-xscale, transform.localScale.y, transform.localScale.z);
            isRight = false;
        }
    }

    public void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
}
