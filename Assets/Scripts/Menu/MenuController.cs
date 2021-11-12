using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject options;
    public GameObject settings;
    public GameObject help;

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void openObject(string name)
    {
        switch (name)
        {
            case "Settings":
                options.SetActive(false);
                settings.SetActive(true);
                break;

            case "Help":
                options.SetActive(false);
                help.SetActive(true);
                break;
        }
    }

    public void closeObject(string name)
    {
        switch (name)
        {
            case "Settings":
                settings.SetActive(false);
                options.SetActive(true);
                break;

            case "Help":
                help.SetActive(false);
                options.SetActive(true);
                break;
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
