using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "CreateDialog", order = 0)]
public class DialoguesScriptable : ScriptableObject
{
    [TextArea]
    public string[] dialogs;
}
