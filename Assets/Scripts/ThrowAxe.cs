using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxeScript : MonoBehaviour
{
    public GameObject player;
    private int axeDamage = 105;
    public Vector3 targetLocation;
    public Vector3 returnLocation;
    bool canAtk = true;
    bool secondAtk = false;
    public BoxCollider2D colldier;
    float speed = 35.0f;



    private void dealDamage(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            secondAtk = true;
        }
        else if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D enemyHitBox)) // get enemy hit box (box collider)
        {
            if (collider == enemyHitBox)    // check for enemy hit box*
            {
                if (collider.gameObject.TryGetComponent<SimpleNavMeshFollow>(out SimpleNavMeshFollow enemyComponent))   // get enemy script to deal damage
                {
                    if (collider.gameObject.tag == "Enemy") //  ensures that it is an enemy *
                    {
                        enemyComponent.takeDamage(axeDamage);

                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (canAtk)
        {
            dealDamage(collider);

            if (secondAtk)
            {
                dealDamage(collider);
            }
        }

    }


    public void goToTarget()
    {
        if (!secondAtk)            // lerp position to target location
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
        }
    }

    public void returnToPlayer()
    {
        if (secondAtk)
        {
            player = GameObject.Find("character");
            returnLocation = player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, returnLocation, Time.deltaTime * speed);
            if (transform.position == returnLocation)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        goToTarget();

        returnToPlayer();

        if (transform.position == targetLocation)
        {
            secondAtk = true;

        }
    }
}
