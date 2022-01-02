using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    public GameObject MIDDLE_FINGER_MCP, MIDDLE_FINGER_MCP2, WRIST, WRIST2; 
    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    public Text countdown, instructions;

    public static float distRef, distRef2;

    public GameObject cube;

    public string user1;
    public static string user;

    public string hand1;
    public static string hand;

    // Start is called before the first frame update
    void Start()
    {
        //Flag that says that the countdown is running
        timerIsRunning = true;

        //Keep the cube in the start position for the calibration process
        cube.GetComponent<Rigidbody>().isKinematic = true;
        cube.GetComponent<Rigidbody>().detectCollisions = false;

        //Start the reference for both hands at 0
        distRef = 0f;
        distRef2 = 0f;

        user = null;
        user1 = null;

        hand = null;
        hand1 = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(user == null || hand == null)
        {
            user = user1;
            hand = hand1;
        }
        //Check if the Countdown is still ON
        else if (timerIsRunning)
        {
            //If the countdown is still running keep decreasing the number and diplay it
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdown.GetComponent<Text>().text = (timeRemaining).ToString().Replace(",", " ");
            }
            //If the countdown has come to 0
            else
            {
                timeRemaining = 0;
                //Flag changed to demonstrate that the countdown is not running anymore
                timerIsRunning = false;
                //Delete the TXT's in the screen
                Destroy(countdown);
                Destroy(instructions);
                //Fix the reference of the distances
                float x1 = MIDDLE_FINGER_MCP.transform.position.x;
                float x2 = WRIST.transform.position.x;
                float y1 = MIDDLE_FINGER_MCP.transform.position.y;
                float y2 = WRIST.transform.position.y;
                float z1 = MIDDLE_FINGER_MCP.transform.position.z;
                float z2 = WRIST.transform.position.z;
                //Reference for the Right Hand
                distRef = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

                x1 = MIDDLE_FINGER_MCP2.transform.position.x;
                x2 = WRIST2.transform.position.x;
                y1 = MIDDLE_FINGER_MCP2.transform.position.y;
                y2 = WRIST2.transform.position.y;
                z1 = MIDDLE_FINGER_MCP2.transform.position.z;
                z2 = WRIST2.transform.position.z;
                //Reference for the Left Hand
                distRef2 = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

                //Establish the normal behaviour of the cube (Physics and Collisions)
                cube.GetComponent<Rigidbody>().isKinematic = false;
                cube.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
        else 
        {
            Debug.Log("Right Reference: " + distRef+ " and Left Reference: " + distRef2);
        }
    }
}