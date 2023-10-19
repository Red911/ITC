using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "CreateDialog", order = 0)]
public class DialoguesScriptable : ScriptableObject
{
    public new string name;
    public AudioClip[] _animalese;
    [System.Serializable]
    public struct DialAndSound
    {
        [TextArea]
        public string _dialogs;
        public AudioClip _sound;
    }
    public DialAndSound[] _dialAndSound;
    [TextArea]
    public string[] dialogs;
    [TextArea]
    public string[] emotions;
}
