using DG.Tweening;
using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{
    [SerializeField]private EnemyController _enemy;
    [SerializeField]private DialogSpawner _dialogSpawner;

    [SerializeField] private AudioClip _music;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            _enemy.transform.root.gameObject.SetActive(true);
            //_dialogSpawner.gameObject.SetActive(true);
            _enemy.DialogSpawner = _dialogSpawner;
            if(_music != null) _player.ChangeMusic(_music);                
            
            this.gameObject.SetActive(false);
        }

    }
}
