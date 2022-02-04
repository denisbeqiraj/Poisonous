using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startRepeating()
    {
        InvokeRepeating("spawnEnemy", 5, 25);
    }

    public void spawnEnemy()
    {
        Vector3 position = transform.position;
        for (int i = 0; i < 100; i++)
        {
            RaycastHit raycastHit;
            Vector3 spawnPosition;
            spawnPosition.x = position.x + Random.Range(-17, 17);
            spawnPosition.y = position.y + Random.Range(0, 17);
            spawnPosition.z = position.z + Random.Range(-17, 17);
            if (Physics.Raycast(spawnPosition, spawnPosition - new Vector3(0, 10, 0), out raycastHit, 50))
            {
                if (raycastHit.transform.tag == "Terrain")
                {
                    Instantiate(zombie, spawnPosition, Quaternion.Euler(new Vector3(Random.Range(0, 360), 0, 0)));

                }
            }
        }
    }

    public void setEnemy(GameObject obj)
    {
        zombie = obj;
    }
}
