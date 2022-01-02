using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandAffected : MonoBehaviour
{
    public string handAffected;

    public GameObject left, right, button;

    string hand;

    public void StoreHandAffected()
    {
        if(left.GetComponent<Toggle>().isOn == true )
        {
            handAffected = "L";
            //hand = "Left";
        }

        if (right.GetComponent<Toggle>().isOn == true)
        {
            handAffected = handAffected + "R";
            //hand = "Right"
        }

        GameObject.Find("Main Camera").GetComponent<Calibration>().hand1 = handAffected;//hand;

        Destroy(left);
        Destroy(right);
        Destroy(button);
    }
}
