using Game;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Rendering;

public class GetGaze : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GazeAware _gazeAware;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material redMat;
    private Material _startMat;

    [SerializeField]private KeyCode input;

    private Player player;

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
    }

    private void Update()
    {
        if (_gazeAware.HasGazeFocus && Input.GetKeyDown(input))
        {
            GazeCheck();
        }
        
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
