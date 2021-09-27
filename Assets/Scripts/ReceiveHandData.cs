/*
    @author: Thanos
    -----------------------
    UDP-Receive (from python client)
    -----------------------
    // [url]https://google.github.io/mediapipe/solutions/hands[/url]
   
    // > receive
    // 127.0.0.1 : 5005
   
    // send
    // nc -u 127.0.0.1 5005
 
*/
using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class ReceiveHandData : MonoBehaviour {

    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    // public string IP = "127.0.0.1"; default local
    public int port; // define > init

    /*
    //[url]https://google.github.io/mediapipe/images/mobile/hand_landmarks.png[/url]
    Dictionary<string, int> handLandmarks = new Dictionary<string, int>(){{"WRIST", 0},
        {"THUMB_CMC", 1}, {"THUMB_MCP", 2}, {"THUMB_IP", 3}, {"THUMB_TIP", 4},
        {"INDEX_FINGER_MCP", 5}, {"INDEX_FINGER_PIP", 6}, {"INDEX_FINGER_DIP", 7}, {"INDEX_FINGER_TIP", 8},
        {"MIDDLE_FINGER_MCP", 9}, {"MIDDLE_FINGER_PIP", 10}, {"MIDDLE_FINGER_DIP", 11}, {"MIDDLE_FINGER_TIP", 12},
        {"RING_FINGER_PIP", 13}, {"RING_FINGER_DIP", 14}, {"RING_FINGER_TIP", 15}, {"PINKY_MCP", 17}, {"PINKY_PIP", 18}, {"PINKY_DIP", 19}, {"PINKY_TIP", 20} };
        */

    public GameObject  WRIST, THUMB_CMC, THUMB_MCP, THUMB_IP, THUMB_TIP, 
        INDEX_FINGER_MCP, INDEX_FINGER_PIP, INDEX_FINGER_DIP, INDEX_FINGER_TIP,
        MIDDLE_FINGER_MCP, MIDDLE_FINGER_PIP, MIDDLE_FINGER_DIP, MIDDLE_FINGER_TIP, RING_FINGER_MCP,
        RING_FINGER_PIP, RING_FINGER_DIP, RING_FINGER_TIP, PINKY_MCP, PINKY_PIP, PINKY_DIP, PINKY_TIP;

    Vector3 p0pos, p1pos, p2pos, p3pos, p4pos, p5pos, p6pos, p7pos, p8pos, p9pos, p10pos, p11pos, p12pos,
         p13pos, p14pos, p15pos, p16pos, p17pos, p18pos, p19pos, p20pos;

    // Added to create another hand
    public GameObject WRIST2, THUMB_CMC2, THUMB_MCP2, THUMB_IP2, THUMB_TIP2,
        INDEX_FINGER_MCP2, INDEX_FINGER_PIP2, INDEX_FINGER_DIP2, INDEX_FINGER_TIP2,
        MIDDLE_FINGER_MCP2, MIDDLE_FINGER_PIP2, MIDDLE_FINGER_DIP2, MIDDLE_FINGER_TIP2, RING_FINGER_MCP2,
        RING_FINGER_PIP2, RING_FINGER_DIP2, RING_FINGER_TIP2, PINKY_MCP2, PINKY_PIP2, PINKY_DIP2, PINKY_TIP2;

    Vector3 p0pos2, p1pos2, p2pos2, p3pos2, p4pos2, p5pos2, p6pos2, p7pos2, p8pos2, p9pos2, p10pos2, p11pos2, p12pos2,
         p13pos2, p14pos2, p15pos2, p16pos2, p17pos2, p18pos2, p19pos2, p20pos2;

    //Vector3 grabPosition;

    public float gain = 10;

    private int mirror;

    public float[] newdata;

    float rightHandClosed, leftHandClosed;

    public GameObject GrabPointRight, GrabPointLeft;

    public GameObject cup, cup1;

    /*int check1;
    int check2;*/

    // start
    public void Start()
    {
        init();

        /*int check1 = 0;
        int check2 = 0;*/

        gain = 10;
        Mirror = -1;// set -1 for mirroring, else set 1

    //    foreach (KeyValuePair<string, float> mark in handLandmarks)
    //        Debug.LogFormat("Key: {0}, Value: {1}", mark.Key, mark.Value);

    }


    public void Update()
    {
        /*if (check1 == 0)
        {*/
        WRIST.transform.position = (p0pos * Mirror);
        WRIST.transform.eulerAngles = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        THUMB_CMC.transform.position = (p1pos /*- p0pos*/) * Mirror;
        THUMB_CMC.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_MCP.transform.position = (p2pos /*- p1pos*/) * Mirror;
        THUMB_CMC.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_IP.transform.position = (p3pos /*- p2pos*/) * Mirror;
        THUMB_IP.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_TIP.transform.position = (p4pos /*- p3pos*/) * Mirror;
        THUMB_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        INDEX_FINGER_MCP.transform.position = (p5pos /*- p0pos*/) * Mirror;
        INDEX_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_PIP.transform.position = (p6pos /*- p5pos*/) * Mirror;
        INDEX_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_DIP.transform.position = (p7pos /*- p6pos*/) * Mirror;
        INDEX_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_TIP.transform.position = (p8pos/* - p7pos*/) * Mirror;
        INDEX_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        MIDDLE_FINGER_MCP.transform.position = (p9pos /*- p0pos*/) * Mirror;
        MIDDLE_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_PIP.transform.position = (p10pos /*- p9pos*/) * Mirror;
        MIDDLE_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_DIP.transform.position = (p11pos /*- p10pos*/) * Mirror;
        MIDDLE_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_TIP.transform.position = (p12pos/* - p11pos*/) * Mirror;
        MIDDLE_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        RING_FINGER_MCP.transform.position = (p13pos /*- p0pos*/) * Mirror;
        RING_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_PIP.transform.position = (p14pos /*- p13pos*/) * Mirror;
        RING_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_DIP.transform.position = (p15pos /*-p14pos*/) * Mirror;
        RING_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_TIP.transform.position = (p16pos /*- p15pos*/) * Mirror;
        RING_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        PINKY_MCP.transform.position = (p17pos /*- p0pos*/) * Mirror;
        PINKY_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_PIP.transform.position = (p18pos /*- p17pos*/) * Mirror;
        PINKY_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_DIP.transform.position = (p19pos /*- p18pos*/) * Mirror;
        PINKY_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_TIP.transform.position = (p20pos /*-p19pos*/) * Mirror;
        PINKY_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        /*double alpha = Math.Atan2(PINKY_TIP.transform.position[0] - WRIST.transform.position[0], PINKY_TIP.transform.position[1] - WRIST.transform.position[0]);
        double beta = Math.Atan2(MIDDLE_FINGER_TIP.transform.position[0] - WRIST.transform.position[0], MIDDLE_FINGER_TIP.transform.position[2] - WRIST.transform.position[2]);
        double gamma = Math.Atan2(MIDDLE_FINGER_MCP.transform.position[1] - WRIST.transform.position[0], MIDDLE_FINGER_MCP.transform.position[2] - WRIST.transform.position[0]);

        double x = 2 * (Math.Cos(alpha) * Math.Sin(beta) * Math.Sin(gamma) - Math.Sin(alpha) * Math.Cos(gamma)) + 3 * (Math.Cos(alpha) * Math.Sin(beta) * Math.Cos(gamma) + Math.Sin(alpha) * Math.Sin(gamma));
            double y = 2 * (Math.Sin(alpha) * Math.Sin(beta) * Math.Sin(gamma) + Math.Cos(alpha) * Math.Cos(gamma)) + 3 * (Math.Sin(alpha) * Math.Sin(beta) * Math.Cos(gamma) - Math.Cos(alpha) * Math.Sin(gamma));
            double z = 2 * Math.Cos(beta) * Math.Sin(gamma) + 3 * Math.Cos(beta) * Math.Cos(gamma);

            grabPosition = new Vector3((int)x, (int)y, (int)z);

            GrabPoint.transform.position = grabPosition;*/

        float centerXright = (PINKY_TIP.transform.position[0]*0.15f + RING_FINGER_TIP.transform.position[0]*0.15f + MIDDLE_FINGER_TIP.transform.position[0]*0.25f + INDEX_FINGER_TIP.transform.position[0]*0.15f + THUMB_TIP.transform.position[0]*0.3f) /*/ 5.0f*/;
        float centerYright = (PINKY_TIP.transform.position[1]*0.15f + RING_FINGER_TIP.transform.position[1]*0.15f + MIDDLE_FINGER_TIP.transform.position[1]*0.25f + INDEX_FINGER_TIP.transform.position[1]*0.15f + THUMB_TIP.transform.position[1]*0.3f) /*/ 5.0f*/;
        float centerZright = (PINKY_TIP.transform.position[2]*0.15f + RING_FINGER_TIP.transform.position[2]*0.15f + MIDDLE_FINGER_TIP.transform.position[2]*0.25f + INDEX_FINGER_TIP.transform.position[2]*0.15f + THUMB_TIP.transform.position[2]*0.3f) /*/ 5.0f*/;

        GrabPointRight.transform.position = new Vector3(centerXright, centerYright, centerZright);

        if(rightHandClosed == 1)
        {
            GrabPointRight.tag = "Closed";
        }
        else if(rightHandClosed == 0)
        {
            GrabPointRight.tag = "Open";
        }

        /*}*/
        //ADDED TO TRACK THE SECOND HAND
        /*if (check2 == 0)
        { */
        WRIST2.transform.position = p0pos2 * Mirror;
        WRIST2.transform.eulerAngles = new Vector3(0, 0, 0);

        THUMB_CMC2.transform.position = p1pos2 * Mirror;
        THUMB_CMC2.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_MCP2.transform.position = p2pos2 * Mirror;
        THUMB_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_IP2.transform.position = p3pos2 * Mirror;
        THUMB_IP2.transform.eulerAngles = new Vector3(0, 0, 0);
        THUMB_TIP2.transform.position = p4pos2 * Mirror;
        THUMB_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        INDEX_FINGER_MCP2.transform.position = p5pos2 * Mirror;
        INDEX_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_PIP2.transform.position = p6pos2 * Mirror;
        INDEX_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_DIP2.transform.position = p7pos2 * Mirror;
        INDEX_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        INDEX_FINGER_TIP2.transform.position = p8pos2 * Mirror;
        INDEX_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        MIDDLE_FINGER_MCP2.transform.position = p9pos2 * Mirror;
        MIDDLE_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_PIP2.transform.position = p10pos2 * Mirror;
        MIDDLE_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_DIP2.transform.position = p11pos2 * Mirror;
        MIDDLE_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        MIDDLE_FINGER_TIP2.transform.position = p12pos2 * Mirror;
        MIDDLE_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        RING_FINGER_MCP2.transform.position = p13pos2 * Mirror;
        RING_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_PIP2.transform.position = p14pos2 * Mirror;
        RING_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_DIP2.transform.position = p15pos2 * Mirror;
        RING_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        RING_FINGER_TIP2.transform.position = p16pos2 * Mirror;
        RING_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        PINKY_MCP2.transform.position = p17pos2 * Mirror;
        PINKY_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_PIP2.transform.position = p18pos2 * Mirror;
        PINKY_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_DIP2.transform.position = p19pos2 * Mirror;
        PINKY_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        PINKY_TIP2.transform.position = p20pos2 * Mirror;
        PINKY_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        float centerXleft = (PINKY_TIP2.transform.position[0] * 0.15f + RING_FINGER_TIP2.transform.position[0] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[0] * 0.25f + INDEX_FINGER_TIP2.transform.position[0] * 0.15f + THUMB_TIP2.transform.position[0] * 0.3f) /*/ 5.0f*/;
        float centerYleft = (PINKY_TIP2.transform.position[1] * 0.15f + RING_FINGER_TIP2.transform.position[1] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[1] * 0.25f + INDEX_FINGER_TIP2.transform.position[1] * 0.15f + THUMB_TIP2.transform.position[1] * 0.3f) /*/ 5.0f*/;
        float centerZleft = (PINKY_TIP2.transform.position[2] * 0.15f + RING_FINGER_TIP2.transform.position[2] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[2] * 0.25f + INDEX_FINGER_TIP2.transform.position[2] * 0.15f + THUMB_TIP2.transform.position[2] * 0.3f) /*/ 5.0f*/;

        GrabPointLeft.transform.position = new Vector3(centerXleft, centerYleft, centerZleft);

        if (leftHandClosed == 1)
        {
            GrabPointLeft.tag = "Closed";
        }
        else if (leftHandClosed == 0)
        {
            GrabPointLeft.tag = "Open";
        }
        /*}*/

        cup.transform.position = new Vector3(-8.65f, -9.5f, 12.11f);
        cup1.transform.localPosition = new Vector3(-2.19f, -0.3899994f, 2.41f);
        cup.transform.eulerAngles = new Vector3(0, 0, 0);
        cup1.transform.eulerAngles = new Vector3(0, 0, 0);

    }


    // init
    private void init()
    {
        print("UDPSend.init()");

        // define port
        port = 5005;

        // status
        print("Sending to 127.0.0.1 : " + port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");

        // Create a receive-data thread
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {

        client = new UdpClient(port);
        while (true)
        {

            try
            {
                // Receive bytes 
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // convert bytes to float array
                newdata = ConvertByteToFloat(data);

                setPosition(newdata);

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public static float[] ConvertByteToFloat(byte[] array)
    {
        float[] floatArr = new float[array.Length / 4];
        for (int i = 0; i < floatArr.Length; i++)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array, i * 4, 4);
            }
            floatArr[i] = BitConverter.ToSingle(array, i * 4);
        }
        return floatArr; 
    }

    //mirror tracking
    public int Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }

    // ToDo: design it better
    public void setPosition(float[] trackdata)
    {
        int bone = (int)trackdata[0];
        int hand = (int)trackdata[4];
        if (hand == 0)
        {
            if(newdata[5] == 1)
            {
                rightHandClosed  = 1;
            }
            else if(newdata[5] == 0)
            {
                rightHandClosed = 0;
            }

            switch (bone)
            {
                case 0:
                    p0pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 1:
                    p1pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 2:
                    p2pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 3:
                    p3pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 4:
                    p4pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 5:
                    p5pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 6:
                    p6pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 7:
                    p7pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 8:
                    p8pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 9:
                    p9pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 10:
                    p10pos = new Vector3(newdata[1], newdata[2],newdata[3]) * gain;
                    break;
                case 11:
                    p11pos = new Vector3(newdata[1], newdata[2],newdata[3]) * gain;
                    break;
                case 12:
                    p12pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 13:
                    p13pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 14:
                    p14pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 15:
                    p15pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 16:
                    p16pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 17:
                    p17pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 18:
                    p18pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 19:
                    p19pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 20:
                    p20pos = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
            }
        }
        else if(hand==1)
        {
            if (newdata[5] == 1)
            {
                leftHandClosed = 1;
            }
            else if (newdata[5] == 0)
            {
                leftHandClosed = 0;
            }
            switch (bone)
            {
                case 0:
                    p0pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 1:
                    p1pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 2:
                    p2pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 3:
                    p3pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 4:
                    p4pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 5:
                    p5pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 6:
                    p6pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 7:
                    p7pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 8:
                    p8pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 9:
                    p9pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 10:
                    p10pos2 = new Vector3(newdata[1], newdata[2],newdata[3]) * gain;
                    break;
                case 11:
                    p11pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 12:
                    p12pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 13:
                    p13pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 14:
                    p14pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 15:
                    p15pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 16:
                    p16pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 17:
                    p17pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 18:
                    p18pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 19:
                    p19pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
                case 20:
                    p20pos2 = new Vector3(newdata[1], newdata[2], newdata[3]) * gain;
                    break;
            }
        }
    }
    

    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }


}