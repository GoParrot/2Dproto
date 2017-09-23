using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    void onTriggerEnter2D(Collider2D col)
    {


        if (col.isTrigger != true)
        {


            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(1);
            }
            Destroy(gameObject);
        }
    }   

}

