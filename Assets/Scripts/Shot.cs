using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
    private float speed = 20.0f;
    public Vector3 direction;
    public float lifeTime = 10.0f;
    public GameObject explosion;
    public GameObject throwElement;

    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        throwElement=Instantiate(throwElement, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        // start with explosive velocity, also called impulse
        _rigidbody.AddForce(gameObject.transform.forward * speed, ForceMode.VelocityChange);
    }

    void Update()
    {
        // handle life of the shot
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            // Destroy whole gameobject, if "this" is being used instead of gameObject -> then only this script (MonoBehaviour) will be destroyed
            StartCoroutine(DestroyRoutine(0.3f));
        }
        throwElement.transform.position = gameObject.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            TargetBehaviour targetBehaviour=collision.gameObject.GetComponent<TargetBehaviour>();
            targetBehaviour.Hit(20);
        }
        StartCoroutine(DestroyRoutine(0.3f));
    }

    private IEnumerator DestroyRoutine(float time)
    {
        // handle how the shot is destroyed (briefly alive to show particles (from particle lesson))
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.detectCollisions = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _rigidbody.isKinematic = true;
        if (explosion)
        {
            explosion.SetActive(true);
        }
        yield return new WaitForSeconds(time);
        Destroy(throwElement);
        Destroy(gameObject);
    }
}