using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public string username;
    public GameObject inputField, deletion;
    public GameObject button;

    public void StoreUsername()
    {
        username = inputField.GetComponent<Text>().text;
        GameObject.Find("Main Camera").GetComponent<Calibration>().user1 = username;
        Destroy(deletion);
        Destroy(button);
    }
}
