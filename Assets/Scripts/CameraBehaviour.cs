using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public GameObject firstPerson;
    public GameObject thirdPerson;

    private float xRotation = 0f;

    private bool thirdPersonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            thirdPersonOn = !thirdPersonOn;
            modifyCamera();
        }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void modifyCamera()
    {
        if (thirdPersonOn)
        {
            transform.position = new Vector3(thirdPerson.transform.position.x, thirdPerson.transform.position.y, thirdPerson.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(firstPerson.transform.position.x, firstPerson.transform.position.y, firstPerson.transform.position.z);
        }
    }
}