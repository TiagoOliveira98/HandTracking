using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        //if (EditorApplication.isPlaying)
        //{
            //UnityEditor.EditorApplication.isPlaying = false;
        //}
        //else
        //{
            Application.Quit();
        //}  
    }
}
