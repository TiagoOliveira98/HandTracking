using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.IO;

public class LoadCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadCalculatorGame()
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
        SceneManager.LoadScene("Calculator");
    } 
}
