using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHittable : MonoBehaviour
{
    [SerializeField] private int life = 150;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(int damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
