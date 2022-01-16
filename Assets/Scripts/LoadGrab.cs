using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGrab : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGrabGame()
    {
        if(Calibration.hand == "L")
        {
            SceneManager.LoadScene("GrabLeft");
        }
        else if(Calibration.hand == "R")
        {
            SceneManager.LoadScene("Grab");
        }
        else if(Calibration.hand == "LR")
        {
            if (Random.Range(1, 3) == 1)
            {
            SceneManager.LoadScene("Grab");
            }
            else
            {
                SceneManager.LoadScene("GrabLeft");
            }
        }
        ////SceneManager.LoadScene("Level2");
    }
}
