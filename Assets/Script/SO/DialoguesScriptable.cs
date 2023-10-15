using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "CreateDialog", order = 0)]
public class DialoguesScriptable : ScriptableObject
{
    public new string name;
    public AudioClip clip;
    [TextArea]
    public string[] dialogs;
}
