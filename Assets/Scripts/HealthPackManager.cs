using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : MonoBehaviour
{
    private float healAmount = -50f;
    public BoxCollider2D boxCollider;



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D playerHitBox))
        {
            if (collider == playerHitBox)
            {
                if (collider.gameObject.TryGetComponent<TopDownCharacterController>(out TopDownCharacterController playerScript))
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        playerScript.takeDamage(healAmount);

                        death();
                    }
                }
            }
        }
    }


    private void death()
    {
        Destroy(gameObject);
    }


}
