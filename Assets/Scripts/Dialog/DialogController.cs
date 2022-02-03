using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DialogController : MonoBehaviour
{
    private FileInfo theSourceFile = null;
    private StreamReader reader = null;
    private string text = ""; // assigned to allow first line to be read below 

    private string file = "";

    private int numLines = 4;

    public UnityEngine.UI.Text textDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setFile(string name)
    {
        file = name;

        theSourceFile = new FileInfo(file);
        reader = theSourceFile.OpenText();
        printLines(numLines);
    }

    public void printLines(int numLines) {
        int i = 0;

        while(i < numLines && text != null)
        {
            text += reader.ReadLine();
            i++;
        }

        if(text != null)
        {
            textDialog.enabled = true;
            textDialog.text = text;
        }
        else
        {
            textDialog.enabled = false;
            file = "";
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
