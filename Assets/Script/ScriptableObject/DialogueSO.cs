using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
    public new string name;
    
    [TextArea]
    public string text;
}
