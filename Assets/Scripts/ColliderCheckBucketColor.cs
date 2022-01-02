using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColliderCheckBucketColor : MonoBehaviour
{
    GameObject droppedObject;
    //GameObject Counter;

    public GameObject cup, cup1;

    GameObject find, clone;
    GameObject cameras;
    Points points;

    public Text Text;

    public string color;

    public float x1, y1, z1, x2, y2, z2;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        //Get the Main Camera object from the scene to access to the points variable
        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();

    }

    // Update is called once per frame
    void Update()
    {
        //Update the points in the screen
        Debug.Log("Points: " + points.point);
        Text.GetComponent<Text>().text = "Points: " + points.point;

        //Keep the buckets always at these coordinates to not be affeted by collisions
        cup.transform.position = new Vector3(x1, y1, z1);
        cup1.transform.localPosition = new Vector3(x2, y2, z2);
        cup.transform.eulerAngles = new Vector3(0, 0, 0);
        cup1.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    //Function to check the collision with the cubes
    private void OnTriggerEnter(Collider other)
    {
        droppedObject = other.gameObject;
        find = GameObject.Find("Cube");
        if ( other.gameObject.tag == color)
        {
            points.point += 1;
            //Creation of a new Cube
            clone = Instantiate(find);
            Destroy(droppedObject);
            clone.name = "Cube"; 

        }
        else if(find == droppedObject && other.gameObject.tag != color)
        {
            //Put the cube back into the initial position
            if (scene.name == "Level2_Left")
            {
                other.gameObject.transform.position = new Vector3(-10.16f, -5.72f, 1.8f);
            }
            else
            {
                other.gameObject.transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
            }
            
            Debug.Log("Wrong Bucket!");

        }
    }
}

