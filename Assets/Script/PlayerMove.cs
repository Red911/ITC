using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform endingPoint;

    [SerializeField, Header("AUDIO")] private AudioClip _testSFX;


    private void Start()
    {
        ServiceLocator.Get().PlaySound(_testSFX);
    }

}
