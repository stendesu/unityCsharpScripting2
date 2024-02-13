using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class SwordLerp : MonoBehaviour
{
    Quaternion targetRotation;
    public float speed = 1f;


    void Start()
    {
        var currentRot = transform.eulerAngles;
        
        targetRotation = Quaternion.AngleAxis(currentRot.z + 250f, Vector3.back);

        Debug.Log("target rotation " + targetRotation.z);
        Debug.Log("current rotation " + transform.rotation.z);

        //targetRotation.z = transform.rotation.z + 210f;
        //targetRotation.y = 0f; 
        //targetRotation.x = 0f;
    }

    void Update()
    {
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);

    }
}
