using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public GameObject screen;

    public void StartIn()
    {
        //StartCoroutine("waiter");
        GetComponent<Collider>().enabled = false;

        //Vector3 v = transform.position;
        //v.y = -0.2f;
        //transform.position = new Vector3(transform.position.x, -0.2f, transform.position.z);
        transform.localPosition = new Vector3(transform.localPosition.x, -0.2f, transform.localPosition.z);


        Invoke("ResetIn", 3);
        //yield return new WaitForSecondsRealtime(5);

        //v.y = 0f;
        //GetComponent<Collider>().enabled = true;
    }

    public void ResetIn()
    {
        //Vector3 v = transform.position;

        //v.y = 0f;
        transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "INDEX_FINGER_TIP" || other.name == "INDEX_FINGER_TIP2")
        {
            StartIn();

            //string a = GameObject.Find("Confirmation").gameObject.GetComponent<Confirmation>().initial;
            screen.GetComponent<TextMesh>().text = GameObject.Find("Calculator").gameObject.GetComponent<Calculator>().initial;
            //Debug.Log(a);
            //screen.GetComponent<TextMesh>().text = Confirmation.initial;
            Confirmation.answer = "";
        }
    }
}
