using Unity.VisualScripting;
using UnityEngine;

namespace Game.Script.SoundManager
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        public void PlaySound(AudioClip audioClip)
        {
            if (audioClip == null) return;
            
            if (gameObject.GetComponent<AudioSource>() == null)
            {
                gameObject.AddComponent<AudioSource>();   
            }

            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            if (gameObject.GetComponent<AudioSource>() == null)
            {
                gameObject.AddComponent<AudioSource>();
            }

            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        public void StopMusic()
        {
            var audioSource = gameObject?.GetComponent<AudioSource>();
            if (audioSource.clip == null) return;
            audioSource.Pause();
        }
    }
}
