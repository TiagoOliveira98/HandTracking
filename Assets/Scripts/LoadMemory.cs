using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMemory : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMemoryGame()
    {
        SceneManager.LoadScene("MemoryGame");
    }
}
