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

    //public int points;

    // Start is called before the first frame update
    void Start()
    {
        //points = 0;
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
            Points.point += 1;
            //points += 1;
            Destroy(droppedObject);
            SceneManager.LoadScene("Level2");
        }
        else 
        { 
            //Destruy the object and relocate it to teh initial position
        }
    }
}
