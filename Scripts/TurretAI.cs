using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour
{
    //int
    public int curHealth;
    public int maxHealth;

    //float
    public float distance;
    public float wakeRange;
    public float shootInterval ;
    public float bulletsSpeed = 10;
    public float bulletTimer;
   

    //booleans
    public bool awake = false;
    public bool lookingRight = true;

    //References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft;
    public Transform shootPointRight;
    public GameMaster gm;

    //budjenje turenta
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        if (distance < wakeRange)
        {

            awake = true;
            Attack(lookingRight);

        }
    }
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }
        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


   




    // proveravanje range-a
    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            awake = true;
        }
        if (distance > wakeRange)
        {
            awake = false;
        }


    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();


            if (!attackingRight)
            {
               
                GameObject bulletClone;

                //pokretanje  metka
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletsSpeed;

                bulletTimer = 0;

            }
            if(attackingRight)
            {
                GameObject bulletClone;  
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletsSpeed;

                bulletTimer = 0;
            }
        }
    }

        public void Damage(int damage){
            curHealth -= damage;
            gameObject.GetComponent<Animation>().Play("Player_RedFlash");

        }
    }










    

