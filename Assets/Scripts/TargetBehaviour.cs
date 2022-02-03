using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBehaviour : MonoBehaviour
{

    private bool isAlive;
    public Collider mainController;
    public Collider[] allColliders;
    public Animator animator;
    public int health;

    [SerializeField] private MenuController menuController;
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
        
        if(gameObject.tag == "Enemy")
        {
            DoRagdoll(true);
            StartCoroutine(DieEnemy());
        }else if(gameObject.tag == "Player")
        {
            DiePlayer();
        }
    }

    public void Hit(int damage)
    {
        health = health - damage;
       
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<PlayerCharacter>().setLife(health);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void addLife(int life)
    {
        health += life;

        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<PlayerCharacter>().setLife(health);
        }
    }

    public IEnumerator DieEnemy()
    {
        isAlive = false;
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    public void DiePlayer()
    {
        isAlive = false;
        Screen.lockCursor = false;
        menuController.loadScene("LoseScene");
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