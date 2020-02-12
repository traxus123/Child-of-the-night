using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int life = 10;
    public int att = 1;
    public bool inHit = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inHit = true;
            Damage(2);
        }
        if (!anim.GetBool("inHit"))
        {
            inHit = false;
        }
        if(life <= 0)
        {
            transform.gameObject.tag = "D_Enemy";
            anim.SetBool("Dead", true);
        }
        anim.SetBool("inHit", inHit);
    }


    public delegate void damageEvent();
    public event damageEvent OnDamage;

    public void Damage(int damage)
    {
        life = life - damage;
        if (OnDamage != null)
        {
            OnDamage();
        }
    }
}
