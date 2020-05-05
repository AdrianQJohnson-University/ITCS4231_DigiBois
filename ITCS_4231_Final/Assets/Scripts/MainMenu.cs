using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown("c"))
        {
            SceneManager.LoadScene("Credits");
        }
        else if (Input.GetKeyDown("d"))
        {
            SceneManager.LoadScene("Description");
        }
    }
}
