using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRandomMemory : MonoBehaviour
{
    public GameObject c1, c2, c3, c4, c5, c6, c7, c8, c9, c10;//, c11, c12;
    public Material red, yellow, blue, black, green, defaultMaterial;

    GameObject[] objectsArray;

    public int[] colors;

    public static bool check;

    public bool correct;

    Points points;

    GameObject cameras;

    public Text Text;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
        //Create give colors randomly to the squares
        colors = new int[12];

        colors = Randomizer(5);

        AssociateColors(5);

        correct = false;

        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if(correct)
        {
            points.point += 1;

            check = false;

            resetGame();

            colors = Randomizer(5);

            AssociateColors(5);
            
            correct = false;
        }

        Text.GetComponent<Text>().text = "Points: " + points.point;
    }

    int[] Randomizer(int size)
    {
        //1-Red 2-Yellow 3-Blue 4-Black 5-Green
        int[] n = new int[2*size];
        bool flag = true;
        int number = 0;
        //int[] check = new int[6];

        for (int i = 0; i < 2*size; i++)
        {
            n[i] = 0;
        }

        for (int i = 1; i < size+1; i++)
        {
            //flag = true;
            for (int j = 0; j < 2; j++)
            {
                while (flag)
                {
                    number = Random.Range(0, 2*size);
                    //Debug.Log(number + " " + i + " " + j + " " + n[number]);
                    if (n[number] == 0)
                    {
                        n[number] = i;
                        flag = false;
                    }
                }
                flag = true;
            }
        }

        return n;
    }

    private void changeMaterial(int size, GameObject[] objectsArray)
    {
        for (int i = 0; i < 2 * size; i++)
        {
            if (colors[i] == 1)
            {
                //objectsArray[i].GetComponent<MeshRenderer>().material = red;
                objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = red;
                objectsArray[i].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = red;
            }
            else if (colors[i] == 2)
            {
                //objectsArray[i].GetComponent<MeshRenderer>().material = yellow;
                objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = yellow;
                objectsArray[i].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = yellow;
            }
            else if (colors[i] == 3)
            {
                //objectsArray[i].GetComponent<MeshRenderer>().material = blue;
                objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = blue;
                objectsArray[i].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = blue;
            }
            else if (colors[i] == 4)
            {
                //objectsArray[i].GetComponent<MeshRenderer>().material = black;
                objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = black;
                objectsArray[i].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = black;
            }
            else if (colors[i] == 5)
            {
                //objectsArray[i].GetComponent<MeshRenderer>().material = green;
                objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = green;
                objectsArray[i].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = green;
            }
        }
    }

    void resetMaterial()//int size, GameObject[] objectsArray)
    {
        for (int i = 0; i < 2 * 5; i++)
        {
            objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        check = true;
    }

    void resetGame()
    {
        for (int i = 0; i < 2 * 5; i++)
        {
            GameObject.Find((i+1).ToString()).GetComponent<MeshRenderer>().material = defaultMaterial;
            GameObject.Find((i+1).ToString()).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        //check = true;

        GameObject.Find("Choice1").GetComponent<MeshRenderer>().material = defaultMaterial;
        GameObject.Find("Choice2").GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    void AssociateColors(int size)
    {
        //1-Red 2-Yellow 3-Blue 4-Black 5-Green
        objectsArray = new GameObject[10];
        objectsArray[0] = c1;
        objectsArray[1] = c2;
        objectsArray[2] = c3;
        objectsArray[3] = c4;
        objectsArray[4] = c5;
        objectsArray[5] = c6;
        objectsArray[6] = c7;
        objectsArray[7] = c8;
        objectsArray[8] = c9;
        objectsArray[9] = c10;

        changeMaterial(size, objectsArray);

        Invoke("resetMaterial", 4);

        //check = true;

        //resetMaterial(size, objectsArray);

        //for (int i = 0; i < 2*size; i++)
        //{
            //if(colors[i] == 1)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = red;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = red;
            //}
            //else if (colors[i] == 2)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = yellow;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = yellow;
            //}
            //else if (colors[i] == 3)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = blue;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = blue;
            //}
            //else if (colors[i] == 4)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = black;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = black;
            //}
            //else if (colors[i] == 5)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = purple;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = purple;
            //}
            //else if (colors[i] == 6)
            //{
                ////objectsArray[i].GetComponent<MeshRenderer>().material = green;
                //objectsArray[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = green;
            //}
        //}
    }

    
}
