using Game.Script.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class StartSound : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private bool _isMusic;
    [SerializeField] private bool _deactivateSound = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Sound();
        }

    }

    public void Sound()
    {
        if (_isMusic)
        {
            ServiceLocator.Get().StopMusic();
            ServiceLocator.Get().PlayMusic(_sound);
        }
        else ServiceLocator.Get().PlaySound(_sound);
        if (_deactivateSound) this.gameObject.SetActive(false);
    }
}
