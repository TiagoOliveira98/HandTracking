using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using System;
using System.IO;

public class LoadGrab : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGrabGame()
    {
        GameObject high = GameObject.Find("HighScore");
        if (high != null)
        {
            if (high.GetComponent<HighScore>().change == true && SceneManager.GetActiveScene().name != "main")
            {
                StreamWriter writer = new StreamWriter(high.GetComponent<HighScore>().path, false);
                writer.WriteLine("Highscore:" + high.GetComponent<HighScore>().num);
                writer.Close();
            }
        }

        if (Calibration.hand == "L")
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
