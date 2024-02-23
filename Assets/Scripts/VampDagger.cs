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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, target.position);
        transform.rotation = rotation;

        player = GameObject.Find("character");
        playerScript = player.GetComponent<TopDownCharacterController>();

    }

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
                        enemyComponent.takeDamage(DaggerDamage);


                        float healReturn = (DaggerDamage / 2) / -1;
                        playerScript.takeDamage(healReturn);

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

        //Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
