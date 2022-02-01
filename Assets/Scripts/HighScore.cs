using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.IO;

public class HighScore : MonoBehaviour
{
    public GameObject cameras;
    public Text high;

    Points points;
    public int num;

    public string[] x;

    public bool change;

    public string path;

    public string line;

    // Start is called before the first frame update
    void Start()
    {
        change = false;

        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();

        //Read from the file the Highscore recorded
        path = Directory.GetCurrentDirectory();
        if(SceneManager.GetActiveScene().name == "GrabLeft")
            path += @"\Users\" + Calibration.user +  @"\Highscores\Grab.txt";
        else
            path += @"\Users\" + Calibration.user + @"\Highscores\" + SceneManager.GetActiveScene().name + ".txt";

        StreamReader reader = new StreamReader(path);
        line = reader.ReadLine();
        reader.Close();

        x = line.Split(':');
        num = int.Parse(x[1]);
    }

    // Update is called once per frame
    void Update()
    {
        high.text = "HighScore: " + num ;

        if( points.point > num )
        {
            num = points.point;
            //Write in the file
            change = true;
        }
    }

    private void OnApplicationQuit()
    {
        if(change)
        {
            //Debug.Log(path);
            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine("Highscore:" + num);
            writer.Close();
        }
    }
}
