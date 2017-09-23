using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;

    public int curHealth;
    public int maxHealth = 100;


    private Rigidbody2D rb2d;
    private Animator anim;
    private GameMaster gm;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;




    // Use this for initialization
    void Start() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        

        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update() {

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //Levo
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        //Desno
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        //double jump, jump & wallslide
      if (Input.GetButtonDown("Jump") && !wallSliding && grounded)
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }

        }

        if (wallCheck == false || grounded) 
        {
            wallSliding = false;
        }



    }


    void HandleWallSliding()
    {

        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1, 3)*jumpPower);
            }
            else
            {
                rb2d.AddForce(new Vector2(1, 3) * jumpPower);
            }
        }

    }

    void FixedUpdate() {


        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.x *= 0.75f;
        easeVelocity.z = 0.0f;

        float h = Input.GetAxis("Horizontal");


        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed / 2) * h);
        }
        

       


        //Ogranicavanje brzine kretanja

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
       
    }
    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;

        gameObject.GetComponent<Animation>().Play("Player_RedFlash");


    }
    // reakcija na bodlje, odbacivanje  i sl
    public IEnumerator Knockback (float knockDur, float knockbackPwr, Vector4 knockbackDir )
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while (knockDur > timer) 
        {
            timer += Time.deltaTime;

            rb2d.AddForce(new Vector4(knockbackDir.x * -5, knockbackDir.y * -5 * knockbackPwr, transform.position.z, transform.position.y));
        }
        yield return 0;
    }


    void onTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
        }
    }
}
