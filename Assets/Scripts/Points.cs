using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{
    public int point;

    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        //Start the points with 0
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //To keep the coordinates from the plane
        plane.transform.position = new Vector3(-1.966059f, -8.89f, -5.946551f);
    }
}
