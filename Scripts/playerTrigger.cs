using UnityEngine;
using System.Collections;

public class playerTrigger : MonoBehaviour {

    bool isTrigger;
    public int dmg = 20;

    void onTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger != true && col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Player", dmg);
        }

    }
}
