using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;


public class Username : MonoBehaviour
{
    public bool a;

    private StreamWriter fileWriter;

    public string username;
    public GameObject inputField, deletion;
    public GameObject button;

    public GameObject leftBackground, leftCheckmark, leftLabel;
    public GameObject rightBackground, rightCheckmark, rightLabel;

    public GameObject buttonHand, textHand;

    public void StoreUsername()
    {
        username = inputField.GetComponent<Text>().text;
        GameObject.Find("Main Camera").GetComponent<Calibration>().user1 = username;
        Destroy(deletion);
        Destroy(button);

        leftCheckmark.GetComponent<Image>().enabled = true;
        leftBackground.GetComponent<Image>().enabled = true;
        leftLabel.GetComponent<Text>().enabled = true;

        rightCheckmark.GetComponent<Image>().enabled = true;
        rightBackground.GetComponent<Image>().enabled = true;
        rightLabel.GetComponent<Text>().enabled = true;

        buttonHand.GetComponent<Image>().enabled = true;
        textHand.GetComponent<Text>().enabled = true;

        //Save the user
        CreateFileUser(username);
    }

    public void CreateFileUser(string user)
    {
        string path = Directory.GetCurrentDirectory();
        path += @"\Users\" + user + @"\Highscores";

        if (!Directory.Exists(path))
        {
            //Directory.CreateDirectory(@"\Users\" + user);
            Directory.CreateDirectory(path);

            StartInfo(path, "Grab");
            StartInfo(path, "Calculator");
            StartInfo(path, "MemoryGame");
        }
    }

    public void StartInfo(string path, string task)
    {
        string line = "HighScore:0";
        fileWriter = File.CreateText(path + @"\" + task + ".txt");
        fileWriter.WriteLine(line);
        fileWriter.Close();
    }
}
