using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject PauseUI;
    private bool paused = true;
	// Use this for initialization
	
	
	// Update is called once per frame
	 public void Pause () {

        if (paused==true)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        
	}
       public void Resume(){

           if (paused == true)
           {
               PauseUI.SetActive(false);
               Time.timeScale = 1;
           }
        }

       public void Restart()
       {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }
       public void MainMenu()
       {
           Application.LoadLevel(0);
       }
       public void Quit()
       {
           Application.Quit();
       }
}
