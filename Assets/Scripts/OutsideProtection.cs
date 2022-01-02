using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideProtection : MonoBehaviour
{
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -11.96f || transform.position.x > 2.0f || transform.position.z < -9.95f || transform.position.z > 10.6f )
        {
            if (scene.name == "Level2_Left")
            {
                transform.position = new Vector3(-10.16f, -5.72f, 1.8f);
            }
            else
            {
                transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
            }
            //transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
        }

        if(transform.position.y < -9)
        {
            if (scene.name == "Level2_Left")
            {
                transform.position = new Vector3(-10.16f, -5.72f, 1.8f);
            }
            else
            {
                transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
            }
            //transform.position = new Vector3(0.6000002f, -7.99f, 1.8f);
        }
    }
}
