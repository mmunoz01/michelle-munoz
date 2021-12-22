using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copAI : MonoBehaviour
{
    public int enemyHP = 100;
    public GameObject projectile;
    public Transform projectilePoint;
    public Animator animator;

    public void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 7, ForceMode.Impulse);
    }

    public void takeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        if (enemyHP <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
