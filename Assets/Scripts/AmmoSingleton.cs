using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSingleton : MonoBehaviour
{
    public static AmmoSingleton instance;
    public static int total;
    public GameObject gunAmmo;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        total = gunAmmo.GetComponent<Launcher>().getTotal();
    }
}
