using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool Paused;
    public GameObject pauseImage;
    void Start()
    {
        Paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (!Paused){
            Time.timeScale = 0;
            Paused = true;
            Debug.Log("Paused");
                pauseImage.SetActive(true);
            }
            else if (Paused){
            Time.timeScale = 1;
            Paused = false;
                pauseImage.SetActive(false);
                Debug.Log("Unpaused");
            }
        }
        
    }

    
}
