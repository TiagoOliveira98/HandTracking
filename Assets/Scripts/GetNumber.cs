using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityAsync;
using System.Threading.Tasks;


public class GetNumber : MonoBehaviour
{
    public GameObject screen;

    public GameObject enter;

    int index;

    string answer, str;

    // Start is called before the first frame update
    void Start()
    {
        //answer = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerable waiter()
    //{
    //    GetComponent<Collider>().enabled = false;
    //
    //    Vector3 v = transform.position;
    //    v.y = -0.2f;
    
    //    yield return new WaitForSecondsRealtime(5);
    
    //    v.y = 0f;
    //    GetComponent<Collider>().enabled = true;
    //}

    public void startIn()
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
        //Verify a collision with a finger tip
        if(other.name == "INDEX_FINGER_TIP" || other.name == "INDEX_FINGER_TIP2")
        {
            //get the index of the '_' in the screen to be able to change
            index = screen.GetComponent<TextMesh>().text.IndexOf("_");
            if(index != -1)
            {
                screen.GetComponent<TextMesh>().text = screen.GetComponent<TextMesh>().text.Substring(0, index) + name + screen.GetComponent<TextMesh>().text.Substring(index + 1);
                //answer += name; 
                //str += name;
                //Debug.Log("STR: " + str);
                //GameObject.Find("Confirmation").gameObject.GetComponent<Confirmation>().answer = str;
                //Debug.Log(GameObject.Find("Confirmation").gameObject.GetComponent<Confirmation>().answer);
                str = Confirmation.answer;
                Debug.Log("STR: " + str);
                answer = str + name;
                Debug.Log("ANSWER: " + answer);
                Confirmation.answer = answer ;

                //Block double click
                //GetComponent<Rigidbody>().detectCollisions = false;
                ////////GetComponent<Collider>().enabled = false;

                //Allow confirmations
                enter.GetComponent<Confirmation>().canConfirm = true;
                //Confirmation.canConfirm = true;

                //commented
                //StartCoroutine("waiter");
                startIn();
                
                
                //wait 2 seconds
                //float time = 0;
                //while(time < 5)
                //{
                    //time += Time.deltaTime;
                //}

                //Unblock collisions
                //GetComponent<Rigidbody>().detectCollisions = true;
                ///////GetComponent<Collider>().enabled = true;
            }          
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    StartCoroutine("waiter");
    //}

}
