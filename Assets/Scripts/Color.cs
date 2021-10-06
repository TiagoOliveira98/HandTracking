using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    public Material blue, green;
    int number;
    // Start is called before the first frame update
    void Start()
    {
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

        transform.position = new Vector3(0.6f, -5.67f, 1.8f);
    }
}
