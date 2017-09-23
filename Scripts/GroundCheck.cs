using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{

    private Player player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger)
        {
            player.grounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger)
        {
            player.grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.isTrigger)
        {
            player.grounded = false;
        }
    }
}