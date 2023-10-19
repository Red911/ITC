using Game;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(GazeAware))]
public class GetGaze : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GazeAware _gazeAware;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField]private Slider eyeKeepSlider;

    [Space(25)]
    [SerializeField] private float timeToGaze = 3f;
    
    private Material _startMat;
    private Player player;
    private float _timer;

    public enum GazeType
    {
        VALID,
        INVALID,
        NEUTRAL
    
    }

    public GazeType _type;
    

    private void Start()
    {
        _startMat = mesh.material;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        eyeKeepSlider.maxValue = timeToGaze;
    }

    private void Update()
    {
        _timer = Mathf.Clamp(_timer, 0f, timeToGaze);
        
        if (_gazeAware.HasGazeFocus)
        {
            // if (player.IsTalking) return;
            eyeKeepSlider.gameObject.SetActive(true);
            _timer += Time.deltaTime;
            if (_timer >= timeToGaze)
            {
                _timer = 0;
                GazeCheck();
            }
            
        }
        else
        {
            _timer -= Time.deltaTime / 1.5f;
            eyeKeepSlider.gameObject.SetActive(false);
        }
        eyeKeepSlider.value = _timer;
        
    }

    [Button("Gaze Check", EButtonEnableMode.Playmode)]
    private void GazeCheck()
    {
            if (player.IsTalking) return;
           switch(_type)
            {
                case GazeType.VALID:
                    player.CurrentEnemy.NeutralState.EnterHappyState();
                break;
                
                case GazeType.INVALID:
                    player.DamagePlayer(1);
                    player.CurrentEnemy.NeutralState.EnterHurtState();
                break;
                
                case GazeType.NEUTRAL:
                    player.CurrentEnemy.NeutralState.TalkNeutral();
                break;
            }

    }
}
