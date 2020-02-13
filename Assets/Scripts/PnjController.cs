using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjController : MonoBehaviour
{

    public int life = 10;
    public bool inHit = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Damage(int damage)
    {
        life = life - damage;
        if (life <= 0)
        {
            //transform.gameObject.tag = "Dead";
            //anim.SetBool("Dead", true);
            Debug.Log(damage + " sur PNJ");
            gameObject.active = false;
        }
        else
        {
            //anim.SetBool("inHit", true);
        }
    }
}
