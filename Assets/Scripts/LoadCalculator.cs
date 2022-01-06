using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadCalculatorGame()
    {
        SceneManager.LoadScene("Calculator");
    } 
}
