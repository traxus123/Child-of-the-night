using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int life = 40;
    public int att = 5;
    public bool inHit = false;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        
    }

    public void Damage(int damage)
    {
        life = life - damage;
        if (life <= 0)
        {
            //transform.gameObject.tag = "Dead";
            //anim.SetBool("Dead", true);
            Debug.Log(damage + " sur enemie");
            gameObject.SetActive(false);
        }
        else
        {
            anim.SetBool("inHit", true);
        }
    }
}
