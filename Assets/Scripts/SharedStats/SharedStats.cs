using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedStats : MonoBehaviour
{
    [SerializeField] private string difficulty = "EASY";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getDifficulty()
    {
        return difficulty;
    }
}
