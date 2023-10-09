using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EasyButtons;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform endingPoint;

    [SerializeField] private float speed = 100f;
    private bool _isMoving;

    private void FixedUpdate()
    {
        print(Vector3.Distance(transform.position, endingPoint.position));
        if (_isMoving && Vector3.Distance(transform.position, endingPoint.position) > 0.5) 
        {
            var dir = new Vector3(endingPoint.position.x - transform.position.x, 0, endingPoint.position.z - transform.position.z);
            dir.Normalize();

            rb.velocity = dir * (speed * Time.fixedDeltaTime);
        }
    }

    [Button]
    private void Move() => _isMoving = true;



}
