using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class GetGaze : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GazeAware _gazeAware;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material redMat;
    private Material _startMat;

    [SerializeField]private KeyCode input;
    

    private void Start()
    {
        _startMat = mesh.material;
    }

    private void Update()
    {
        if (_gazeAware.HasGazeFocus && Input.GetKeyDown(input))
        {
            mesh.material = redMat;
        }
        
    }
}
