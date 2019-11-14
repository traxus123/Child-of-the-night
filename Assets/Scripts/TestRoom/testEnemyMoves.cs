using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemyMoves : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 5.0f;
    public float patrolSpeed = 3.0f;
    public float jumpForce = 18.0f;
    public bool isRight = true;
    void Start()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
    }

    public void Left(float speed)
    {
        body.MovePosition(new Vector2(body.position.x - speed * Time.fixedDeltaTime, body.position.y));
        isRight = false;
    }

    public void Right(float speed)
    {
        body.MovePosition(new Vector2(body.position.x + speed * Time.fixedDeltaTime, body.position.y));
        isRight = true;
    }

    public void Move(float speed, bool right = true)
    {
        if (right) Right(speed);
        else Left(speed);
    }

    public void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
}
