using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScriptPickUp : MonoBehaviour
{
    public GameObject swordPickUp;
    public CircleCollider2D circleCollider;

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
                        playerScript.canSwing = true;

                        Destroy(gameObject);
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
