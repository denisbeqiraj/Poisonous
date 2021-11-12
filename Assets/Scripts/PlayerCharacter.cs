/*** SOURCE EXAMPLE
 * https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
 * Adapted to have fixed rotation while moving
 */

using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.18f;
    public float jumpHeight = 1.4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Camera camera;
    Vector3 velocity;
    bool isGrounded;

    public InventorySystem inventory;

    public UnityEngine.UI.Slider slider;

    public int health=100;

    private BuildingSystem buildingSystem;

    private void Start()
    {
        slider.value = health;
        buildingSystem = GetComponentInChildren<BuildingSystem>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.V))
        {
            EnemySpawner spawner = GetComponent<EnemySpawner>();
            RaycastHit raycastHit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, 1000))
            {
                spawner.spawnEnemy();
                ObjectTake objectTake;
                if (raycastHit.transform.tag == "Object")
                {
                    objectTake = raycastHit.transform.GetComponent<ObjectTake>();
                    inventory.Add(objectTake.name);
                    objectTake.Die();
                }
            }
        }
    }

}