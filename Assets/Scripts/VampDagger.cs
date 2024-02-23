using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VampDagger : MonoBehaviour
{
    private float DaggerDamage = 35.0f;
    public BoxCollider2D Colldier;
    [SerializeField] private GameObject par_daggerImpact;
    [SerializeField] private Rigidbody2D rb;
    public Transform target;
    public GameObject player;
    public TopDownCharacterController playerScript;
    int maxTargetHit = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("character");
        playerScript = player.GetComponent<TopDownCharacterController>();

        Vector3 Look = transform.InverseTransformPoint(playerScript.mousePos);
        float Angle = Mathf.Atan2(Look.y , Look.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, Angle);


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")  // check for wall
        {
            spawnImpactParticle();

            Destroy(gameObject);

        }
        else if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D enemyHitBox)) // get enemy hit box (box collider)
        {
            if (collider == enemyHitBox)    // check for enemy hit box*
            {
                if (collider.gameObject.TryGetComponent<SimpleNavMeshFollow>(out SimpleNavMeshFollow enemyComponent))   // get enemy script to deal damage
                {
                    if (collider.gameObject.tag == "Enemy") //  ensures that it is an enemy *
                    {
                        enemyComponent.takeDamage(DaggerDamage);

                        float healReturn = (DaggerDamage / 2) / -1;
                        playerScript.takeDamage(healReturn);

                        maxTargetHit += 1;

                        death();
                    }
                }
            }
        }
    }

    private void spawnImpactParticle()
    {
        Vector3 spawnLocation = transform.position;
        Instantiate(par_daggerImpact, spawnLocation, Quaternion.identity);
    }

    private void death()
    {
        spawnImpactParticle();

        if (maxTargetHit >= 3)
        {
            Destroy(gameObject);
        }

    }
}
