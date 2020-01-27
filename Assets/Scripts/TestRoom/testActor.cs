using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testActor : MonoBehaviour
{
    public int life = 100;
    public int maxLife = 100;
    public int xp = 1;
    public int att = 1;
    public int def = 1;
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void damageEvent();
    public event damageEvent OnDamage;
    public void Damage(int damage)
    {
        if (OnDamage != null)
        {
            OnDamage();
        }
        else
        {
            if (alive)
            {
                damage = Mathf.Min(1, damage - def);
                life = Mathf.Min(maxLife, Mathf.Max(0, life - damage));
                if (life <= 0)
                {
                    alive = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
