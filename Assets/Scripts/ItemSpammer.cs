using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpammer : MonoBehaviour
{

    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObject, player.transform.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
