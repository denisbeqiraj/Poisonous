using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpammer : MonoBehaviour
{

    [SerializeField] private GameObject bananaObj;
    [SerializeField] private Terrain terrain;
    [SerializeField] private int numItems;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 terrainPos = terrain.transform.position;
        Debug.Log("Pos " + terrainPos);
        Vector3 terrainSize = terrain.terrainData.size;
        Debug.Log("Size " + terrainSize);
        float maxX = terrainSize.x;
        float maxY = terrainSize.y;
        float maxZ = terrainSize.z;

        Vector3 pos = new Vector3(terrainPos.x, maxY / 4, terrainPos.z);

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


        for(int i=0; i<numItems; i++)
        {

            pos.x = minX + Random.Range(0.0f, maxX);
            pos.z = minZ + Random.Range(0.0f, maxZ);

            Instantiate(bananaObj, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
