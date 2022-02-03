using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMesh;
    public GameObject[] player;
    private TargetBehaviour target;
    public Animator animator;

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject leg;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GetComponent<TargetBehaviour>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
        Invoke("Despawn", 2);
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

        Vector3 pos = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);

        if (Physics.Raycast(pos, transform.forward, out raycastHit, 1))
        {
            StartCoroutine(hitted(raycastHit));
        }
        else{
            pos = new Vector3(leg.transform.position.x, leg.transform.position.y, leg.transform.position.z);

            if (Physics.Raycast(pos, transform.forward, out raycastHit, 1))
            {
                StartCoroutine(hitted(raycastHit));
            }
        }
    }

    private IEnumerator hitted(RaycastHit raycastHit)
    {
        if (raycastHit.transform.tag.Equals("Player"))
        {
            TargetBehaviour player = raycastHit.transform.GetComponent<TargetBehaviour>();

            yield return new WaitForSeconds(3);

            player.Hit(10);

        }

        if (raycastHit.transform.tag.Contains("Build"))
        {
            BuildHittable build = raycastHit.transform.GetComponent<BuildHittable>();

            yield return new WaitForSeconds(3);

            if(build != null) {
                build.hit(30);
            }
        }
    }

    void Despawn()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Vector3.down, 1000);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform.name.Contains("Terrain") && hit.distance > 20)
            {
                Destroy(gameObject);
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
