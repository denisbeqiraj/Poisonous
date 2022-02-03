using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMesh;
    public GameObject[] player;
    private float minDistance = 10;
    private TargetBehaviour target;
    public Animator animator;
    private float timeRemaining = -1;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject leg;

    [SerializeField] private AudioSource zombieSoundSource;
    [SerializeField] private AudioClip zombieAudioClip;
    
    [SerializeField] private GameObject sharedStats;
    private int damagePlayer;
    private int damageStructures;

    private bool isThrow;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GetComponent<TargetBehaviour>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
        Invoke("Despawn", 2);
        if (gameObject.name.Contains("throw"))
        {
            isThrow = true;
        }
        else
        {
            isThrow = false;
        }

        switch (sharedStats.GetComponent<SharedStats>().getDifficulty())
        {
            case "EASY":
                damagePlayer = 10;
                damageStructures = 30;
                break;
            case "HARD":
                damagePlayer = 20;
                damageStructures = 50;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Magnitude(transform.position - player[0].transform.position) < 10 && target.getIsAlive())
        {
            navMesh.isStopped = false;
            navMesh.destination = player[0].transform.position;
            animator.SetBool("isRun", true);
            if (isThrow && timeRemaining <= 0)
            {
                timeRemaining = 3;
                Instantiate(projectile, gameObject.transform.position+new Vector3(0,1,0) + gameObject.transform.forward*3, gameObject.transform.rotation);
            }
        }
        else
        {
            animator.SetBool("isRun", false);
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
        }

        if (Vector3.Magnitude(transform.position - player[0].transform.position) > 250 && target.getIsAlive())
        {
            Destroy(gameObject);
        }

        RaycastHit raycastHit;

        Vector3 pos = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);

        if (Physics.Raycast(pos, transform.forward, out raycastHit, 1))
        {
            hitted(raycastHit);
        }
        else{
            pos = new Vector3(leg.transform.position.x, leg.transform.position.y, leg.transform.position.z);

            if (Physics.Raycast(pos, transform.forward, out raycastHit, 1))
            {
                hitted(raycastHit);
            }
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        float distance = Vector3.Distance(transform.position, player[0].transform.position);
        if (distance <= minDistance)
        {
            if (!zombieSoundSource.isPlaying)
            {
                zombieSoundSource.PlayOneShot(zombieAudioClip);
            }
        }

    }

    private void hitted(RaycastHit raycastHit)
    {
        if (raycastHit.transform.tag.Equals("Player"))
        {
            if (timeRemaining <= 0)
            {
                timeRemaining = 3;
                TargetBehaviour player = raycastHit.transform.GetComponent<TargetBehaviour>();
                player.Hit(damagePlayer);
            }
        }

        if (raycastHit.transform.tag.Contains("Build"))
        {
            if (timeRemaining <= 0)
            {
                timeRemaining = 3;
                BuildHittable build = raycastHit.transform.GetComponent<BuildHittable>();


                if (build != null)
                {
                    build.hit(damageStructures);
                }
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
