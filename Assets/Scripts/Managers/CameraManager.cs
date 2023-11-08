using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float camFollowSpeed = 0.2f;

    private Transform player;
    private Vector3 camFollowVelocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void FollowTarget()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, player.position, ref camFollowVelocity, camFollowSpeed);
        transform.position = targetPos;
    }
}