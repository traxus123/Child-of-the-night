using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemyMoves : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 5.0f;
    public float patrolSpeed = 3.0f;
    public float jumpForce = 18.0f;
    public bool isRight = true;
    public bool isRunning = false;
    public bool isAttacking = false;
    void Start()
    {
        if (body == null) body = GetComponent<Rigidbody>();
    }

    // TO DO ATTAQUE ?

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
        isRunning = true;
    }

    public void Look(bool right = true)
    {
        if (right) {
            isRight = true;
        } else {
            isRight = false;
        }
    }

    void LateUpdate()
    {
        if (isRight) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
}
