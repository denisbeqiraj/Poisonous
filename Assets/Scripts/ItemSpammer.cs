using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpammer : MonoBehaviour
{

    [SerializeField] private GameObject bananaObj;
    [SerializeField] private GameObject hamburgerObj;
    [SerializeField] private GameObject weapon1Obj;
    [SerializeField] private GameObject ammo1Obj;
    [SerializeField] private Terrain terrain;
    [SerializeField] private int numItems;

    private static int numItemAvailable = 2;
    private GameObject[] items = new GameObject[numItemAvailable];

    // Start is called before the first frame update
    void Start()
    {
        Vector3 terrainPos = terrain.transform.position;

        Vector3 terrainSize = terrain.terrainData.size;

        float maxX = terrainSize.x;
        float maxY = terrainSize.y;
        float maxZ = terrainSize.z;

        Vector3 pos = new Vector3(terrainPos.x, maxY / 12, terrainPos.z);

        //DIMENSIONI MASSIME QUADRATO TERRAIN

        /*pos = new Vector3(terrainPos.x, 10, terrainPos.z);
        Instantiate(bananaObj, pos, Quaternion.identity);

        pos = new Vector3(terrainPos.x + terrainSize.x, 10, terrainPos.z);
        Instantiate(bananaObj, pos, Quaternion.identity);

        pos = new Vector3(terrainPos.x + terrainSize.x, 10, terrainPos.z + terrainSize.z);
        Instantiate(bananaObj, pos, Quaternion.identity);

        pos = new Vector3(terrainPos.x, 10, terrainPos.z + terrainSize.z);
        Instantiate(bananaObj, pos, Quaternion.identity);*/

        //zone sicurezza
        float minX = terrainPos.x + 10;
        float minZ = terrainPos.z + 10;
        maxX = maxX - 10;
        maxZ = maxZ - 10;

        items[0] = bananaObj;
        items[1] = hamburgerObj;
        /*items[2] = weapon1Obj;
        items[3] = ammo1Obj;*/

        int rnd;

        for (int i=0; i<numItems; i++)
        {

            pos.x = minX + Random.Range(0.0f, maxX);
            pos.z = minZ + Random.Range(0.0f, maxZ);

            rnd = (int)Random.Range(0.0f, numItemAvailable);

            Instantiate(items[rnd], pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
