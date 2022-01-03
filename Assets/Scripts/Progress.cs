using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    Points points;

    GameObject cameras;

    public float goal;

    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.Find("Main Camera");
        points = cameras.GetComponent<Points>();

        goal = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float n = (float)points.point;
        GetComponent<RectTransform>().anchorMax = new Vector2(n / goal, GetComponent<RectTransform>().anchorMax.y);
    }
}
