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

    //
    bool go;

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

    public float gain = 15;
    public float gain2 = 4;
    public float gain3;

    private int mirror;

    public float[] newdata;

    float rightHandClosed, leftHandClosed;

    public GameObject GrabPointRight, GrabPointLeft;
    public GameObject rightHand, leftHand;
    public GameObject grabRight, grabLeft;

    string line;

    public GameObject log;

    float dist, dist2;
    public float ref1, ref2;
    float addLeft, addRight;

    float x1, x2, y1, y2, z1, z2;

    float wristRightX, wristLeftX;

    int check2, check3;

    // start
    public void Start()
    {
       

        init();
        
        gain = 15; //Gain for the hands
        gain3 = -2;
        Mirror = -1; // set -1 for mirroring, else set 1

        //To fix a bug
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        GrabPointLeft.SetActive(false);
        GrabPointRight.SetActive(false);
        GrabPointLeft.SetActive(true);
        GrabPointRight.SetActive(true);

        //Values of the references from the calibration
        ref1 = Calibration.distRef;
        ref2 = Calibration.distRef2;

        //Variables that show the difference between the values calculated from teh data and teh references
        addLeft = 0f;
        addRight = 0f;

        x1 = 0;
        x2 = 0;
        y1 = 0;
        y2 = 0;
        z1 = 0;
        z2 = 0;

        wristRightX = 0f;
        wristLeftX = 0f;
        if (GameObject.Find("Bucket") == false)
        {
            line = "WRIST,THUMB_CMC,THUMB_MCP,THUMB_IP,THUMB_TIP," +
            "INDEX_FINGER_MCP,INDEX_FINGER_PIP,INDEX_FINGER_DIP,INDEX_FINGER_TIP," +
            "MIDDLE_FINGER_MCP,MIDDLE_FINGER_PIP,MIDDLE_FINGER_DIP,MIDDLE_FINGER_TIP," +
            "RING_FINGER_MCP,RING_FINGER_PIP,RING_FINGER_DIP,RING_FINGER_TIP," +
            "PINKY_MCP,PINKY_PIP,PINKY_DIP,PINKY_TIP," +
            "WRIST2,THUMB_CMC2,THUMB_MCP2,THUMB_IP2,THUMB_TIP2," +
            "INDEX_FINGER_MCP2,INDEX_FINGER_PIP2,INDEX_FINGER_DIP2,INDEX_FINGER_TIP2," +
            "MIDDLE_FINGER_MCP2,MIDDLE_FINGER_PIP2,MIDDLE_FINGER_DIP2,MIDDLE_FINGER_TIP2," +
            "RING_FINGER_MCP2,RING_FINGER_PIP2,RING_FINGER_DIP2,RING_FINGER_TIP2," +
            "PINKY_MCP2,PINKY_PIP2,PINKY_DIP2,PINKY_TIP2," +
            "Timestamp,Event";
            log.GetComponent<DataLogs>().Log(line, "DnF", false);
            line = "";
        }
        check2 = 0;
        check3 = 0;

        //
        go = false;
        Invoke("Wait", 2);


    }


    public void Update()
    {
        /*if (go)
        {*/
            //Get distance between WRIST and MIDDLE_FINGER_MCP to use to better improve the depth
            x1 = p9pos.x;
            x2 = p0pos.x;
            y1 = p9pos.y;
            y2 = p0pos.y;
            z1 = p9pos.z;
            z2 = p0pos.z;
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

            //
            GameObject check = GameObject.Find("Logging");
            check.GetComponent<DataLogs>().ev = "";


            //Update the coordinates of each joint from both hands
            //RIGHT HAND
            //WRIST
            WRIST.transform.position = (new Vector3(p0pos.x + gain3, p0pos.y, p0pos.z + gain2 * addRight)) * Mirror;
            //WRIST.transform.eulerAngles = new Vector3(0, 0, 0);

            //THUMB
            THUMB_CMC.transform.position = (new Vector3(p1pos.x + gain3, p1pos.y, p1pos.z + gain2 * addRight)) * Mirror;
            //THUMB_CMC.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_MCP.transform.position = (new Vector3(p2pos.x + gain3, p2pos.y, p2pos.z + gain2 * addRight)) * Mirror;
            //THUMB_MCP.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_IP.transform.position = (new Vector3(p3pos.x + gain3, p3pos.y, p3pos.z + gain2 * addRight)) * Mirror;
            //THUMB_IP.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_TIP.transform.position = (new Vector3(p4pos.x + gain3, p4pos.y, p4pos.z + gain2 * addRight)) * Mirror;
            //THUMB_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

            //INDEX FINGER
            INDEX_FINGER_MCP.transform.position = (new Vector3(p5pos.x + gain3, p5pos.y, p5pos.z + gain2 * addRight)) * Mirror;
            //INDEX_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_PIP.transform.position = (new Vector3(p6pos.x + gain3, p6pos.y, p6pos.z + gain2 * addRight)) * Mirror;
            //INDEX_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_DIP.transform.position = (new Vector3(p7pos.x + gain3, p7pos.y, p7pos.z + gain2 * addRight)) * Mirror;
            //INDEX_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_TIP.transform.position = (new Vector3(p8pos.x + gain3, p8pos.y, p8pos.z + gain2 * addRight)) * Mirror;
            //INDEX_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

            //MIDDLE FINGER
            MIDDLE_FINGER_MCP.transform.position = (new Vector3(p9pos.x + gain3, p9pos.y, p9pos.z + gain2 * addRight)) * Mirror;
            //MIDDLE_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_PIP.transform.position = (new Vector3(p10pos.x + gain3, p10pos.y, p10pos.z + gain2 * addRight)) * Mirror;
            //MIDDLE_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_DIP.transform.position = (new Vector3(p11pos.x + gain3, p11pos.y, p11pos.z + gain2 * addRight)) * Mirror;
            //MIDDLE_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_TIP.transform.position = (new Vector3(p12pos.x + gain3, p12pos.y, p12pos.z + gain2 * addRight)) * Mirror;
            //MIDDLE_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

            //RING FINGER
            RING_FINGER_MCP.transform.position = (new Vector3(p13pos.x + gain3, p13pos.y, p13pos.z + gain2 * addRight)) * Mirror;
            //RING_FINGER_MCP.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_PIP.transform.position = (new Vector3(p14pos.x + gain3, p14pos.y, p14pos.z + gain2 * addRight)) * Mirror;
            //RING_FINGER_PIP.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_DIP.transform.position = (new Vector3(p15pos.x + gain3, p15pos.y, p15pos.z + gain2 * addRight)) * Mirror;
            //RING_FINGER_DIP.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_TIP.transform.position = (new Vector3(p16pos.x + gain3, p16pos.y, p16pos.z + gain2 * addRight)) * Mirror;
            //RING_FINGER_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

            //PINKY
            PINKY_MCP.transform.position = (new Vector3(p17pos.x + gain3, p17pos.y, p17pos.z + gain2 * addRight)) * Mirror;
            //PINKY_MCP.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_PIP.transform.position = (new Vector3(p18pos.x + gain3, p18pos.y, p18pos.z + gain2 * addRight)) * Mirror;
            //PINKY_PIP.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_DIP.transform.position = (new Vector3(p19pos.x + gain3, p19pos.y, p19pos.z + gain2 * addRight)) * Mirror;
            //PINKY_DIP.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_TIP.transform.position = (new Vector3(p20pos.x + gain3, p20pos.y, p20pos.z + gain2 * addRight)) * Mirror;
            //PINKY_TIP.transform.eulerAngles = new Vector3(0, 0, 0);

            //COORDINATES FOR THE GRABBING POINT
            float centerXright = (PINKY_TIP.transform.position[0] * 0.15f + RING_FINGER_TIP.transform.position[0] * 0.15f + MIDDLE_FINGER_TIP.transform.position[0] * 0.25f + INDEX_FINGER_TIP.transform.position[0] * 0.15f + THUMB_TIP.transform.position[0] * 0.3f) /*/ 5.0f*/;
            float centerYright = (PINKY_TIP.transform.position[1] * 0.15f + RING_FINGER_TIP.transform.position[1] * 0.15f + MIDDLE_FINGER_TIP.transform.position[1] * 0.25f + INDEX_FINGER_TIP.transform.position[1] * 0.15f + THUMB_TIP.transform.position[1] * 0.3f) /*/ 5.0f*/;
            float centerZright = (PINKY_TIP.transform.position[2] * 0.15f + RING_FINGER_TIP.transform.position[2] * 0.15f + MIDDLE_FINGER_TIP.transform.position[2] * 0.25f + INDEX_FINGER_TIP.transform.position[2] * 0.15f + THUMB_TIP.transform.position[2] * 0.3f) /*/ 5.0f*/;
            GrabPointRight.transform.position = new Vector3(centerXright, centerYright, centerZright);

            //Change the tag to Closed or Open
            if (rightHandClosed == 1)
            {
                //if (check3 == 0)
                //{
                    GrabPointRight.tag = "Closed";
                    //GameObject check = GameObject.Find("Logging");
                    if (check != null)
                    {
                        check.GetComponent<DataLogs>().ev = "Right Hand Closed ";
                    }
                    check3 = 1;
                //}
            }
            else if (rightHandClosed == 0)
            {
                GrabPointRight.tag = "Open";
                check3 = 0;
                /*GameObject check = GameObject.Find("Logging");
                if (check != null)
                {
                    check.GetComponent<DataLogs>().ev = "Right Hand Opened";
                }*/
            }

            //LEFT HAND
            //WRIST
            WRIST2.transform.position = (new Vector3(p0pos2.x + gain3, p0pos2.y, p0pos2.z + gain2 * addLeft)) * Mirror;
            //WRIST2.transform.eulerAngles = new Vector3(0, 0, 0);

            //THUMB
            THUMB_CMC2.transform.position = (new Vector3(p1pos2.x + gain3, p1pos2.y, p1pos2.z + gain2 * addLeft)) * Mirror;
            //THUMB_CMC2.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_MCP2.transform.position = (new Vector3(p2pos2.x + gain3, p2pos2.y, p2pos2.z + gain2 * addLeft)) * Mirror;
            //THUMB_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_IP2.transform.position = (new Vector3(p3pos2.x + gain3, p3pos2.y, p3pos2.z + gain2 * addLeft)) * Mirror;
            //THUMB_IP2.transform.eulerAngles = new Vector3(0, 0, 0);

            THUMB_TIP2.transform.position = (new Vector3(p4pos2.x + gain3, p4pos2.y, p4pos2.z + gain2 * addLeft)) * Mirror;
            //THUMB_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            //INDEX FINGER
            INDEX_FINGER_MCP2.transform.position = (new Vector3(p5pos2.x + gain3, p5pos2.y, p5pos2.z + gain2 * addLeft)) * Mirror;
            //INDEX_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_PIP2.transform.position = (new Vector3(p6pos2.x + gain3, p6pos2.y, p6pos2.z + gain2 * addLeft)) * Mirror;
            //INDEX_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_DIP2.transform.position = (new Vector3(p7pos2.x + gain3, p7pos2.y, p7pos2.z + gain2 * addLeft)) * Mirror;
            //INDEX_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            INDEX_FINGER_TIP2.transform.position = (new Vector3(p8pos2.x + gain3, p8pos2.y, p8pos2.z + gain2 * addLeft)) * Mirror;
            //INDEX_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            //MIDDLE FINGER
            MIDDLE_FINGER_MCP2.transform.position = (new Vector3(p9pos2.x + gain3, p9pos2.y, p9pos2.z + gain2 * addLeft)) * Mirror;
            //MIDDLE_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_PIP2.transform.position = (new Vector3(p10pos2.x + gain3, p10pos2.y, p10pos2.z + gain2 * addLeft)) * Mirror;
            //MIDDLE_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_DIP2.transform.position = (new Vector3(p11pos2.x + gain3, p11pos2.y, p11pos2.z + gain2 * addLeft)) * Mirror;
            //MIDDLE_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            MIDDLE_FINGER_TIP2.transform.position = (new Vector3(p12pos2.x + gain3, p12pos2.y, p12pos2.z + gain2 * addLeft)) * Mirror;
            //MIDDLE_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            //RING FINGER
            RING_FINGER_MCP2.transform.position = (new Vector3(p13pos2.x + gain3, p13pos2.y, p13pos2.z + gain2 * addLeft)) * Mirror;
            //RING_FINGER_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_PIP2.transform.position = (new Vector3(p14pos2.x + gain3, p14pos2.y, p14pos2.z + gain2 * addLeft)) * Mirror;
            //RING_FINGER_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_DIP2.transform.position = (new Vector3(p15pos2.x + gain3, p15pos2.y, p15pos2.z + gain2 * addLeft)) * Mirror;
            //RING_FINGER_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            RING_FINGER_TIP2.transform.position = (new Vector3(p16pos2.x + gain3, p16pos2.y, p16pos2.z + gain2 * addLeft)) * Mirror;
            //RING_FINGER_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            //PINKY
            PINKY_MCP2.transform.position = (new Vector3(p17pos2.x + gain3, p17pos2.y, p17pos2.z + gain2 * addLeft)) * Mirror;
            //PINKY_MCP2.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_PIP2.transform.position = (new Vector3(p18pos2.x + gain3, p18pos2.y, p18pos2.z + gain2 * addLeft)) * Mirror;
            //PINKY_PIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_DIP2.transform.position = (new Vector3(p19pos2.x + gain3, p19pos2.y, p19pos2.z + gain2 * addLeft)) * Mirror;
            //PINKY_DIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            PINKY_TIP2.transform.position = (new Vector3(p20pos2.x + gain3, p20pos2.y, p20pos2.z + gain2 * addLeft)) * Mirror;
            //PINKY_TIP2.transform.eulerAngles = new Vector3(0, 0, 0);

            //COORDINATES FOR THE GRABBING POINT
            float centerXleft = (PINKY_TIP2.transform.position[0] * 0.15f + RING_FINGER_TIP2.transform.position[0] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[0] * 0.25f + INDEX_FINGER_TIP2.transform.position[0] * 0.15f + THUMB_TIP2.transform.position[0] * 0.3f) /*/ 5.0f*/;
            float centerYleft = (PINKY_TIP2.transform.position[1] * 0.15f + RING_FINGER_TIP2.transform.position[1] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[1] * 0.25f + INDEX_FINGER_TIP2.transform.position[1] * 0.15f + THUMB_TIP2.transform.position[1] * 0.3f) /*/ 5.0f*/;
            float centerZleft = (PINKY_TIP2.transform.position[2] * 0.15f + RING_FINGER_TIP2.transform.position[2] * 0.15f + MIDDLE_FINGER_TIP2.transform.position[2] * 0.25f + INDEX_FINGER_TIP2.transform.position[2] * 0.15f + THUMB_TIP2.transform.position[2] * 0.3f) /*/ 5.0f*/;
            GrabPointLeft.transform.position = new Vector3(centerXleft, centerYleft, centerZleft);

            //Change the tag to Closed or Open
            if (leftHandClosed == 1)
            {
                GrabPointLeft.tag = "Closed";
                //if (check2 == 0)
                //{
                    //GameObject check = GameObject.Find("Logging");
                    if (check != null)
                    {
                        check.GetComponent<DataLogs>().ev += "Left Hand Closed";
                    }
                    check2 = 1;
                //}
                //GameObject.Find("Logging").GetComponent<DataLogs>().ev = "Left Hand Closed";
            }
            else if (leftHandClosed == 0)
            {
                GrabPointLeft.tag = "Open";
                check2 = 0;
                //GameObject.Find("Logging").GetComponent<DataLogs>().ev = "Left Hand Opened";
            }

            GameObject g = GameObject.Find("FingersUp");
            if ( g != null )
            {
                string s = " " + g.GetComponent<FingerCount>().fingers.ToString();
                check.GetComponent<DataLogs>().ev += s;
            }

            //Logging Data
            //if (GameObject.Find("BucketBlue") != null /*&& wristRightX != WRIST.transform.position.x && wristLeftX != WRIST2.transform.position.x*/)
            /*{
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
                line += "," + PINKY_TIP2.transform.position.x.ToString().Replace(",",".") + " " + PINKY_TIP2.transform.position.y.ToString().Replace(",", ".") + " " + PINKY_TIP2.transform.position.z.ToString().Replace(",", ".")*/ /*+ "\n"*/;

            /*log.GetComponent<DataLogs>().Log(line);
            line = "";
        }*/

            //wristRightX = WRIST.transform.position.x;
            //wristLeftX = WRIST2.transform.position.x;

        /*}*/
        
    }

    void Wait()
    {
        go = true;
    }


    void OnApplicationQuit()
    {
        log.GetComponent<DataLogs>().CloseFile();
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