using UnityEngine;

namespace Game.Script.SoundManager
{
    public class NullSoundManager : ISoundManager
    {
        public void PlaySound(AudioClip audioClip){}
        public void PlayMusic(AudioClip audioClip){}

        public void StopMusic(){}
    }
}
