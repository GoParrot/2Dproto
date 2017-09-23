using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    public int LeveltoLoad;
    private GameMaster gm;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E], to Enter");
           
            if (Input.GetKeyDown("e"))
            {
                Application.LoadLevel(LeveltoLoad);
            }
        }
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                Application.LoadLevel(LeveltoLoad);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        gm.InputText.text = (" ");
    }
} 
