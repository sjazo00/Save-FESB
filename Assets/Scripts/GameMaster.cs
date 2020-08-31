using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public int score;
    public Text pointsText;
    public Text InputText;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            if (SceneManager.GetActiveScene().name == "Floor1")
            {
                PlayerPrefs.DeleteKey("Score");
                score = 0;
            }
            else
            {
                score = PlayerPrefs.GetInt("Score");

            }

        }
        
    }

    void Update()
    {
        pointsText.text = ("Points: " + score);


    }
}
