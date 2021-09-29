using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static int point;
    // Start is called before the first frame update
    void Start()
    {
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Points: " + point);
    }
}
