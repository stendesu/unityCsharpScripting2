using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    private float projectileDmg = 50.0f;
    public CircleCollider2D Collider2D;
    [SerializeField] private GameObject par_projectileImpact;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private int maxBounces = 3;

    //private Vector3 lastVelocity;
    //private int bounces = 0;
    //private Vector3 startLocation;
    //private Vector3 endLocation;
    //private bool bounced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //startLocation = transform.position;
    }

    void Update()
    {
        //lastVelocity = rb.velocity;

    }
    /*
            mousePointOnScreen.z = 0;
            Vector2 shootDir = (mousePointOnScreen - transform.position).normalized;
            Debug.Log(shootDir);
            bulletToSpawn.GetComponent<Rigidbody2D>().AddForce(shootDir * m_projectileSpeed, ForceMode2D.Impulse);
*/

    //  projectile bounce attempts
    //private void bounceEvent()
    //{
    //    if (!bounced)
    //    {
    //        startLocation.z = 0;
    //        endLocation.z = 0;  

    //        Vector2 nextDir = (startLocation - endLocation).normalized;
    //        Debug.Log("bounce direction: " + nextDir);
    //        GetComponent<Rigidbody2D>().AddForce((nextDir * -1) * 45, ForceMode2D.Impulse);
    //        bounces++;
    //        startLocation = endLocation;
    //        bounced = true;
    //    }


    //}


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var speed = lastVelocity.magnitude;
    //    var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

    //    Debug.Log("initiating velocity");
    //    rb.velocity = direction * Mathf.Max(speed, 0f);
    //    bounces++;

    //}



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")  // check for wall
        {
            death();

        }
        else if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D enemyHitBox)) // get enemy hit box (box collider)
        {
            if (collider == enemyHitBox)    // check for enemy hit box*
            {
                if (collider.gameObject.TryGetComponent<SimpleNavMeshFollow>(out SimpleNavMeshFollow enemyComponent))   // get enemy script to deal damage
                {
                    if (collider.gameObject.tag == "Enemy") //  ensures that it is an enemy *
                    {
                        enemyComponent.takeDamage(projectileDmg);

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
