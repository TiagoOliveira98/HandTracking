using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachingTest : MonoBehaviour
{
    //public GameObject grabPoint;
    [SerializeField] float minZ, maxZ, minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update the minimum value of X
        if(transform.position.x < minX)
        {
            minX =transform.position.x;
        }

        //Update the maximum value of X
        if (transform.position.x > maxX)
        {
            maxX = transform.position.x;
        }

        //Update the minimum value of Z
        if (transform.position.z < minZ)
        {
            minZ = transform.position.z;
        }

        //Update the maximum value of Z
        if (transform.position.z > maxZ)
        {
            maxZ = transform.position.z;
        }
    }
}
