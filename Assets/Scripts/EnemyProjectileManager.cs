using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{

    private float projectileDmg = 10.0f;
    public CircleCollider2D circleCollider;
    [SerializeField] private GameObject par_projectileImpact;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            death();
        }
        else if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D playerHitBox))
        {
            if (collider == playerHitBox)
            {
                if (collider.gameObject.TryGetComponent<TopDownCharacterController>(out TopDownCharacterController playerScript))
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        playerScript.takeDamage(projectileDmg);

                        death();
                    }
                }
            }
        }
    }


    private void spawnImpactParticle()
    {
        Vector3 spawnLocation = transform.position;
        Instantiate(par_projectileImpact, spawnLocation, Quaternion.identity);
    }


    private void death()
    {
        spawnImpactParticle();

        Destroy(gameObject);
    }





}
