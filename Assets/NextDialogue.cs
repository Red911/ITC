using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GazeAware))]
public class NextDialogue : MonoBehaviour
{
    [Header("Component")]
    //[SerializeField]private GazeAware _gazeAware;
    private bool _hasGazeFocus;
    [SerializeField]private GetTheDialogue _dialogue;
    [SerializeField]private Slider nextDialogSlider;
    
    [Header("Time")]
    [SerializeField]private float _timeToGaze = 1f;
    private float _timer;

    private void Start()
    {
        nextDialogSlider.maxValue = _timeToGaze;
    }

    void Update()
    {
        _timer = Mathf.Clamp(_timer, 0f, _timeToGaze);
        
        if (_hasGazeFocus)
        {
            _timer += Time.deltaTime;
            
            if (_timer >= 1) 
            {
                _timer = 0;
                _dialogue.SkipDialog();
            }
        }
        else
        {
            _timer -= Time.deltaTime / 1.5f;
            
        }
        
        nextDialogSlider.value = _timer;
    }

    private void OnMouseEnter()
    {
        _hasGazeFocus = true;
    }

    private void OnMouseExit()
    {
        _hasGazeFocus = false;
    }

    private void OnDisable()
    {
        _hasGazeFocus = false;
    }
}
