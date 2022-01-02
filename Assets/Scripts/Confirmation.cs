using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Confirmation : MonoBehaviour
{
    GameObject cameras;
    Points points;

    //Debug
    public string[] auxString;
    public int sizeDebug;

    public Text Text;

    public Text lives;

    public int size;

    public string heart;

    //Variable with the initial equation created by the Calculator script
    public static string initial;

    //Variable that stores the current answer to be confirmed
    public static string answer;

    //Variable to store the solution that it is got from Calculator script
    public string solution;

    //Variable that signals that the answer was correct or not. Serves to communicate with the Calculator script to create a new equation
    public static bool correct;

    //GameObject for the screen of the calculator
    public GameObject screen;

    //Variable that allows the confoirmation of new answers
    public bool canConfirm;

    string latest;

    // Start is called before the first frame update
    void Start()
    {
        //initial = null;
        //Initialize variables
        answer = "";
        solution = null;
        correct = false;
        canConfirm = false;

        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();

        //latest = Text.GetComponent<Text>().text;
    } 

    // Update is called once per frame
    void Update()
    {
        Text.GetComponent<Text>().text = "Points: " + points.point;
    }

    public void StartIn()
    {
        GetComponent<Collider>().enabled = false;

        transform.localPosition = new Vector3(transform.localPosition.x, -0.2f, transform.localPosition.z);

        Invoke("ResetIn", 3);
    }

    public void ResetIn()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Verify if there was a collsion with a finger tip and if there is already input to confirm
        if ((other.name == "INDEX_FINGER_TIP" || other.name == "INDEX_FINGER_TIP2") && canConfirm == true)
        {
            //Get the solution to the problem created in teh Calculator object script
            solution = GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().solution;

            StartIn();

            //Case if the answer and solution are the same
            if (answer == GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().solution)
            {
                GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().correct = true;
                points.point += 1;

                //Debug.Log(answer);
                //Reset the answer
                answer = "";
                //screen.GetComponent<TextMesh>().text = "Correct";
                //Render another operation

                //Give a Point or somethig similar/ Progress bar
                //correct = true;

                //Text.GetComponent<Text>().text = latest;

            }
            else
            {
                screen.GetComponent<TextMesh>().text = GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().initial;
                //Debug.Log(answer);
                //Debug.Log(solution);
                Text aux = lives.GetComponent<Text>();
                size = aux.text.Length;

                //Reset the answer
                answer = "";
                //Render the same operation
                //Maybe set a group of 3 hearts and take one of them out for each wrong answer
                if(aux.text.Length == 1)
                {
                    //No more lives left
                    //Reset points
                    //points.point = 0;

                    //Test
                    GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().correct = true;

                    points.point = 0;
                    aux.text = "";
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 0)
                            aux.text += heart;
                        else
                        {
                            aux.text += ' ';
                            aux.text += heart;
                        }
                    }
                    auxString = null;
                }
                else
                {
                    //aux.text = aux.text.Remove(aux.text.Length - 3);
                    //aux.text = string.Replace(heart, aux.text);
                    //TEST
                    auxString = aux.text.Split(' ');
                    sizeDebug = auxString.Length;

                    //if(auxString.Length > 1)
                    //{
                        aux.text = "";
                        for(int i = 0; i < auxString.Length - 1; i++)
                        {
                            if(i == 0)
                                aux.text += heart;
                            else
                            {
                                aux.text += ' ';
                                aux.text += heart;
                            }
                            
                        }
                    //}
                    //else
                    //{
                    //    points.point = 0;
                    //    aux.text = "";
                    //    for (int i = 0; i < 5; i++)
                    //    {
                    //        if (i == 0)
                    //            aux.text += heart;
                    //        else
                    //        {
                    //            aux.text += ' ';
                    //            aux.text += heart;
                    //        }
                    //    }
                    //    auxString = null;
                    //}
                }

            }
            //Disable confirmations after entering aan answer
            canConfirm = false;
        }
    }
}
