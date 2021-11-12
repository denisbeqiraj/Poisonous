using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Camera camera;
    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnEnemy()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, 1000))
        {
            if (raycastHit.transform.tag == "Terrain")
            {
                Instantiate(zombie, raycastHit.point, Quaternion.identity);
            }
        }
    }
}
