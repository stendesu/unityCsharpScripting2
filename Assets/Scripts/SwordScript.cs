using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class SwordLerp : MonoBehaviour
{
    [SerializeField] private Vector3[] _angles;
    [SerializeField] private float _lerpTime;

    Quaternion targetRotation;
    Quaternion startRotation;
    private float speed = 0.1f;


    void Start()
    {
        
        targetRotation.z = transform.rotation.z + 100f;
        
    }

    void Update()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

    }
}
