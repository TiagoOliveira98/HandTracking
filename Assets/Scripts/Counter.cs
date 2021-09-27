using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private float updateNSeconds = 0.25f;
    private float lastUpdateTime = 0f;

    public Text scoreText;
    int scoreCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreCounter.ToString();
    }

    /*private void OnGUI()
    {
        s = i.ToString();
        GUI.Label(Rect(0, 0, 100, 100), s);
    }*/
}
