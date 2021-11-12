using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Launcher : MonoBehaviour
{
    public int fireRateSeconds = 5;

    // This has to be set from Inspector (or can be loaded at runtime from Resource path)
    public GameObject shot;
    public float shootSpeed = 1.0f;
    public Transform shotSpawn;
    public bool automaticShoot = false;
    public float pitchRotationSpeed = 10f;
    public float pitchMin = -45f, pitchMax = 45f;
    public bool invertPitchAxis = false;
    public Camera camera;
    private float _cooldown = 0f;
    private float _cooldownTimer = 0f;
    private bool _canShoot = false;
    private uint _shotCounter = 0;
    private float _launcherPitch;

    public ParticleSystem particle;

    public GameObject[] particleHit;

    private BuildingSystem buildingSystem;

    private List<GameObject> weapon;

    private GameObject weaponHand;
    // Start is called before the first frame update

    private void Awake()
    {
        weapon = new List<GameObject>();
        weapon.Add(weaponHand = GameObject.Find("First"));
        weapon.Add(weaponHand = GameObject.Find("Second"));
        weapon.Add(weaponHand = GameObject.Find("Third"));

        weapon[0].SetActive(true);
        weapon[1].SetActive(false);
        weapon[2].SetActive(false);
    }
    void Start()
    {
        _cooldown = 1f / fireRateSeconds;
        buildingSystem = GetComponentInParent<BuildingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.transform.GetChild(0).position, camera.transform.GetChild(0).forward, Color.blue);

        // handle fire rate
        _cooldownTimer -= Time.deltaTime;
        _canShoot = false;
        if(!buildingSystem.isEditMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (GameObject w in weapon)
                {
                    w.SetActive(false);
                }
                weapon[0].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (GameObject w in weapon)
                {
                    w.SetActive(false);
                }
                weapon[1].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (GameObject w in weapon)
                {
                    w.SetActive(false);
                }
                weapon[2].SetActive(true);
            }
        }
        if (_cooldownTimer <= 0f)
        {
            _canShoot = true;
        }

        // Input has been put outside cooldown check so we can do something else if character cannot shoot, for example display something or reproduce a sound
        if (
            (Input.GetMouseButtonDown(0)
            || automaticShoot && Input.GetMouseButton(0)) && !buildingSystem.isEditMode
        )
        {
            if (shot && _canShoot)
            {
                // reset cooldown
                _cooldownTimer = _cooldown;
                RaycastHit raycastHit;

                Vector3 pos = new Vector3(camera.transform.parent.position.x, camera.transform.position.y, camera.transform.parent.position.z);

                if (Physics.Raycast(pos, camera.transform.forward, out raycastHit, 1000))
                {
                    TargetBehaviour enemy;
                    if (raycastHit.transform.tag == "Enemy")
                    {
                        enemy = raycastHit.transform.GetComponent<TargetBehaviour>();
                        enemy.Hit(10);
                    }
                    GameObject objectHit = Instantiate(particleHit[0], raycastHit.point, raycastHit.transform.rotation);
                    if (objectHit != null)
                    {
                        Destroy(objectHit, 2f);
                    }
                }
                _shotCounter++;
            }
        }
        
        
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }
}