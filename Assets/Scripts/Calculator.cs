using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public string initial;
    public string solution;
    public bool correct;

    public GameObject screen;

    // Start is called before the first frame update
    void Start()
    {
        //solution = "15";
        //initial = "3 x 5 = __";
        //Confirmation.initial = initial;
        correct = true;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Confirmation").gameObject.GetComponent<Confirmation>().solution = solution;
        //GameObject.Find("Confirmation").gameObject.GetComponent<Confirmation>().initial = initial;
        if(correct == true)
        {
            initial = GenerateEquation();
            screen.GetComponent<TextMesh>().text = initial;
            correct = false;
        }
    }

    string GenerateEquation()
    {
        int num1, num2, ans;
        //string str;
        
        //1 -> Multiplication; 2 -> Addition; 3 -> Subtraction
        int operation = Random.Range(1, 4);
        if(operation == 1)
        {
            num1 = Random.Range(0, 10);
            num2 = Random.Range(0, 10);

            ans = num1 * num2;
            solution = ans.ToString();

            return (CreateString(num1.ToString(), num2.ToString(), ans, 'x'));
        }
        else if(operation==2)
        {
            num1 = Random.Range(0, 40);
            num2 = Random.Range(0, 40);
            ans = num1 + num2;
            solution = ans.ToString();

            return (CreateString(num1.ToString(), num2.ToString(), ans, '+'));
        }
        else
        {
            int aux = Random.Range(0, 40);
            int aux2 = Random.Range(0, 40);
            if(aux >= aux2)
            {
                num1 = aux;
                num2 = aux2;
            }
            else
            {
                num1 = aux2;
                num2 = aux;
            }

            ans = num1 - num2;
            solution = ans.ToString();
            string n1 = num1.ToString();
            string n2 = num2.ToString();

            return (CreateString(n1, n2, ans, '-'));
        }

    }

    string CreateString(string n1, string n2, int ans, char opt)
    {
        string str;

        if (ans < 10)
        {

            str = n1 + " " + opt + " " + n2 + " =  _";
        }
        else
        {
            str = n1 + " " + opt + " " + n2 + " =  __";
        }

        return str;
    }
}
