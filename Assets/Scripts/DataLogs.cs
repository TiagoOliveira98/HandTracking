using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class DataLogs : MonoBehaviour
{
    private StreamWriter fileWriter;
    float time;
    void Start()
    {
        string path = Directory.GetCurrentDirectory();
        path += @"\Debugs\Debugs(0).txt";

        Directory.CreateDirectory("Debugs");
        if (!File.Exists(path))
        {
            fileWriter = File.CreateText(path);
        }
        else
        {
            for (int i = 1; i < 200; i++)
            {
                path = Directory.GetCurrentDirectory();
                path += @"\Debugs\Debugs" + "(" + i.ToString() + ").txt";
                if (!File.Exists(path))
                {
                    fileWriter = File.CreateText(path);
                    break;
                }
            }
        }

        time = 0;
    }
    private void Update()
    {
        time += Time.unscaledDeltaTime;
    }

    public void Log(string line, bool showTimeStamp = true)
    {
            if (showTimeStamp)
            {
                fileWriter.WriteLine(line + " at " + time.ToString("F2") + " seconds of game");
            }
            else
            {
                fileWriter.WriteLine(line);
            }
    }

    public void CloseFile()
    {
            fileWriter.Close();
    }

}
