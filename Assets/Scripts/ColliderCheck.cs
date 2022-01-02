using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderCheck : MonoBehaviour
{
    GameObject droppedObject;
    //GameObject Counter;

    public GameObject cup, cup1;

    public string color;

    public float x1, y1, z1, x2, y2, z2;

    Scene scene;

    //public int points;

    // Start is called before the first frame update
    void Start()
    {
        //points = 0;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Points: " + points);
        cup.transform.position = new Vector3(x1, y1, z1);
        cup1.transform.localPosition = new Vector3(x2, y2, z2);
        cup.transform.eulerAngles = new Vector3(0, 0, 0);
        cup1.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grabbable" || other.gameObject.tag == color)
        {
            droppedObject = other.gameObject;
            //Points.point += 1;
            //points += 1;
            Destroy(droppedObject);

            //Randomize the task
            int rando = Random.Range(1,4);
            if(rando == 1)//Random.Range(1,3) == 1)
            {
                if (GameObject.Find("HandAffected").gameObject.GetComponent<HandAffected>().handAffected == "L")
                {
                    SceneManager.LoadScene("Level2_Left");
                }
                else if (GameObject.Find("HandAffected").gameObject.GetComponent<HandAffected>().handAffected == "R")
                {
                    SceneManager.LoadScene("Level2");
                    //SceneManager.LoadScene("Calculator");
                }
                else if(GameObject.Find("HandAffected").gameObject.GetComponent<HandAffected>().handAffected == "LR")
                {
                    if(Random.Range(1,3) == 1)
                    {
                        SceneManager.LoadScene("Level2_Left");
                    }
                    else 
                    {
                        SceneManager.LoadScene("Level2");
                    }
                }
            }
            //else
            else if(rando == 2)
            {
                SceneManager.LoadScene("Calculator");
            }
            else if(rando == 3)
            {
                SceneManager.LoadScene("MemoryGame");
            }

            ///if (GameObject.Find("HandAffected").gameObject.GetComponent<HandAffected>().handAffected == "L")
            ///{
                ///SceneManager.LoadScene("Level2_Left");
            ///}
            ///else if(GameObject.Find("HandAffected").gameObject.GetComponent<HandAffected>().handAffected == "R")
            ///{
                ///SceneManager.LoadScene("Level2");
                /////SceneManager.LoadScene("Calculator");
            ///}
            //Random se for LR
            //SceneManager.LoadScene("Level2");
        }
        else 
        {
            //Destroy the object and relocate it to teh initial position
            //if (scene.name == "Level2_Left")
            //{
                //other.gameObject.transform.position = new Vector3(-10.16f, -5.72f, 1.8f);
            //}
            //else
            //{
                //other.gameObject.transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
            //}
            other.gameObject.transform.position = new Vector3( 0.6000002f, -7.99f, 1.8f);

        }
    }
}
