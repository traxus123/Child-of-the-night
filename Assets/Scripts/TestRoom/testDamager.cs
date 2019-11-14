﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDamager : MonoBehaviour
{
    public int damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController p = col.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            p.Damage(damage);
        }
    }
}
