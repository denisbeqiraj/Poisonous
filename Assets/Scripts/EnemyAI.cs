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

        RaycastHit raycastHit;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Physics.Raycast(pos, transform.forward, out raycastHit, 10))
        {

            Debug.Log("Hit");
            Debug.Log(raycastHit.transform.name);

            if (raycastHit.transform.tag.Equals("Player"))
            {
                Debug.Log("Player");
                TargetBehaviour player = raycastHit.transform.GetComponent<TargetBehaviour>();

                player.Hit(10);

            }

            if (raycastHit.transform.tag.Contains("Build"))
            {
                Debug.Log("Build");
                BuildHittable build = raycastHit.transform.GetComponent<BuildHittable>();

                build.hit(30);
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.tag.Equals("Player")){
            Debug.Log("Player");
            TargetBehaviour player = collision.gameObject.GetComponent<TargetBehaviour>();

            player.Hit(10);

        }

        if (collision.gameObject.tag.Contains("Build"))
        {
            Debug.Log("Build");
            BuildHittable build = collision.gameObject.GetComponent<BuildHittable>();

            build.hit(30);
        }
    }*/
}
