using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] 
    private ObstacleType obstacleType;
    [SerializeField]
    private float sawMoveDistance = -4f, sawMoveTime = 0.6f, sawSpinAmount = 720f, sawSpinTime = 0.6f;

    void Start()
    {
        if(obstacleType == ObstacleType.SAW_BLADE)
        {
            LeanTween.moveX(gameObject, (transform.position.x + sawMoveDistance), sawMoveTime).setLoopPingPong();
            LeanTween.rotateAround(gameObject, Vector3.forward, sawSpinAmount, sawMoveTime).setLoopClamp();
        }
    }
}

public enum ObstacleType
{
    NONE,
    SAW_BLADE,
    SPIKE,
    DESTRUCTION_RIFT
}
