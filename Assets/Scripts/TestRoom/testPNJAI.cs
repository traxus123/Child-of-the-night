﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPNJAI : MonoBehaviour
{
    public float sightLength = 4.0f;
    public float sightAngle = 180f;
    public testPNJMoves testPNJMoves;
    public GameObject player;
    public AI_STATE status = AI_STATE.PATROL;
    // Start is called before the first frame update
    void Start()
    {
        if (testPNJMoves == null) testPNJMoves = GetComponent<testPNJMoves>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Think();
    }

    private int alertTime = 300;
    private int currentAlertTime = 0;

    // Valeur pour un all-sight vs alert sight en cas de poursuite
    // private float patrolAngle = 180f;
    // private float alertAngle = 22.5f;
    // private float patrolLength = 4.0f;
    // private float alertLength = 8.0f;
    public void Think()
    {
        if (status == AI_STATE.IDLE)
        {
            if (Sight(true))
            {
                testPNJMoves.Look(PlayerDirection());
            }
            if (Random.Range(0, 300) == 1)
            {
                testPNJMoves.Look((Random.Range(0, 3) <= 1));
                status = AI_STATE.PATROL;
            }
        }
        else if (status == AI_STATE.PATROL)
        {        
            if (GroundCheck(true))
            {
                testPNJMoves.Move(testPNJMoves.patrolSpeed, testPNJMoves.isRight);
            }
            else
            {
                status = AI_STATE.IDLE;
                testPNJMoves.anim.SetBool("isRunning", false);
            }

            if (Random.Range(0, 120) == 1)
            {
                status = AI_STATE.IDLE;
                testPNJMoves.anim.SetBool("isRunning", false);
            }
        }
        // PAS D'ALERTE SUR LES PNJ POUR L'INSTANT MAIS COMPORTEMENT SIMILAIRE AU COMBAT CLASSIQUE !
        else if (status == AI_STATE.ALERT)
        {
            testPNJMoves.Look(PlayerDirection());
            if (GroundCheck(true))
            {
                testPNJMoves.Move(testPNJMoves.speed, testPNJMoves.isRight);
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

        Rigidbody body = testPNJMoves.body;
        RaycastHit hit;
        Vector2 direction = (testPNJMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
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
        Rigidbody body = testPNJMoves.body;
        Vector2 direction = (testPNJMoves.isRight ? (Vector2)transform.right : -(Vector2)transform.right);
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

        float angleb = Vector2.Angle(transform.right, (Vector2)player.transform.position - (Vector2)testPNJMoves.body.position);
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