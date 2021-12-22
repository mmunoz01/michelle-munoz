using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class gun : MonoBehaviour
{

    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public AudioSource weaponShot;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 20;
    public int magazineAmmo = 50;
     
    public Animator animator;

    public float reloadTime = 2f;
    public bool isReloading;
    public TextMeshProUGUI ammoInfoText;

    InputAction shoot;
    // Start is called before the first frame update
    void Start()
    {
        shoot = new InputAction("shoot", binding: "<keyboard>/leftshift");
        //shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }


    // Update is called once per frame
    void Update()
    {

        gun currentGun = FindObjectOfType<gun>();
        ammoInfoText.text = currentGun.currentAmmo + "/" + currentGun.magazineAmmo;
        if (currentAmmo == 0 && magazineAmmo == 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;

        bool isShooting = CrossPlatformInputManager.GetButton("shoot");
        animator.SetBool("isShooting", isShooting);

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if (currentAmmo == 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        currentAmmo--; 
        RaycastHit hit;
        //muzzleFlash.Play();
        weaponShot.Play();
        if (Physics.Raycast(fpsCam.position + fpsCam.forward, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                
            }
            copAI cop = hit.transform.GetComponent<copAI>();
            if (cop != null)
            {
                cop.takeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("isReloading", false);
        if (magazineAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineAmmo -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineAmmo;
            magazineAmmo = 0;
        }
        isReloading = false;
    }

}
