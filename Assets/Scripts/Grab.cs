using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject WRIST, THUMB_CMC, THUMB_MCP, THUMB_IP, THUMB_TIP,
        INDEX_FINGER_MCP, INDEX_FINGER_PIP, INDEX_FINGER_DIP, INDEX_FINGER_TIP,
        MIDDLE_FINGER_MCP, MIDDLE_FINGER_PIP, MIDDLE_FINGER_DIP, MIDDLE_FINGER_TIP, RING_FINGER_MCP,
        RING_FINGER_PIP, RING_FINGER_DIP, RING_FINGER_TIP, PINKY_MCP, PINKY_PIP, PINKY_DIP, PINKY_TIP;

    /*blic GameObject WRIST2, THUMB_CMC2, THUMB_MCP2, THUMB_IP2, THUMB_TIP2,
        INDEX_FINGER_MCP2, INDEX_FINGER_PIP2, INDEX_FINGER_DIP2, INDEX_FINGER_TIP2,
        MIDDLE_FINGER_MCP2, MIDDLE_FINGER_PIP2, MIDDLE_FINGER_DIP2, MIDDLE_FINGER_TIP2, RING_FINGER_MCP2,
        RING_FINGER_PIP2, RING_FINGER_DIP2, RING_FINGER_TIP2, PINKY_MCP2, PINKY_PIP2, PINKY_DIP2, PINKY_TIP2;*/

    GameObject grabbedObject;

    //[SerializeField]GameObject grabPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "Open" && grabbedObject)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().detectCollisions = true;
            grabbedObject = null;
        }
        if(grabbedObject)
        {
            grabbedObject.transform.position = transform.position;
        }
    }



    /*private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Collides " + collision.gameObject.name);
        if(other.gameObject.tag == "Grabbable" /**//*&& tag == "Closed"*//*)
        {
            //PUT EVERY JOINT TO isKinematic = true
            //RIGHT HAND
            WRIST.GetComponent<Rigidbody>().detectCollisions = false;

            THUMB_CMC.GetComponent<Rigidbody>().detectCollisions = false;
            
            THUMB_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            THUMB_IP.GetComponent<Rigidbody>().detectCollisions = false;
            THUMB_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            INDEX_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            MIDDLE_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            RING_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;


            PINKY_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            //LEFT HAND
            /*RIST2.GetComponent<Rigidbody>().isKinematic = true;

             THUMB_CMC2.GetComponent<Rigidbody>().isKinematic = true;

             THUMB_MCP2.GetComponent<Rigidbody>().isKinematic = true;
             THUMB_IP2.GetComponent<Rigidbody>().isKinematic = true;
             THUMB_TIP2.GetComponent<Rigidbody>().isKinematic = true;

             INDEX_FINGER_MCP2.GetComponent<Rigidbody>().isKinematic = true;
             INDEX_FINGER_PIP2.GetComponent<Rigidbody>().isKinematic = true;
             INDEX_FINGER_DIP2.GetComponent<Rigidbody>().isKinematic = true;
             INDEX_FINGER_TIP2.GetComponent<Rigidbody>().isKinematic = true;

             MIDDLE_FINGER_MCP2.GetComponent<Rigidbody>().isKinematic = true;
             MIDDLE_FINGER_PIP2.GetComponent<Rigidbody>().isKinematic = true;
             MIDDLE_FINGER_DIP2.GetComponent<Rigidbody>().isKinematic = true;
             MIDDLE_FINGER_TIP2.GetComponent<Rigidbody>().isKinematic = true;

             RING_FINGER_MCP2.GetComponent<Rigidbody>().isKinematic = true;
             RING_FINGER_PIP2.GetComponent<Rigidbody>().isKinematic = true;
             RING_FINGER_DIP2.GetComponent<Rigidbody>().isKinematic = true;
             RING_FINGER_TIP2.GetComponent<Rigidbody>().isKinematic = true;


             PINKY_MCP2.GetComponent<Rigidbody>().isKinematic = true;
             PINKY_PIP2.GetComponent<Rigidbody>().isKinematic = true;
             PINKY_DIP2.GetComponent<Rigidbody>().isKinematic = true;
             PINKY_TIP2.GetComponent<Rigidbody>().isKinematic = true;*//**/

            /*if (tag == "Closed")
            {
                grabbedObject = other.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().detectCollisions = false;
            }*/
           /* else if(tag == "Open")
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            }*/
            /*cube = collision.gameObject;
            cube.GetComponent<Rigidbody>().isKinematic = true;*/
            //Debug.Log("Collides BBBB" + collision.gameObject.name);
        /*}*/
       /* else
        {
            cube.GetComponent<Rigidbody>().isKinematic = false;
        }*/
   /* }*/

    private void OnTriggerExit(Collider other)
    {
        WRIST.GetComponent<Rigidbody>().detectCollisions = true;

        THUMB_CMC.GetComponent<Rigidbody>().detectCollisions = true;

        THUMB_MCP.GetComponent<Rigidbody>().detectCollisions = true;
        THUMB_IP.GetComponent<Rigidbody>().detectCollisions = true;
        THUMB_TIP.GetComponent<Rigidbody>().detectCollisions = true;

        INDEX_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = true;
        INDEX_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = true;
        INDEX_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = true;
        INDEX_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = true;

        MIDDLE_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = true;
        MIDDLE_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = true;
        MIDDLE_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = true;
        MIDDLE_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = true;

        RING_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = true;
        RING_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = true;
        RING_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = true;
        RING_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = true;


        PINKY_MCP.GetComponent<Rigidbody>().detectCollisions = true;
        PINKY_PIP.GetComponent<Rigidbody>().detectCollisions = true;
        PINKY_DIP.GetComponent<Rigidbody>().detectCollisions = true;
        PINKY_TIP.GetComponent<Rigidbody>().detectCollisions = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            //Turn off the collisions
            WRIST.GetComponent<Rigidbody>().detectCollisions = false;

            THUMB_CMC.GetComponent<Rigidbody>().detectCollisions = false;

            THUMB_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            THUMB_IP.GetComponent<Rigidbody>().detectCollisions = false;
            THUMB_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            INDEX_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            INDEX_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            MIDDLE_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            MIDDLE_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            RING_FINGER_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            RING_FINGER_TIP.GetComponent<Rigidbody>().detectCollisions = false;


            PINKY_MCP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_PIP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_DIP.GetComponent<Rigidbody>().detectCollisions = false;
            PINKY_TIP.GetComponent<Rigidbody>().detectCollisions = false;

            //If the hand is closed 
            if (tag == "Closed")
            {
                grabbedObject = other.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().detectCollisions = false;
            }
        }
    }
}

