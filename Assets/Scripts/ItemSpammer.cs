using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpammer : MonoBehaviour
{

    [SerializeField] private GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObject, transform.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
