using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Color : MonoBehaviour
{
    public Material blue, green;
    int number;

    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        number = Random.Range(0, 2);
        if(number == 0)
        {
            GetComponent<MeshRenderer>().material = blue;
            tag = "blue";
        }
        else if(number == 1)
        {
            GetComponent<MeshRenderer>().material = green;
            tag = "green";
        }

        if (scene.name == "Level2_Left")
        {
            transform.position = new Vector3(-10.16f, -5.72f, 1.8f);
        }
        else
        {
            transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
        }

        //transform.position = new Vector3(0.6f, -5.67f, 1.8f);
    }
}
