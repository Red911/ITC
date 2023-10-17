using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Tobii.Gaming;
using UnityEngine;


[RequireComponent(typeof(GazeAware))]
public class NextDialogue : MonoBehaviour
{
    [SerializeField]private GazeAware _gazeAware;
    [SerializeField]private GetTheDialogue _dialogue;
    [SerializeField]private float _timeToGaze = 1f;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
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
            // _timer = 0;
        }
    }
}
