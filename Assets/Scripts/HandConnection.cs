using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandConnection : MonoBehaviour
{
    public GameObject point1, point2, point3, point4, point5;

    float x1, x2, x3, x4, x5 = 0;
    float y1, y2, y3, y4, y5 = 0;
    float z1, z2, z3, z4, z5 = 0;

    Vector3 v1, v2, v3, v4, v5;

    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        v1 = new Vector3(x1, y1, z1);
        v2 = new Vector3(x2, y2, z2);
        v3 = new Vector3(x3, y3, z3);
        v4 = new Vector3(x4, y4, z4);
        v5 = new Vector3(x5, y5, z5);
    }

    // Update is called once per frame
    void Update()
    {
        x1 = point1.transform.position.x;
        y1 = point1.transform.position.y;
        z1 = point1.transform.position.z;

        x2 = point2.transform.position.x;
        y2 = point2.transform.position.y;
        z2 = point2.transform.position.z;

        x3 = point3.transform.position.x;
        y3 = point3.transform.position.y;
        z3 = point3.transform.position.z;

        x4 = point4.transform.position.x;
        y4 = point4.transform.position.y;
        z4 = point4.transform.position.z;

        x5 = point5.transform.position.x;
        y5 = point5.transform.position.y;
        z5 = point5.transform.position.z;

        v1.x = x1;
        v1.y = y1;
        v1.z = z1;
        v2.x = x2;
        v2.y = y2;
        v2.z = z2;
        v3.x = x3;
        v3.y = y3;
        v3.z = z3;
        v4.x = x4;
        v4.y = y4;
        v4.z = z4;
        v5.x = x5;
        v5.y = y5;
        v5.z = z5;


        lr.SetPosition(0, v1);
        lr.SetPosition(1, v2);
        lr.SetPosition(2, v3);
        lr.SetPosition(3, v4);
        lr.SetPosition(4, v5);
    }
}
