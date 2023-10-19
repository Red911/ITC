using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{
    [SerializeField]private EnemyController _enemy;
    [SerializeField]private DialogSpawner _dialogSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            _enemy.transform.root.gameObject.SetActive(true);
            //_dialogSpawner.gameObject.SetActive(true);
            _enemy.DialogSpawner = _dialogSpawner;
            this.gameObject.SetActive(false);
        }

    }
}
