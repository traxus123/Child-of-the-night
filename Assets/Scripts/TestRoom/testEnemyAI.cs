using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemyAI : MonoBehaviour
{
    public float sightLength = 8.0f;
    public float sightAngle = 22.5f;
    public testEnemyMoves testEnemyMoves;
    public EnemyController actor;
    public GameObject player;
    public AI_STATE status = AI_STATE.PATROL;
    // Start is called before the first frame update
    void Start()
    {
        if (testEnemyMoves == null) testEnemyMoves = GetComponent<testEnemyMoves>();
        if (actor == null) actor = GetComponent<EnemyController>();
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
                testEnemyMoves.isRunning = false;
            }
        }
        else if (status == AI_STATE.ALERT)
        {

            testEnemyMoves.isRight = PlayerDirection();
            if (testEnemyMoves.isAttacking == false)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= 1.5)
                {
                    testEnemyMoves.isAttacking = true; // Remplacer par l'appel de l'ATTAQUE !!
                    actor.anim.SetBool("isAttacking", testEnemyMoves.isAttacking);
                }
                else if (GroundCheck(true))
                {
                    testEnemyMoves.Move(testEnemyMoves.speed, testEnemyMoves.isRight);
                }
            }
            else
            {
                if (actor.anim.GetBool("isAttacking") == false)
                {
                    testEnemyMoves.isAttacking = false;
                }
            }
            currentAlertTime += 1;
            if (currentAlertTime >= alertTime)
            {
                status = AI_STATE.IDLE;
                currentAlertTime = 0;
            }
        }
        actor.anim.SetBool("isRunning", testEnemyMoves.isRunning);
    }

    public bool Sight(bool debug = false)
    {
        if (!FindPlayer())
        {
            return false;
        }

        Rigidbody body = testEnemyMoves.body;
        RaycastHit hit;
        Vector2 direction = (testEnemyMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
        float angleb = Vector2.Angle(direction, player.transform.position - body.position);
        if (Physics.Raycast(body.position, player.transform.position - body.position, out hit, sightLength))
        {
            if (debug)
            {
                Debug.DrawRay(body.position, (player.transform.position - body.position).normalized * sightLength, Color.red);
                Debug.DrawRay(body.position, Quaternion.Euler(0, 0, sightAngle) * direction * sightLength, Color.green);
                Debug.DrawRay(body.position, Quaternion.Euler(0, 0, -sightAngle) * direction * sightLength, Color.green);
            }
            if (hit.collider && hit.collider.gameObject.GetComponent<PlayerController>() && angleb <= sightAngle)
            {
                return true;
            }
        }
        return false;
    }

    public bool GroundCheck(bool debug = false)
    {
        Rigidbody body = testEnemyMoves.body;
        Vector2 direction = (testEnemyMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
        RaycastHit hit;
        if (Physics.Raycast((Vector2)body.position + new Vector2(transform.localScale.x/2, 0) * direction, -Vector2.up, out hit, transform.localScale.y))
        {
            if (debug)
            {
                Debug.DrawRay((Vector2)body.position + new Vector2(transform.localScale.x/2, 0) * direction, -Vector2.up * hit.distance, Color.yellow);
            }
            if (hit.collider && hit.collider.gameObject.CompareTag("Mapping"))
            {
                return true;
            }
        }
        return false;
    }

    public bool PlayerDirection()
    {
        if (!FindPlayer())
        {
            return false;
        }

        float angleb = Vector2.Angle(transform.right, (Vector2)player.transform.position - (Vector2)testEnemyMoves.body.position);
        if (angleb < 90)
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
