using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemyAI : MonoBehaviour
{
    public float sightLength = 8.0f;
    public float sightAngle = 22.5f;
    public testEnemyMoves testEnemyMoves;
    public GameObject player;
    public AI_STATE status = AI_STATE.PATROL;
    // Start is called before the first frame update
    void Start()
    {
        if (testEnemyMoves == null) testEnemyMoves = GetComponent<testEnemyMoves>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Think();
    }

    private int alertTime = 300;
    private int currentAlertTime = 0;
    public void Think()
    {
        if (Sight(true))
        {
            status = AI_STATE.ALERT;
            currentAlertTime = 0;
        }

        if (status == AI_STATE.IDLE)
        {
            if (Random.Range(0, 180) == 1)
            {
                testEnemyMoves.isRight = !testEnemyMoves.isRight;
                status = AI_STATE.PATROL;
            }
        }
        else if (status == AI_STATE.PATROL)
        {        
            if (GroundCheck(true))
            {
                testEnemyMoves.Move(testEnemyMoves.patrolSpeed, testEnemyMoves.isRight);
            }
            else
            {
                status = AI_STATE.IDLE;
            }
        }
        else if (status == AI_STATE.ALERT)
        {

            testEnemyMoves.isRight = PlayerDirection();
            if (GroundCheck(true))
            {
                testEnemyMoves.Move(testEnemyMoves.speed, testEnemyMoves.isRight);
            }
            currentAlertTime += 1;
            if (currentAlertTime >= alertTime)
            {
                status = AI_STATE.IDLE;
                currentAlertTime = 0;
            }
        }
    }

    public bool Sight(bool debug = false)
    {
        if (!FindPlayer())
        {
            return false;
        }

        Rigidbody2D body = testEnemyMoves.body;
        RaycastHit2D hit2D = Physics2D.Raycast(body.position, (Vector2)player.transform.position - body.position, sightLength);
        Vector2 direction = (testEnemyMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
        float angleb = Vector2.Angle(direction, (Vector2)player.transform.position - body.position);
        if (debug)
        {
            Debug.DrawRay(body.position, ((Vector2)player.transform.position - body.position).normalized * sightLength, Color.red);
            Debug.DrawRay(body.position, Quaternion.Euler(0, 0, sightAngle) * direction * sightLength, Color.green);
            Debug.DrawRay(body.position, Quaternion.Euler(0, 0, -sightAngle) * direction * sightLength, Color.green);
        }
        if (hit2D.collider && hit2D.collider.gameObject.GetComponent<PlayerController>() && angleb <= sightAngle)
        {
            return true;
        }

        return false;
    }

    public bool GroundCheck(bool debug = false)
    {
        Rigidbody2D body = testEnemyMoves.body;
        Vector2 direction = (testEnemyMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
        RaycastHit2D hit2D = Physics2D.Raycast(body.position + new Vector2(GetComponent<SpriteRenderer>().bounds.size.x/2, 0) * direction, -Vector2.up, GetComponent<SpriteRenderer>().bounds.size.y);
        if (debug)
        {
            Debug.DrawRay(body.position + new Vector2(GetComponent<SpriteRenderer>().bounds.size.x/2, 0) * direction, -Vector2.up * GetComponent<SpriteRenderer>().bounds.size.y, Color.yellow);
        }
        if (hit2D.collider && hit2D.collider.gameObject.CompareTag("Mapping"))
        {
            return true;
        }
        
        return false;
    }

    public bool PlayerDirection()
    {
        if (!FindPlayer())
        {
            return false;
        }

        float angleb = Vector2.Angle(transform.right, (Vector2)player.transform.position - testEnemyMoves.body.position);
        if (angleb <= 90)
        {
            return true;
        }
        return false;
    }

    public bool FindPlayer()
    {
        if (player == null) 
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            if (player == null) return false;
            else return true;
        }
        return true;
    }

    public enum AI_STATE
    {
        IDLE = 0,
        PATROL = 1,
        ALERT = 2
    }
}
