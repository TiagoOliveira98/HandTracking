using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject wristR, knuckle1R, knuckle2R, knuckle3R;
    public GameObject wristL, knuckle1L, knuckle2L, knuckle3L;

    public GameObject cubeR, cubeL;
    public float speed = 10f;

    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-4.92353f, -4f, -2f);
        //target = new Vector3(Random.Range(-12.9f, 2.3f), Random.Range(-7.25f, 0.3f), -2f);
        if (tag == "right")
        {
            //target = new Vector3(Random.Range(-5.8f, 2.3f), Random.Range(-7.25f, 0.3f), -2f);
            target = new Vector3(Random.Range(-5.8f, -0.5f), Random.Range(-7.25f, -2f), -2f);
            cubeR.transform.localPosition = target;
        }
        else if (tag == "left")
        {
            //target = new Vector3(Random.Range(-12.9f ,-5.8f), Random.Range(-7.25f, 0.3f), -2f);
            target = new Vector3(Random.Range(-9f, -5.8f), Random.Range(-7.25f, -2f), -2f);
            cubeL.transform.localPosition = target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Catch();

        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            // Swap the position of the cylinder.
            //float x = Random.Range(-12.9f, 2.3f);
            float x;

            if(tag == "right")
            {
                //x = Random.Range(-5.8f, 2.3f);
                x = Random.Range(-5.8f, -0.5f);
            }
            else
            {
                //x = Random.Range(-12.9f, -5.8f);
                x = Random.Range(-9f, -5.8f);
            }

            //float y = Random.Range(-7.25f, 0.3f);
            float y = Random.Range(-7.25f, -2f);

            target = new Vector3(x, y, -2f);
            if(tag == "right")
            {
                cubeR.transform.localPosition = target;
            }
            else if(tag == "left")
            {
                cubeL.transform.localPosition = target;
            }
        }
    }

    //Verify if the hand is on the dot
    void Catch()
    {
        if (tag == "right")
        {
            if (cubeR.transform.position.y < knuckle2R.transform.position.y && cubeR.transform.position.y > wristR.transform.position.y)
            {
                if ((cubeR.transform.position.x > knuckle1R.transform.position.x && cubeR.transform.position.x < knuckle3R.transform.position.x) || (cubeR.transform.position.x < knuckle1R.transform.position.x && cubeR.transform.position.x > knuckle3R.transform.position.x))
                {
                    //DO STUFF
                    //STILL MINOR TWEAKS TO DO
                    //DO THE MOVEMENT OF THE BALL BETWEEN THE LAST TARGET AND THE NEW TARGET
                    transform.position = target;

                    float x = Random.Range(-5.8f, -0.5f);
                    float y = Random.Range(-7.25f, -2f);
                    target = new Vector3(x, y, -2f);
                    cubeR.transform.localPosition = target;

                    //Add Points
                    //Maybe add multipliers

                }
            }
        }
        else if(tag == "left")
        {
            if (cubeL.transform.position.y < knuckle2L.transform.position.y && cubeR.transform.position.y > wristL.transform.position.y)
            {
                if ((cubeL.transform.position.x < knuckle1L.transform.position.x && cubeL.transform.position.x > knuckle3L.transform.position.x) || (cubeL.transform.position.x > knuckle1L.transform.position.x && cubeL.transform.position.x < knuckle3L.transform.position.x))
                {
                    //DO STUFF
                    //STILL MINOR TWEAKS TO DO
                    //DO THE MOVEMENT OF THE BALL BETWEEN THE LAST TARGET AND THE NEW TARGET
                    transform.position = target;

                    float x = Random.Range(-9f, -5.8f);
                    float y = Random.Range(-7.25f, -2f);
                    target = new Vector3(x, y, -2f);
                    cubeL.transform.localPosition = target;

                    //Add Points
                    //Maybe add multipliers

                }
            }
        }
    }
}