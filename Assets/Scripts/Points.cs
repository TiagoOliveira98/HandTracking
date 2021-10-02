using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int point;

    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Points: " + point);
        plane.transform.position = new Vector3(-1.966059f, -8.89f, -5.946551f);
    }
}
