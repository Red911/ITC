using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class GetGaze : MonoBehaviour
{
    [SerializeField] private GazeAware _gazeAware;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material redMat;
    private Material _startMat;
    

    private void Start()
    {
        _startMat = mesh.material;
    }

    private void Update()
    {
        mesh.material = _gazeAware.HasGazeFocus ? redMat : _startMat;
    }
}
