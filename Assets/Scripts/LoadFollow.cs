using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadFollowGame()
    {
        SceneManager.LoadScene("Follow");
    }
}
