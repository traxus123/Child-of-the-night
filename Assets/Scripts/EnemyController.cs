using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int life = 40;
    public int att = 5;
    public bool inHit = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player") && life >= 0)
        {
            Debug.Log("Enemy");
            inHit = true;
        }
        if (!anim.GetBool("inHit"))
        {
            inHit = false;
        }
        
        anim.SetBool("inHit", inHit);
    }

    public void Damage(int damage)
    {
        life = life - damage;
        if (life <= 0)
        {
            //transform.gameObject.tag = "Dead";
            //anim.SetBool("Dead", true);
            Debug.Log(att + " sur enemie");
            gameObject.active = false;
        }
    }
}
