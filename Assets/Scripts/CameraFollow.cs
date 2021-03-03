using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // Drop the player in the inspector of the camera

    public float desiredPositionX;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        desiredPositionX = target.position.x + 7;
        float smoothedPositionX = Mathf.Lerp(transform.position.x, desiredPositionX, Time.deltaTime * 4f);
        transform.position = new Vector3(smoothedPositionX, 0, -10);
    }
}
