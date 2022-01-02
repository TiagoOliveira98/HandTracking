using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerCount : MonoBehaviour
{
    public float time;

    public int fingers;

    public int selection;

    int fingerAux;

    int numberSelection;

    public GameObject indexTip, indexJoint, middleTip, middleJoint, ringTip, ringJoint, pinkyTip, pinkyJoint;
    public GameObject indexTip2, indexJoint2, middleTip2, middleJoint2, ringTip2, ringJoint2, pinkyTip2, pinkyJoint2;
    public GameObject thumbTip, thumbJoint;
    public GameObject thumbTip2, thumbJoint2;

    public GameObject colorSelect1, colorSelect2;

    public Material choice, defaultMaterial;

    public GameObject c1, c2, c3, c4, c5, c6, c7, c8, c9, c10;

    //public Text N;

    public int[] vector;

    public int[] selections;

    GameObject aux, aux1;

    // Start is called before the first frame update

    // Start is called before the first frame update
    void Start()
    {
        fingers = 0;
        time = 0f;
        selection = -1;
        numberSelection = 1;

        vector = new int[10];
        for(int i = 0; i<10; i++)
        {
            vector[i] = 0;
        }
        selections = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        //Obtain the last and newest value chosen
        fingerAux = fingers;
        fingerCount();

        //Show the number focused
        //N.text = fingers.ToString();

        ResetDisplay();
        //Verify if the number displayed changed
        //if(fingerAux != fingers && ColorRandomMemory.check == true)
        //{
        //    //Verify if the number displayed is valid
        //    if(fingerAux != 0 && fingers != 0 )//&& vector[fingerAux - 1] != 1)
        //    {
        //        //If the last box chosen isn't already locked in a choice or checked right
        //        if(vector[fingerAux - 1] != 1)
        //        {
        //            //Return to the default material
        //            GameObject.Find(fingerAux.ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
        //            GameObject.Find(fingerAux.ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
        //        }
                  
        //        //Reveal the box chosen with a color
        //        if (vector[fingers - 1] != 1)
        //            GameObject.Find(fingers.ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = choice;
        //    }
            
        //    //Restart time
        //    time = 0;
        //}

        //Protection to not select boxes already taken
        if(fingers != 0)
        {
            if (vector[fingers - 1] == 1)
                time = 0;
        }
        
            
        //Count the time fixed in one chosen number to then select it
        if (fingerAux == fingers && ColorRandomMemory.check == true && fingers != 0)
        {
            time += Time.deltaTime;
        }

        //In case that the user maintains a numbers selection for more than the time of treshold
        if(time > 2)
        {
            //First Selection
            if(numberSelection == 1)
            {
                FirstChoice();
                //GameObject a1 = GameObject.Find(selections[0].ToString());
                //GameObject a2 = GameObject.Find(selections[1].ToString());

                //if (selections[1] != 0 && a1.transform.GetChild(1).GetComponent<MeshRenderer>().material.name != a2.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                //{
                //    //GameObject a1 = GameObject.Find(selections[0].ToString());
                //    a1.GetComponent<MeshRenderer>().material = defaultMaterial;
                //    a1.transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
                //    //GameObject a2 = GameObject.Find(selections[1].ToString());
                //    a2.GetComponent<MeshRenderer>().material = defaultMaterial;
                //    a2.transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
                //}


                ////Set the Indicators for Choices for the default and the first color of choice
                //colorSelect1.GetComponent<MeshRenderer>().material = GameObject.Find(fingers.ToString()).transform.GetChild(1).GetComponent<MeshRenderer>().material;
                //colorSelect2.GetComponent<MeshRenderer>().material = defaultMaterial;

                ////Identify that it is going to be the second choice next
                //numberSelection = 2;
                ////Restart the timer
                //time = 0;
                ////Save the second choice in an array
                //selections[0] = fingers;

                ////Put Color in the box
                //aux = GameObject.Find(selections[0].ToString());
                //aux.GetComponent<MeshRenderer>().material = aux.transform.GetChild(1).GetComponent<MeshRenderer>().material;
                //aux.transform.GetChild(0).GetComponent<MeshRenderer>().material = aux.transform.GetChild(1).GetComponent<MeshRenderer>().material;

                ////Identify that the box is already ticked
                //vector[fingers - 1] = 1;
            }
            //Second Selection
            else if(numberSelection == 2)
            {
                SecondChoice();
                ////Set the second choice on the board
                //colorSelect2.GetComponent<MeshRenderer>().material = GameObject.Find(fingers.ToString()).transform.GetChild(1).GetComponent<MeshRenderer>().material;

                ////numberSelection = -1;
                ////Save the second choice in an array
                //selections[1] = fingers;
                ////Identify that the box is already ticked
                //vector[fingers - 1] = 1;

                ////Put Color in the box
                //aux1 = GameObject.Find(selections[1].ToString());
                //aux1.GetComponent<MeshRenderer>().material = aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material;
                //aux1.transform.GetChild(0).GetComponent<MeshRenderer>().material = aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material;

                ////Compare the two choices
                ////If negative reset the two positions ticked during the choosing part
                //if (aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material.name != aux.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
                //{
                //    vector[selections[0] - 1] = 0;
                //    vector[selections[1] - 1] = 0;
                //
                //    //
                //    GameObject.Find(selections[0].ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
                //    GameObject.Find(selections[0].ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;

                //    GameObject.Find(selections[1].ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
                //    GameObject.Find(selections[1].ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
                //}
                ////Reset the time
                //time = 0;
                ////Identify that the next choice is the first agaion
                //numberSelection = 1;
                EndPuzzle();
            } 
        }
    }

    void ResetDisplay()
    {
        if (fingerAux != fingers && ColorRandomMemory.check == true)
        {
            //Verify if the number displayed is valid
            if (fingerAux != 0 && fingers != 0)//&& vector[fingerAux - 1] != 1)
            {
                //If the last box chosen isn't already locked in a choice or checked right
                if (vector[fingerAux - 1] != 1)
                {
                    //Return to the default material
                    GameObject.Find(fingerAux.ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
                    GameObject.Find(fingerAux.ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
                }

                //Reveal the box chosen with a color
                if (vector[fingers - 1] != 1)
                    GameObject.Find(fingers.ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = choice;
            }

            //Restart time
            time = 0;
        }
    }
    void FirstChoice()
    {
        GameObject a1 = GameObject.Find(selections[0].ToString());
        GameObject a2 = GameObject.Find(selections[1].ToString());

        if (selections[1] != 0 && a1.transform.GetChild(1).GetComponent<MeshRenderer>().material.name != a2.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
        {
            //GameObject a1 = GameObject.Find(selections[0].ToString());
            a1.GetComponent<MeshRenderer>().material = defaultMaterial;
            a1.transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
            //GameObject a2 = GameObject.Find(selections[1].ToString());
            a2.GetComponent<MeshRenderer>().material = defaultMaterial;
            a2.transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
        }


        //Set the Indicators for Choices for the default and the first color of choice
        colorSelect1.GetComponent<MeshRenderer>().material = GameObject.Find(fingers.ToString()).transform.GetChild(1).GetComponent<MeshRenderer>().material;
        colorSelect2.GetComponent<MeshRenderer>().material = defaultMaterial;

        //Identify that it is going to be the second choice next
        numberSelection = 2;
        //Restart the timer
        time = 0;
        //Save the second choice in an array
        selections[0] = fingers;

        //Put Color in the box
        aux = GameObject.Find(selections[0].ToString());
        aux.GetComponent<MeshRenderer>().material = aux.transform.GetChild(1).GetComponent<MeshRenderer>().material;
        aux.transform.GetChild(0).GetComponent<MeshRenderer>().material = aux.transform.GetChild(1).GetComponent<MeshRenderer>().material;

        //Identify that the box is already ticked
        vector[fingers - 1] = 1;
    }

    void SecondChoice()
    {
        //Set the second choice on the board
        colorSelect2.GetComponent<MeshRenderer>().material = GameObject.Find(fingers.ToString()).transform.GetChild(1).GetComponent<MeshRenderer>().material;

        //numberSelection = -1;
        //Save the second choice in an array
        selections[1] = fingers;
        //Identify that the box is already ticked
        vector[fingers - 1] = 1;

        //Put Color in the box
        aux1 = GameObject.Find(selections[1].ToString());
        aux1.GetComponent<MeshRenderer>().material = aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material;
        aux1.transform.GetChild(0).GetComponent<MeshRenderer>().material = aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material;

        //Compare the two choices
        //If negative reset the two positions ticked during the choosing part
        if (aux1.transform.GetChild(1).GetComponent<MeshRenderer>().material.name != aux.transform.GetChild(1).GetComponent<MeshRenderer>().material.name)
        {
            vector[selections[0] - 1] = 0;
            vector[selections[1] - 1] = 0;

            //
            GameObject.Find(selections[0].ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
            GameObject.Find(selections[0].ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;

            GameObject.Find(selections[1].ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
            GameObject.Find(selections[1].ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        /*else
        {
            GameObject.Find("Board").GetComponent<ColorRandomMemory>().correct = true;
        }*/
        //Reset the time
        time = 0;
        //Identify that the next choice is the first agaion
        numberSelection = 1;
    }

    //Count the number of fingers up
    void fingerCount()
    {
        fingers = 0;

        //Index Fingers
        if (indexTip.transform.position.y > indexJoint.transform.position.y)
        {
            fingers+=1;
        }
        if (indexTip2.transform.position.y > indexJoint2.transform.position.y)
        {
            fingers += 1;
        }

        //Middle Fingers
        if (middleTip.transform.position.y > middleJoint.transform.position.y)
        {
            fingers += 1;
        }
        if (middleTip2.transform.position.y > middleJoint2.transform.position.y)
        {
            fingers += 1;
        }

        //Ring Fingers
        if (ringTip.transform.position.y > ringJoint.transform.position.y)
        {
            fingers += 1;
        }
        if (ringTip2.transform.position.y > ringJoint2.transform.position.y)
        {
            fingers += 1;
        }

        //Pinky Fingers
        if (pinkyTip.transform.position.y > pinkyJoint.transform.position.y)
        {
            fingers += 1;
        }
        if (pinkyTip2.transform.position.y > pinkyJoint2.transform.position.y)
        {
            fingers += 1;
        }

        //Right Thumb
        if (thumbTip.transform.position.x < thumbJoint.transform.position.x)
        {
            fingers += 1;
        }

        //Left Thumb
        if (thumbTip2.transform.position.x > thumbJoint2.transform.position.x)
        {
            fingers += 1;
        }
    }

    void EndPuzzle()
    {
        int count = 0;

        for(int i = 0; i < 10; i++)
        {
            if(vector[i] == 1)
            {
                count++;
            }
        }

        if (count == 10)
        {
            for (int i = 0; i < 10; i++)
            {
                vector[i] = 0;
            }

            GameObject.Find("Board").GetComponent<ColorRandomMemory>().correct = true;
        }      
    }
}
