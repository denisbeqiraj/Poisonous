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

    private GameObject gun;

    private BuildingSystem buildingSystem;

    [SerializeField] private GameObject dialogSystem;
    private DialogController dialogController;

    private void Start()
    {
        slider.value = 100;
        buildingSystem = GetComponentInChildren<BuildingSystem>();

        gun = gameObject.transform.Find("Main Camera").Find("Gun").gameObject;

        dialogController = dialogSystem.GetComponent<DialogController>();
    }

    public void setLife(int life)
    {
        slider.value = life;
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
            RaycastHit raycastHit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, 4))
            {
                ObjectTake objectTake;
                if (raycastHit.transform.tag == "Food")
                {
                    objectTake = raycastHit.transform.GetComponent<ObjectTake>();
                    inventory.Add(objectTake.name);
                    objectTake.Die();

                    gameObject.GetComponent<TargetBehaviour>().addLife(10);
                }else if(raycastHit.transform.tag == "Ammo")
                {
                    objectTake = raycastHit.transform.GetComponent<ObjectTake>();
                    inventory.Add(objectTake.name);
                    objectTake.Die();

                    gun.GetComponent<Launcher>().addAmmo(30);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, 4))
            {
                GameObject speaker;
                if (raycastHit.transform.tag == "Npc")
                {
                    speaker = raycastHit.transform.gameObject;
                    string file = speaker.name;

                    file = file + ".txt";

                    Debug.Log(file);
                    Debug.Log(dialogController.getFile());

                    if (file == dialogController.getFile())
                    {
                        dialogController.printLines(4);
                    }
                    else {
                        Debug.Log(dialogController.getFile());
                        dialogController.setFile(file);
                    }
                }
            }
        }
    }

}