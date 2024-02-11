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
        targetRotation.z = transform.rotation.z + 210f;
    }

    void Update()
    {
        Debug.Log("target rotation" + targetRotation.z);
        Debug.Log("current rotation" + transform.rotation.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);

    }
}
