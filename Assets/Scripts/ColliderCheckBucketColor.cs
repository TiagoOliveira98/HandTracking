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

    //public int points;

    // Start is called before the first frame update
    void Start()
    {
        //points = 0;
        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Points: " + points.point);
        Text.GetComponent<Text>().text = "Points: " + points.point;
        cup.transform.position = new Vector3(x1, y1, z1);
        cup1.transform.localPosition = new Vector3(x2, y2, z2);
        cup.transform.eulerAngles = new Vector3(0, 0, 0);
        cup1.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        droppedObject = other.gameObject;
        find = GameObject.Find("Cube");
        if (/*other.gameObject.tag == "Grabbable" ||*/ other.gameObject.tag == color)
        {
            //droppedObject = other.gameObject;
            //Points.point = Points.point + 1;
            points.point += 1;
            //find = GameObject.Find("Cube");
            clone = Instantiate(find);
            Destroy(droppedObject);
            clone.name = "Cube";

        }
        else if(find == droppedObject && other.gameObject.tag != color)
        {
            other.gameObject.transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
            Debug.Log("Wrong Bucket!");

        }
    }
}

