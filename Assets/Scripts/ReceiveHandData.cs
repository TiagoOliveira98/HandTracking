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
    public float gain2 = 3.5f;

    private int mirror;

    public float[] newdata;

    float rightHandClosed, leftHandClosed;

    public GameObject GrabPointRight, GrabPointLeft;

    /*GameObject find, clone, clone2;*/

    public GameObject rightHand, leftHand;

    //public GameObject cup, cup1;

    float dist, dist2;
    public float ref1, ref2;
    float addLeft, addRight;
    Vector3 addR,addL;

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
        /*find = GameObject.Find("RightHand");
        clone = Instantiate(find);
        Destroy(rightHand);
        clone.name = "RightHand";

        find = GameObject.Find("LeftHand");
        clone2 = Instantiate(find);
        Destroy(leftHand);
        clone2.name = "LeftHand";*/
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        ref1 = Calibration.distRef;
        ref2 = Calibration.distRef2;

        addLeft = 0f;
        addRight = 0f;

        Vector3 addR = new Vector3(0f, 0f, addRight);
        Vector3 addL = new Vector3(0f, 0f, addLeft);
    }


    public void Update()
    {
        //addR.z = addRight;
        //Get distance between WRIST and MIDDLE_FINGER_MCP to use to better improve the depth
        float x1 = p9pos.x;
        float x2 = p0pos.x;
        float y1 = p9pos.y;
        float y2 = p0pos.y;
        float z1 = p9pos.z;
        float z2 = p0pos.z;
        dist = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

        x1 = p9pos2.x;
        x2 = p0pos2.x;
        y1 = p9pos2.y;
        y2 = p0pos2.y;
        z1 = p9pos2.z;
        z2 = p0pos2.z;
        dist2 = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

        ref1 = Calibration.distRef;
        ref2 = Calibration.distRef2;
        if (ref1 != 0f)
        {
            addRight = ref1 - dist;
            addLeft = ref2 - dist2;
        }

        //p0pos.z = p0pos.z + 0.1f* addRight;
        /*p1pos = p1pos + addR * gain;
        p2pos = p2pos + addR * gain;
        p3pos = p3pos + addR * gain;
        p4pos = p4pos + addR * gain;
        p5pos = p5pos + addR * gain;
        p6pos = p6pos + addR * gain;
        p7pos = p7pos + addR * gain;
        p8pos = p8pos + addR * gain;
        p9pos = p9pos + addR * gain;
        p10pos = p10pos + addR * gain;
        p11pos = p11pos + addR * gain;
        p12pos = p12pos + addR * gain;
        p13pos = p13pos + addR * gain;
        p14pos = p14pos + addR * gain;
        p15pos = p15pos + addR * gain;
        p16pos = p16pos + addR * gain;
        p17pos = p17pos + addR * gain;
        p18pos = p18pos + addR * gain;
        p19pos = p19pos + addR * gain;
        p20pos = p20pos + addR * gain;*/

        //WRIST.transform.position = (p0pos * Mirror);
        WRIST.transform.position = (new Vector3(p0pos.x, p0pos.y, p0pos.z + gain2 * addRight))*Mirror;
        WRIST.transform.eulerAngles = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //THUMB_CMC.transform.position = (p1pos /*- p0pos*/) * Mirror;
        THUMB_CMC.transform.position = (new Vector3(p1pos.x, p1pos.y, p1pos.z + gain2 * addRight)) * Mirror;
        THUMB_CMC.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_MCP.transform.position = (p2pos /*- p1pos*/) * Mirror;
        THUMB_MCP.transform.position = (new Vector3(p2pos.x, p2pos.y, p2pos.z + gain2 * addRight)) * Mirror;
        THUMB_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_IP.transform.position = (p3pos /*- p2pos*/) * Mirror;
        THUMB_IP.transform.position = (new Vector3(p3pos.x, p3pos.y, p3pos.z + gain2 * addRight)) * Mirror;
        THUMB_IP.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_TIP.transform.position = (p4pos /*- p3pos*/) * Mirror;
        THUMB_TIP.transform.position = (new Vector3(p4pos.x, p4pos.y, p4pos.z + gain2 * addRight)) * Mirror;
        THUMB_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        //INDEX_FINGER_MCP.transform.position = (p5pos /*- p0pos*/) * Mirror;
        INDEX_FINGER_MCP.transform.position = (new Vector3(p5pos.x, p5pos.y, p5pos.z + gain2 * addRight)) * Mirror;
        INDEX_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_PIP.transform.position = (p6pos /*- p5pos*/) * Mirror;
        INDEX_FINGER_PIP.transform.position = (new Vector3(p6pos.x, p6pos.y, p6pos.z + gain2 * addRight)) * Mirror;
        INDEX_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_DIP.transform.position = (p7pos /*- p6pos*/) * Mirror;
        INDEX_FINGER_DIP.transform.position = (new Vector3(p7pos.x, p7pos.y, p7pos.z + gain2 * addRight)) * Mirror;
        INDEX_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_TIP.transform.position = (p8pos/* - p7pos*/) * Mirror;
        INDEX_FINGER_TIP.transform.position = (new Vector3(p8pos.x, p8pos.y, p8pos.z + gain2 * addRight)) * Mirror;
        INDEX_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        //MIDDLE_FINGER_MCP.transform.position = (p9pos /*- p0pos*/) * Mirror;
        MIDDLE_FINGER_MCP.transform.position = (new Vector3(p9pos.x, p9pos.y, p9pos.z + gain2 * addRight)) * Mirror;
        MIDDLE_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_PIP.transform.position = (p10pos /*- p9pos*/) * Mirror;
        MIDDLE_FINGER_PIP.transform.position = (new Vector3(p10pos.x, p10pos.y, p10pos.z + gain2 * addRight)) * Mirror;
        MIDDLE_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_DIP.transform.position = (p11pos /*- p10pos*/) * Mirror;
        MIDDLE_FINGER_DIP.transform.position = (new Vector3(p11pos.x, p11pos.y, p11pos.z + gain2 * addRight)) * Mirror;
        MIDDLE_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_TIP.transform.position = (p12pos/* - p11pos*/) * Mirror;
        MIDDLE_FINGER_TIP.transform.position = (new Vector3(p12pos.x, p12pos.y, p12pos.z + gain2 * addRight)) * Mirror;
        MIDDLE_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        //RING_FINGER_MCP.transform.position = (p13pos /*- p0pos*/) * Mirror;
        RING_FINGER_MCP.transform.position = (new Vector3(p13pos.x, p13pos.y, p13pos.z + gain2 * addRight)) * Mirror;
        RING_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_PIP.transform.position = (p14pos /*- p13pos*/) * Mirror;
        RING_FINGER_PIP.transform.position = (new Vector3(p14pos.x, p14pos.y, p14pos.z + gain2 * addRight)) * Mirror;
        RING_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_DIP.transform.position = (p15pos /*-p14pos*/) * Mirror;
        RING_FINGER_DIP.transform.position = (new Vector3(p15pos.x, p15pos.y, p15pos.z + gain2 * addRight)) * Mirror;
        RING_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_TIP.transform.position = (p16pos /*- p15pos*/) * Mirror;
        RING_FINGER_TIP.transform.position = (new Vector3(p16pos.x, p16pos.y, p16pos.z + gain2 * addRight)) * Mirror;
        RING_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

        //PINKY_MCP.transform.position = (p17pos /*- p0pos*/) * Mirror;
        PINKY_MCP.transform.position = (new Vector3(p17pos.x, p17pos.y, p17pos.z + gain2 * addRight)) * Mirror;
        PINKY_MCP.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_PIP.transform.position = (p18pos /*- p17pos*/) * Mirror;
        PINKY_PIP.transform.position = (new Vector3(p18pos.x, p18pos.y, p18pos.z + gain2 * addRight)) * Mirror;
        PINKY_PIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_DIP.transform.position = (p19pos /*- p18pos*/) * Mirror;
        PINKY_DIP.transform.position = (new Vector3(p19pos.x, p19pos.y, p19pos.z + gain2 * addRight)) * Mirror;
        PINKY_DIP.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_TIP.transform.position = (p20pos /*-p19pos*/) * Mirror;
        PINKY_TIP.transform.position = (new Vector3(p20pos.x, p20pos.y, p20pos.z + gain2 * addRight)) * Mirror;
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

        /*addL.z = addLeft;

        p0pos2 = p0pos2 + addL;
        p1pos2 = p1pos2 + addL;
        p2pos2 = p2pos2 + addL;
        p3pos2 = p3pos2 + addL;
        p4pos2 = p4pos2 + addL;
        p5pos2 = p5pos2 + addL;
        p6pos2 = p6pos2 + addL;
        p7pos2 = p7pos2 + addL;
        p8pos2 = p8pos2 + addL;
        p9pos2 = p9pos2 + addL;
        p10pos2 = p10pos2 + addL;
        p11pos2 = p11pos2 + addL;
        p12pos2 = p12pos2 + addL;
        p13pos2 = p13pos2 + addL;
        p14pos2 = p14pos2 + addL;
        p15pos2 = p15pos2 + addL;
        p16pos2 = p16pos2 + addL;
        p17pos2 = p17pos2 + addL;
        p18pos2 = p18pos2 + addL;
        p19pos2 = p19pos2 + addL;
        p20pos2 = p20pos2 + addL;*/


        //WRIST.transform.position = (p0pos * Mirror);
        WRIST2.transform.position = (new Vector3(p0pos2.x, p0pos2.y, p0pos2.z + gain2 * addLeft)) * Mirror;
        WRIST2.transform.eulerAngles = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //WRIST.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //THUMB_CMC.transform.position = (p1pos /*- p0pos*/) * Mirror;
        THUMB_CMC2.transform.position = (new Vector3(p1pos2.x, p1pos2.y, p1pos2.z + gain2 * addLeft)) * Mirror;
        THUMB_CMC2.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_MCP.transform.position = (p2pos /*- p1pos*/) * Mirror;
        THUMB_MCP2.transform.position = (new Vector3(p2pos2.x, p2pos2.y, p2pos2.z + gain2 * addLeft)) * Mirror;
        THUMB_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_IP.transform.position = (p3pos /*- p2pos*/) * Mirror;
        THUMB_IP2.transform.position = (new Vector3(p3pos2.x, p3pos2.y, p3pos2.z + gain2 * addLeft)) * Mirror;
        THUMB_IP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //THUMB_TIP.transform.position = (p4pos /*- p3pos*/) * Mirror;
        THUMB_TIP2.transform.position = (new Vector3(p4pos2.x, p4pos2.y, p4pos2.z + gain2 * addLeft)) * Mirror;
        THUMB_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        //INDEX_FINGER_MCP.transform.position = (p5pos /*- p0pos*/) * Mirror;
        INDEX_FINGER_MCP2.transform.position = (new Vector3(p5pos2.x, p5pos2.y, p5pos2.z + gain2 * addLeft)) * Mirror;
        INDEX_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_PIP.transform.position = (p6pos /*- p5pos*/) * Mirror;
        INDEX_FINGER_PIP2.transform.position = (new Vector3(p6pos2.x, p6pos2.y, p6pos2.z + gain2 * addLeft)) * Mirror;
        INDEX_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_DIP.transform.position = (p7pos /*- p6pos*/) * Mirror;
        INDEX_FINGER_DIP2.transform.position = (new Vector3(p7pos2.x, p7pos2.y, p7pos2.z + gain2 * addLeft)) * Mirror;
        INDEX_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //INDEX_FINGER_TIP.transform.position = (p8pos/* - p7pos*/) * Mirror;
        INDEX_FINGER_TIP2.transform.position = (new Vector3(p8pos2.x, p8pos2.y, p8pos2.z + gain2 * addLeft)) * Mirror;
        INDEX_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        //MIDDLE_FINGER_MCP.transform.position = (p9pos /*- p0pos*/) * Mirror;
        MIDDLE_FINGER_MCP2.transform.position = (new Vector3(p9pos2.x, p9pos2.y, p9pos2.z + gain2 * addLeft)) * Mirror;
        MIDDLE_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_PIP.transform.position = (p10pos /*- p9pos*/) * Mirror;
        MIDDLE_FINGER_PIP2.transform.position = (new Vector3(p10pos2.x, p10pos2.y, p10pos2.z + gain2 * addLeft)) * Mirror;
        MIDDLE_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_DIP.transform.position = (p11pos /*- p10pos*/) * Mirror;
        MIDDLE_FINGER_DIP2.transform.position = (new Vector3(p11pos2.x, p11pos2.y, p11pos2.z + gain2 * addLeft)) * Mirror;
        MIDDLE_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //MIDDLE_FINGER_TIP.transform.position = (p12pos/* - p11pos*/) * Mirror;
        MIDDLE_FINGER_TIP2.transform.position = (new Vector3(p12pos2.x, p12pos2.y, p12pos2.z + gain2 * addLeft)) * Mirror;
        MIDDLE_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        //RING_FINGER_MCP.transform.position = (p13pos /*- p0pos*/) * Mirror;
        RING_FINGER_MCP2.transform.position = (new Vector3(p13pos2.x, p13pos2.y, p13pos2.z + gain2 * addLeft)) * Mirror;
        RING_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_PIP.transform.position = (p14pos /*- p13pos*/) * Mirror;
        RING_FINGER_PIP2.transform.position = (new Vector3(p14pos2.x, p14pos2.y, p14pos2.z + gain2 * addLeft)) * Mirror;
        RING_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_DIP.transform.position = (p15pos /*-p14pos*/) * Mirror;
        RING_FINGER_DIP2.transform.position = (new Vector3(p15pos2.x, p15pos2.y, p15pos2.z + gain2 * addLeft)) * Mirror;
        RING_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //RING_FINGER_TIP.transform.position = (p16pos /*- p15pos*/) * Mirror;
        RING_FINGER_TIP2.transform.position = (new Vector3(p16pos2.x, p16pos2.y, p16pos2.z + gain2 * addLeft)) * Mirror;
        RING_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

        //PINKY_MCP.transform.position = (p17pos /*- p0pos*/) * Mirror;
        PINKY_MCP2.transform.position = (new Vector3(p17pos2.x, p17pos2.y, p17pos2.z + gain2 * addLeft)) * Mirror;
        PINKY_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_PIP.transform.position = (p18pos /*- p17pos*/) * Mirror;
        PINKY_PIP2.transform.position = (new Vector3(p18pos2.x, p18pos2.y, p18pos2.z + gain2 * addLeft)) * Mirror;
        PINKY_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_DIP.transform.position = (p19pos /*- p18pos*/) * Mirror;
        PINKY_DIP2.transform.position = (new Vector3(p19pos2.x, p19pos2.y, p19pos2.z + gain2 * addLeft)) * Mirror;
        PINKY_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);
        //PINKY_TIP.transform.position = (p20pos /*-p19pos*/) * Mirror;
        PINKY_TIP2.transform.position = (new Vector3(p20pos2.x, p20pos2.y, p20pos2.z + gain2 * addLeft)) * Mirror;
        PINKY_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);



        //COMMENTED FROM HERE
        /*
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
        */
        //THROUGH HERE

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

        /*cup.transform.position = new Vector3(-8.65f, -9.5f, 12.11f);
        cup1.transform.localPosition = new Vector3(-2.19f, -0.3899994f, 2.41f);
        cup.transform.eulerAngles = new Vector3(0, 0, 0);
        cup1.transform.eulerAngles = new Vector3(0, 0, 0);*/

        //Get distance between WRIST and MIDDLE_FINGER_MCP to use to better improve the depth
        /*float x1 = MIDDLE_FINGER_MCP.transform.position.x;
        float x2 = WRIST.transform.position.x;
        float y1 = MIDDLE_FINGER_MCP.transform.position.y;
        float y2 = WRIST.transform.position.y;
        float z1 = MIDDLE_FINGER_MCP.transform.position.z;
        float z2 = WRIST.transform.position.z;
        dist = (float)Math.Sqrt( Mathf.Pow(x1-x2,2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

        x1 = MIDDLE_FINGER_MCP2.transform.position.x;
        x2 = WRIST2.transform.position.x;
        y1 = MIDDLE_FINGER_MCP2.transform.position.y;
        y2 = WRIST2.transform.position.y;
        z1 = MIDDLE_FINGER_MCP2.transform.position.z;
        z2 = WRIST2.transform.position.z;
        dist2 = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

        ref1 = Calibration.distRef;
        ref2 = Calibration.distRef2;
        if(ref1 != 0f)
        {
            addRight = ref1 - dist;
            addLeft = ref2 - dist2;
        }*/
        //Compare the values to the reference
        /*addRight = ref1 - dist;
        addLeft = ref2 - dist2;*/


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