using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject impactEffect;
    public float radius = 3;
    public int DamageAmount = 15;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Player")
            {
                StartCoroutine(FindObjectOfType<playerManager>().TakeDamage(DamageAmount));
            }
        }
        
        this.enabled = false;
    }
}
