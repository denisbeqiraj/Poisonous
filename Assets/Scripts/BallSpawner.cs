using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject spawnBall;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ballSpawn", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ballSpawn()
    {
        Instantiate(spawnBall, gameObject.transform.position, gameObject.transform.rotation);
    }
}
