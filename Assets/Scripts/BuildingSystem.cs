using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Transform cam;
    private RaycastHit hit;
    public bool isEditMode;
    private GameObject transparentObject;
    private GameObject physicObject;
    public GameObject transparentPlane;
    public GameObject physicPlane;
    public GameObject transparentWall;
    public GameObject physicWall;
    public GameObject transparentStair;
    public GameObject physicStair;
    public GameObject player;
    private bool isPlane;
    private bool isStair;
    private Vector3 currentpos;
    private float offset = 1.0f;
    private float gridSize = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        isEditMode = false;
        transparentPlane = Instantiate(transparentPlane);
        transparentWall = Instantiate(transparentWall, transform.position, Quaternion.Euler(0, 90, 0));
        transparentStair = Instantiate(transparentStair);
        transparentPlane.SetActive(false);
        transparentWall.SetActive(false);
        transparentStair.SetActive(false);
        transparentObject = transparentPlane;
        physicObject = physicPlane;
        transparentObject.SetActive(false);
        isPlane = true;
        isStair = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEditMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transparentObject.SetActive(false);
                transparentObject = transparentPlane;
                physicObject = physicPlane;
                transparentObject.SetActive(true);
                isPlane = true;
                isStair = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                transparentObject.SetActive(false);
                transparentObject = transparentWall;
                physicObject = physicWall;
                transparentObject.SetActive(true);
                isPlane = false;
                isStair = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                transparentObject.SetActive(false);
                transparentObject = transparentStair;
                physicObject = physicStair;
                transparentObject.SetActive(true);
                isPlane = false;
                isStair = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEditMode = !isEditMode;
            if (isEditMode)
            {
                transparentObject.SetActive(true);
            }
            else
            {
                transparentObject.SetActive(false);
            }
        }
        if (isEditMode)
        {

            if (Physics.Raycast(cam.position, cam.forward, out hit, 9.0f))
            {
                currentpos = hit.point;
                currentpos -= Vector3.one * offset;
                currentpos /= gridSize;
                currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
                currentpos *= gridSize;
                currentpos += Vector3.one * offset;
                transparentObject.transform.position = currentpos;
                if (!isPlane)
                {
                    if (!isStair)
                    {
                        transparentObject.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt((90 + transform.eulerAngles.y) / 90f) * 90 : 0, 0);
                        transparentObject.transform.Translate(physicWall.transform.position);
                    }
                    else
                    {
                        transparentObject.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt((90 + transform.eulerAngles.y) / 90f) * 90 : 0, -45.0f);
                    }
                }
                else
                {
                    transparentObject.transform.Translate(0, 1.5f, 0);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(physicObject, transparentObject.transform.position, transparentObject.transform.rotation);
            }
        }
    }
}
