using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool active = true;
    public bool isJumping = false;
    public bool isRunning = false;
    public int rotation = 0;
    public float speed = 6.0f;
    public float jumpForce = 6.0f;
    public int life = 100;
    public int maxLife = 100;
    private Animator anim;
    private Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per physics tick
    void FixedUpdate()
    {
        if (!active)
        {
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            r.position = new Vector3(r.position.x + speed * Time.fixedDeltaTime, r.position.y, r.position.z);
            transform.localScale = new Vector3(rotation, transform.localScale.y, transform.localScale.z);
            rotation = 1;
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            r.position = new Vector3(r.position.x - speed * Time.fixedDeltaTime, r.position.y, r.position.z);
            transform.localScale = new Vector3(rotation, transform.localScale.y, transform.localScale.z);
            rotation = -1;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                r.velocity = new Vector3(r.velocity.x, jumpForce, r.velocity.z);
                isJumping = true;
            }
        }

        anim.SetBool("isRunning", isRunning);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Mapping") && r.velocity.y == 0 )
        {
            isJumping = false;
        }
    }

    public delegate void damageEvent();
    public event damageEvent OnDamage;

    public void Damage(int damage)
    {
        life = Mathf.Min(maxLife, Mathf.Max(0, life - damage));
        if (OnDamage != null)
        {
            OnDamage();
        }
    }
}
