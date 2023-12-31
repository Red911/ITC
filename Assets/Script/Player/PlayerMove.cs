using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using NaughtyAttributes;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform _endingPoint;
    public Transform EndingPoint { get { return _endingPoint; } set => _endingPoint = value; }
    

    [SerializeField] private float speed = 100f;
    
    [Header("Camera Shake")]
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeFrequency;
    private bool _isMoving;

    private void Start()
    {
        Move();
    }
    private void FixedUpdate()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        if (_isMoving && Vector3.Distance(transform.position, _endingPoint.position) > 0.5) 
        {
           
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = shakeFrequency;
            
            var dir = new Vector3(_endingPoint.position.x - transform.position.x, 0, _endingPoint.position.z - transform.position.z);
            dir.Normalize();

            rb.velocity = dir * (speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
            _isMoving = false;
        }
    }

    [Button]
    public void Move() => _isMoving = true;

    
    //[SerializeField, Header("AUDIO")] private AudioClip _testSFX;


    // private void Start()
    // {
    //     ServiceLocator.Get().PlaySound(_testSFX);
    // }

}
