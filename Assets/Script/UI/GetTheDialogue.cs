using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GetTheDialogue : MonoBehaviour
{
    [SerializeField] private DialoguesScriptable _dialoguesSo;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    
    [SerializeField] private float speedText = .2f;
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private event UnityAction ev;
    public UnityAction Ev { get => ev;  set => ev = value; }

    private bool randomDialog;

    private int _currentLine = 0;

    private void Start()
    {
        _nameText.text = _dialoguesSo.name;
        _dialogueText.text = _dialoguesSo.dialogs[_currentLine];
        StartCoroutine(TypeSentence(_dialoguesSo.dialogs[_currentLine]));
    }

    [Button]
    private void NextDialogue()
    {
        if (_currentLine == _dialoguesSo.dialogs.Length - 1 || randomDialog)
        {
            randomDialog = false;
            DialogueFinish();
            return;
        }

        _currentLine++;
        
        _dialogueText.text = _dialoguesSo.dialogs[_currentLine];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_dialoguesSo.dialogs[_currentLine]));
    }

    private void DialogueFinish()
    {
        dialoguePanel.SetActive(false);
        ev?.Invoke();
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        foreach (var letters in sentence)
        {
            _dialogueText.text += letters;
            yield  return new WaitForSeconds(speedText);
        }
    }

    public void SetDialogAndTypeSentence(DialoguesScriptable dialog, int id, bool isRandomDialog = false)
    {
        _dialoguesSo = dialog;
        _currentLine = id;
        randomDialog = isRandomDialog;
        //StopAllCoroutines();
        if (!dialoguePanel.activeSelf) ShowDialogText();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_dialoguesSo.dialogs[_currentLine]));
    }

    private void ShowDialogText() => dialoguePanel.SetActive(true);
    
    
}
