using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool active = true;
    public bool isJumping = false;
    public bool isRunning = false;
    public bool inDialogue = false;
    public bool inAttaque = false;
    public bool inAttBonus = false;
    public bool hasAtt = false;
    public bool inDrink = false;
    public int rotation = 1;
    public float speed = 6.0f;
    public float jumpForce = 6.0f;
    public int life = 100;
    public int maxLife = 100;
    public int playerXp = 0;
    public int maxXp = 100;
    public int level = 1;
    public int att = 1;
    public int def = 1;
    private Animator anim;
    private Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        OnDamage += HitAnim;
    }

    void Update()
    {
        if (Time.frameCount % 40 == 0)
        {
            hasAtt = false;
        }
    }
    void LateUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x * rotation, transform.localScale.y, transform.localScale.z);
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
        if (!inDialogue)
        {
            if (Input.GetKey(KeyCode.P))
            {
                Damage(2);
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
                    anim.SetBool("isJumping", isJumping);
                }
            }

            if (!Input.GetKey(KeyCode.K) && inAttaque)
            {
                inAttaque = false;

                //transform.localScale = new Vector3(rotation, transform.localScale.y, transform.localScale.z / 2);

            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                inAttBonus = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                inAttBonus = false;
            }
            if (!inAttaque)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    inAttaque = true;

                    if (inAttBonus)
                    {
                        anim.SetBool("inAttBonus", inAttaque);
                        //transform.localScale = new Vector3(rotation, transform.localScale.y, transform.localScale.z * 2);
                    }
                    else
                    {
                        anim.SetBool("inAttaque", inAttaque);
                        if(rotation == 1)
                        {
                            r.position = new Vector3(r.position.x + 21.0f * Time.fixedDeltaTime, r.position.y, r.position.z);
                        }
                        else if(rotation == -1)
                        {
                            r.position = new Vector3(r.position.x - 21.0f * Time.fixedDeltaTime, r.position.y, r.position.z);
                        }
                        //transform.localScale = new Vector3(rotation, transform.localScale.y, transform.localScale.z * 2);
                    }
                }
            }
        }

        if (isJumping)
        {
            RaycastHit ground;
            if (Physics.Linecast(transform.position - new Vector3(0, transform.localScale.y/2-0.1f, 0), transform.position - new Vector3(0, transform.localScale.y/2+0.1f, 0), out ground))
            {
                if (ground.transform.gameObject.CompareTag("Mapping"))
                {
                    isJumping = false;
                    anim.SetBool("isJumping", isJumping);
                }
            }
            
        }
        anim.SetBool("isRunning", isRunning);
    }

    void OnTriggerStay(Collider col)
    {
        
        if (inAttaque && !hasAtt)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                
                if (inAttBonus)
                {
                    Damage(2);

                    col.gameObject.GetComponent<EnemyController>().Damage(att * 2);
                }
                else
                {
                    col.gameObject.GetComponent<EnemyController>().Damage(att);
                }
                hasAtt = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            inDrink = true; 
            anim.SetBool("inDrink", inDrink);
            if (col.gameObject.CompareTag("Enemy"))
            {
                //Dégats Enemie
                col.gameObject.GetComponent<EnemyController>().Damage(att);
                Heal(att);
            }
            if (col.gameObject.CompareTag("NPC"))
            {
                //Dégats NPC
<<<<<<< HEAD
                col.gameObject.GetComponent<PnjController>().Damage(att);
                Heal(att);
=======
                Damage(-att);
>>>>>>> 43e2ac490988a8ebcbc470c9ee02f742895bb961
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            inDrink = false;
            anim.SetBool("inDrink", inDrink);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (col.gameObject.CompareTag("NPC"))
            {
                if (inDialogue)
                {
                    FindObjectOfType<DialogueManager>().displayNextSentence();
                }
                else if (col.gameObject.GetComponentInChildren<DialogueInstance>() != null && !inDialogue)
                {
                    inDialogue = true;
                    Dialogue dialogue = col.gameObject.GetComponentInChildren<DialogueInstance>().dialogue;
                    FindObjectOfType<DialogueManager>().startDialogue(dialogue);
                    Debug.Log("Dialogue test");
                }
            }
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
        if(life <= 0)
        {
            life = 0;
            anim.SetBool("Dead", true);
        }
    }

    public delegate void healEvent();
    public event healEvent onHeal;

    public void Heal(int heal)
    {
        life = Mathf.Min(maxLife, Mathf.Max(0, life + heal));
        if (onHeal != null)
        {
            onHeal();
        }
    }

    public void HitAnim()
    {
        anim.SetBool("isHit", true);
        r.velocity = new Vector3(-rotation * 3.0f, r.velocity.y + 0.1f, r.velocity.z);
    }

    public delegate void xpEvent();
    public event xpEvent OnXp;

    public void Xp(int xp)
    {
        playerXp = playerXp + xp;
        if (playerXp >= maxXp)
        {
            playerXp = playerXp - maxXp;
            level++;
        }
        if (OnXp != null)
        {
            OnXp();
        }
    }

}
