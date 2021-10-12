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

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;

        cube.GetComponent<Rigidbody>().isKinematic = true;
        cube.GetComponent<Rigidbody>().detectCollisions = false;

        distRef = 0f;
        distRef2 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdown.GetComponent<Text>().text = timeRemaining.ToString().Replace(",", "");
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                //Delete the txts in the screen
                Destroy(countdown);
                Destroy(instructions);
                //Fix the reference of the distances
                float x1 = MIDDLE_FINGER_MCP.transform.position.x;
                float x2 = WRIST.transform.position.x;
                float y1 = MIDDLE_FINGER_MCP.transform.position.y;
                float y2 = WRIST.transform.position.y;
                float z1 = MIDDLE_FINGER_MCP.transform.position.z;
                float z2 = WRIST.transform.position.z;
                distRef = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

                x1 = MIDDLE_FINGER_MCP2.transform.position.x;
                x2 = WRIST2.transform.position.x;
                y1 = MIDDLE_FINGER_MCP2.transform.position.y;
                y2 = WRIST2.transform.position.y;
                z1 = MIDDLE_FINGER_MCP2.transform.position.z;
                z2 = WRIST2.transform.position.z;
                distRef2 = (float)Math.Sqrt(Mathf.Pow(x1 - x2, 2f) + Mathf.Pow(y1 - y2, 2f) + Mathf.Pow(z1 - z2, 2f));

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