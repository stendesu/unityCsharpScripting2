using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarManager : MonoBehaviour
{
    private TopDownCharacterController playerScript;
    Vector3 localScale;
    public GameObject playerSelf;


    // Start is called before the first frame update
    void Start()
    {
        playerSelf = transform.parent.gameObject;
        playerScript = playerSelf.GetComponent<TopDownCharacterController>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float hpPercent = (playerScript.currentHp / playerScript.maxHp);
        localScale.x = hpPercent;
        transform.localScale = localScale;
    }
}
