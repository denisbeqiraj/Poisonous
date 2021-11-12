using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{

    private bool isAlive;
    public Collider mainController;
    public Collider[] allColliders;
    public Animator animator;
    public int health;
    void Awake()
    {
        mainController = GetComponent<Collider>();
        allColliders = GetComponentsInChildren<Collider>(true);
        isAlive = true;
        health = 100;
    }

    void Update()
    {
        
    }

    public void Die()
    {
        Animator children = GetComponentInChildren<Animator>();
        DoRagdoll(true);
        StartCoroutine("DieTarget");
    }

    public void Hit(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public IEnumerator DieTarget()
    {
        isAlive = false;
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    void DoRagdoll(bool isRagdoll)
    {
        foreach(var col in allColliders)
        {
            col.enabled = isRagdoll;
        }
        mainController.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}