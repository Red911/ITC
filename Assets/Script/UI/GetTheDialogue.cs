using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTheDialogue : MonoBehaviour
{
    [SerializeField] private DialoguesScriptable _dialoguesSo;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private void Start()
    {
        _nameText.text = _dialoguesSo.name;
        _dialogueText.text = _dialoguesSo.dialogs[0];
        
    }
}
