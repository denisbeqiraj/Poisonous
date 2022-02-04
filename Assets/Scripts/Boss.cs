using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int life = 500;
    [SerializeField] private GameObject sharedStats;

    // Start is called before the first frame update
    void Start()
    {
        switch (sharedStats.GetComponent<SharedStats>().getDifficulty())
        {
            case "EASY":
                life = 500;
                break;
            case "HARD":
                life = 1000;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit(int damage)
    {
        life = life - damage;
    }
}
