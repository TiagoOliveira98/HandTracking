using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class DataLogs : MonoBehaviour
{
    private StreamWriter fileWriter;
    float time;

    long timestamp;

    string line;
    public string ev;

    public GameObject WRIST, THUMB_CMC, THUMB_MCP, THUMB_IP, THUMB_TIP,
        INDEX_FINGER_MCP, INDEX_FINGER_PIP, INDEX_FINGER_DIP, INDEX_FINGER_TIP,
        MIDDLE_FINGER_MCP, MIDDLE_FINGER_PIP, MIDDLE_FINGER_DIP, MIDDLE_FINGER_TIP, RING_FINGER_MCP,
        RING_FINGER_PIP, RING_FINGER_DIP, RING_FINGER_TIP, PINKY_MCP, PINKY_PIP, PINKY_DIP, PINKY_TIP;

    public GameObject WRIST2, THUMB_CMC2, THUMB_MCP2, THUMB_IP2, THUMB_TIP2,
        INDEX_FINGER_MCP2, INDEX_FINGER_PIP2, INDEX_FINGER_DIP2, INDEX_FINGER_TIP2,
        MIDDLE_FINGER_MCP2, MIDDLE_FINGER_PIP2, MIDDLE_FINGER_DIP2, MIDDLE_FINGER_TIP2, RING_FINGER_MCP2,
        RING_FINGER_PIP2, RING_FINGER_DIP2, RING_FINGER_TIP2, PINKY_MCP2, PINKY_PIP2, PINKY_DIP2, PINKY_TIP2;

    void Start()
    {
        var timePass = (System.DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
        timestamp = (long)timePass.TotalSeconds;
        string path = Directory.GetCurrentDirectory();
        //path += @"\DataLogging\JointData\Logs(0).csv";
        path += @"\DataLogging\JointData\Logs" + "(" + timestamp +  ")_" + Calibration.user + ".csv";
        Directory.CreateDirectory(@"DataLogging\JointData");
        fileWriter = File.CreateText(path);


        /* if (!File.Exists(path))
        {
            fileWriter = File.CreateText(path);
        }
        else
        {
            for (int i = 1; i < 200; i++)
            {
                path = Directory.GetCurrentDirectory();
                path += @"\DataLogging\JointData\Logs" + "(" + i.ToString() + ").csv";
                if (!File.Exists(path))
                {
                    fileWriter = File.CreateText(path);
                    break;
                }
            }
        }*/
        ev = "Dnf";
        time = 0;
    }
    private void FixedUpdate()
    {
        //time += Time.unscaledDeltaTime;
        time += Time.fixedDeltaTime;
        //Testing to keep the frequency of loggin constant
        ////Logging Data
        if (GameObject.Find("BucketBlue") != null /*&& wristRightX != WRIST.transform.position.x && wristLeftX != WRIST2.transform.position.x*/)
        {
            //Right Hand
            line += WRIST.transform.position.x.ToString().Replace(",", ".") + " " + WRIST.transform.position.y.ToString().Replace(",", ".") + " " + WRIST.transform.position.z.ToString().Replace(",", ".");

            line += "," + THUMB_CMC.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_CMC.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_CMC.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_MCP.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_MCP.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_MCP.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_IP.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_IP.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_IP.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_TIP.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_TIP.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_TIP.transform.position.z.ToString().Replace(",", ".");

            line += "," + INDEX_FINGER_MCP.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_MCP.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_MCP.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_PIP.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_PIP.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_PIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_DIP.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_DIP.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_DIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_TIP.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_TIP.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_TIP.transform.position.z.ToString().Replace(",", ".");

            line += "," + MIDDLE_FINGER_MCP.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_MCP.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_MCP.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_PIP.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_PIP.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_PIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_DIP.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_DIP.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_DIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_TIP.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_TIP.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_TIP.transform.position.z.ToString().Replace(",", ".");

            line += "," + RING_FINGER_MCP.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_MCP.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_MCP.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_PIP.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_PIP.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_PIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_DIP.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_DIP.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_DIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_TIP.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_TIP.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_TIP.transform.position.z.ToString().Replace(",", ".");

            line += "," + PINKY_MCP.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_MCP.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_MCP.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_PIP.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_PIP.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_PIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_DIP.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_DIP.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_DIP.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_TIP.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_TIP.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_TIP.transform.position.z.ToString().Replace(",", ".");

            //Left Hand
            line += "," + WRIST2.transform.position.x.ToString().Replace(",", ".") + " " + WRIST2.transform.position.y.ToString().Replace(",", ".") + " " + WRIST2.transform.position.z.ToString().Replace(",", ".");

            line += "," + THUMB_CMC2.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_CMC2.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_CMC2.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_MCP2.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_MCP2.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_MCP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_IP2.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_IP2.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_IP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + THUMB_TIP2.transform.position.x.ToString().Replace(",", ".") + " " + THUMB_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + THUMB_TIP2.transform.position.z.ToString().Replace(",", ".");

            line += "," + INDEX_FINGER_MCP2.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_MCP2.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_MCP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_PIP2.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_PIP2.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_PIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_DIP2.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_DIP2.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_DIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + INDEX_FINGER_TIP2.transform.position.x.ToString().Replace(",", ".") + " " + INDEX_FINGER_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + INDEX_FINGER_TIP2.transform.position.z.ToString().Replace(",", ".");

            line += "," + MIDDLE_FINGER_MCP2.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_MCP2.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_MCP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_PIP2.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_PIP2.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_PIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_DIP2.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_DIP2.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_DIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + MIDDLE_FINGER_TIP2.transform.position.x.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + MIDDLE_FINGER_TIP2.transform.position.z.ToString().Replace(",", ".");

            line += "," + RING_FINGER_MCP2.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_MCP2.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_MCP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_PIP2.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_PIP2.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_PIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_DIP2.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_DIP2.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_DIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + RING_FINGER_TIP2.transform.position.x.ToString().Replace(",", ".") + " " + RING_FINGER_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + RING_FINGER_TIP2.transform.position.z.ToString().Replace(",", ".");

            line += "," + PINKY_MCP2.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_MCP2.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_MCP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_PIP2.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_PIP2.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_PIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_DIP2.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_DIP2.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_DIP2.transform.position.z.ToString().Replace(",", ".");
            line += "," + PINKY_TIP2.transform.position.x.ToString().Replace(",", ".") + " " + PINKY_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_TIP2.transform.position.z.ToString().Replace(",", ".") /*+ "\n"*/;

            //log.GetComponent<DataLogs>().Log(line);
            Log(line, ev, true);
            ev = "DnF";
            line = "";
        }

        /*wristRightX = WRIST.transform.position.x;
        wristLeftX = WRIST2.transform.position.x;*/
    }

    public void Log(string line, string eve = "DnF", bool showTimeStamp = true)
    {
            if (showTimeStamp)
            {
                //fileWriter.WriteLine(line + " at " + time.ToString("F2") + " seconds of game");
                //line += ";" + time.ToString("F3");
                //fileWriter.WriteLine(line);
                //fileWriter.WriteLine(line + ";" + time.ToString("F3"));
                fileWriter.WriteLine($"{line},{time.ToString("F3").Replace(",",".")},{eve}");
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
