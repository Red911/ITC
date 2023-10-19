using UnityEngine;

namespace Game.Script.SoundManager
{
    public interface ISoundManager
    {
        void PlaySound(AudioClip audioClip);

        void PlayMusic(AudioClip audioClip);

        void StopMusic(AudioClip audioClip);
    }
}
