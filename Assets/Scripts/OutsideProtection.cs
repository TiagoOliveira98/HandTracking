using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideProtection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -11.96f || transform.position.x > 2.0f || transform.position.z < -9.95f || transform.position.z > 10.6f )
        {
            transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
        }

        if(transform.position.y < -9)
        {
            transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
        }
    }
}
