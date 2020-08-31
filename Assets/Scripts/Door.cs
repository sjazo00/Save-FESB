using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    public int LevelToLoad;
    private GameMaster gm;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

    }

    

   public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
                PlayerPrefs.SetInt("Score", gm.score);
                Application.LoadLevel(LevelToLoad);   
        }

    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
                PlayerPrefs.SetInt("Score", gm.score);
                Application.LoadLevel(LevelToLoad);
        }

    }

    
}
