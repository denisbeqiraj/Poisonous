using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DialogController : MonoBehaviour
{
    private FileInfo theSourceFile = null;
    private StreamReader reader = null;
    private string text = null; // assigned to allow first line to be read below 

    private string file = null;
    private string path = "Assets/Text/";

    private int numLines = 4;

    public UnityEngine.UI.Text textDialog;

    // Start is called before the first frame update
    void Start()
    {
        /*var sr = File.CreateText("Search.txt");
        sr.WriteLine("This is my file.");*/
        textDialog.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setFile(string name)
    {
        file = name;

        theSourceFile = new FileInfo(path + file);
        reader = theSourceFile.OpenText();
        talk(numLines);
    }

    public void talk(int numLines) {
        int i = 0;
        string line = "line";

        while(i < numLines && line != null)
        {
            line = reader.ReadLine();
            if(line != null)
            {
                text += line + '\n';
            }
            
            i++;
        }

        if(text != null)
        {
            textDialog.enabled = true;
            textDialog.text = text;
            text = null;
        }
        else
        {
            textDialog.enabled = false;
            reader.Close();
            file = null;
        }  
    }

    public Boolean isReaderNull()
    {
        return reader == null ? true : false;
    }

    public string getFile()
    {
        return file;
    }
}
