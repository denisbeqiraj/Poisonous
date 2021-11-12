using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMesh;
    public GameObject[] player;
    private TargetBehaviour target;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GetComponent<TargetBehaviour>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Magnitude(transform.position - player[0].transform.position) < 10 && target.getIsAlive())
        {
            navMesh.isStopped = false;
            navMesh.destination = player[0].transform.position;
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
        }
    }
}
