using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject options;
    public GameObject settings;
    public GameObject help;

    private string difficulty = "EASY";

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

    public void changeDifficulty()
    {
        GameObject difficultyButton = settings.transform.Find("DifficultyButton").gameObject;

        if (difficultyButton.GetComponentInChildren<Text>().text == "EASY")
        {
            difficulty = "HARD";
        }
        else
        {
            difficulty = "EASY";
        }

        difficultyButton.GetComponentInChildren<Text>().text = difficulty;
    }

    public void quit()
    {
        //working only in (.exe) game
        Application.Quit();
    }
}
