using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0;
    private float attackCd = 0.3f;

    public Collider2D attackTriger;

    private Animator anim;


    void Awake() 
    {
        anim = gameObject.GetComponent<Animator>( );
        attackTriger.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("f") && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;

            attackTriger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTriger.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
    }
}
