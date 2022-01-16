using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject background;
    public GameObject calculatorButton1, calculatorButton2;
    public GameObject grabbingButton1, grabbingButton2;
    public GameObject followButton1, followButton2;
    public GameObject quitButton1, quitButton2;
    public GameObject menuText, textChoosing;
    public GameObject button4_1, button4_2;

    public GameObject colliderBucket;

    public GameObject cameras;
    bool escDown;
    // Start is called before the first frame update
    void Start()
    {
        escDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (cameras.GetComponent<Calibration>().timerIsRunning == false && colliderBucket.GetComponent<ColliderCheck>().menuOn == true)
        if(GameObject.Find("Bucket") == null )
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                if (escDown == false)
                {
                    escDown = true;
                    cameras.GetComponent<Camera>().fieldOfView = 0.5f;

                    background.GetComponent<RawImage>().enabled = true;

                    calculatorButton1.GetComponent<Image>().enabled = true;
                    calculatorButton1.GetComponent<Button>().enabled = true;
                    calculatorButton2.GetComponent<Text>().enabled = true;

                    grabbingButton1.GetComponent<Image>().enabled = true;
                    grabbingButton1.GetComponent<Button>().enabled = true;
                    grabbingButton2.GetComponent<Text>().enabled = true;

                    followButton1.GetComponent<Image>().enabled = true;
                    followButton1.GetComponent<Button>().enabled = true;
                    followButton2.GetComponent<Text>().enabled = true;

                    quitButton1.GetComponent<Image>().enabled = true;
                    quitButton1.GetComponent<Button>().enabled = true;
                    quitButton2.GetComponent<Text>().enabled = true;

                    if(SceneManager.GetActiveScene().name == "main")
                    {
                        button4_1.GetComponent<Image>().enabled = true;
                        button4_1.GetComponent<Button>().enabled = true;
                        button4_2.GetComponent<Text>().enabled = true;
                    }

                    menuText.GetComponent<Text>().enabled = true;
                    textChoosing.GetComponent<Text>().enabled = true;
                }
                else
                {
                    escDown = false;
                    cameras.GetComponent<Camera>().fieldOfView = 60f;

                    background.GetComponent<RawImage>().enabled = false;

                    calculatorButton1.GetComponent<Image>().enabled = false;
                    calculatorButton1.GetComponent<Button>().enabled = false;
                    calculatorButton2.GetComponent<Text>().enabled = false;

                    grabbingButton1.GetComponent<Image>().enabled = false;
                    grabbingButton1.GetComponent<Button>().enabled = false;
                    grabbingButton2.GetComponent<Text>().enabled = false;

                    followButton1.GetComponent<Image>().enabled = false;
                    followButton1.GetComponent<Button>().enabled = false;
                    followButton2.GetComponent<Text>().enabled = false;

                    quitButton1.GetComponent<Image>().enabled = false;
                    quitButton1.GetComponent<Button>().enabled = false;
                    quitButton2.GetComponent<Text>().enabled = false;

                    menuText.GetComponent<Text>().enabled = false;
                    textChoosing.GetComponent<Text>().enabled = false;
                }
            }
        }
    }
}
