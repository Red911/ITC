using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static GetGaze;
using UnityEngine.Windows;
using Unity.VisualScripting;
using Game.Script.SoundManager;
using Unity.Mathematics;
using System.Linq;

public class GetTheDialogue : MonoBehaviour
{
    [SerializeField] private DialoguesScriptable _dialoguesSo;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    
    [SerializeField] private float speedText = .1f;
    [SerializeField] private float spaceSpeedText = .2f;
    [SerializeField] private float speedTextInGame = .1f;
    [SerializeField] private float spaceSpeedTextInGame = .2f;
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private event UnityAction ev;
    public UnityAction Ev { get => ev;  set => ev = value; }

    private bool _randomDialog;

    private int _currentLine = 0;
    public int CurrentLine { get => _currentLine; set => _currentLine = value;}

    [SerializeField] private KeyCode input;

    [SerializeField]private Player player;

    private void Start()
    {
        _nameText.text = _dialoguesSo.name;
        _dialogueText.text = _dialoguesSo.dialogs[_currentLine];
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(input))
        {
            NextDialogue();
        }

    }

    [Button]
    private void NextDialogue()
    {
        if (_currentLine == _dialoguesSo.dialogs.Length - 1 || _randomDialog)
        {
            _randomDialog = false;
            DialogueFinish();
            return;
        }

        _currentLine++;
        
        _dialogueText.text = _dialoguesSo.dialogs[_currentLine];
        StopAllCoroutines();
        AudioClip animalese = ReturnAnimalese(_dialoguesSo);
        StartCoroutine(TypeSentence(_dialoguesSo._dialAndSound[_currentLine]._dialogs, _dialoguesSo._dialAndSound[_currentLine]._sound, animalese));
    }

    public void SkipDialog() => NextDialogue();

    private void DialogueFinish()
    {
        dialoguePanel.SetActive(false);
        player.IsTalking = false;
        ev?.Invoke();
    }

    IEnumerator TypeSentence(string sentence, AudioClip sound = null, AudioClip animalese = null)
    {
        _dialogueText.text = "";
        if (animalese != null) ServiceLocator.Get().PlaySound(sound);
        foreach (var letters in sentence)
        {
            _dialogueText.text += letters;
            ServiceLocator.Get().PlaySound(animalese);
            if (letters != ' ') yield return new WaitForSeconds(speedText);
            else yield return new WaitForSeconds(spaceSpeedText);
        }
    }

    public void SetDialogAndTypeSentence(DialoguesScriptable dialog, int id, bool isRandomDialog = false)
    {
        _dialoguesSo = dialog;
        _currentLine = id;
        _randomDialog = isRandomDialog;
        player.IsTalking = true;
        if (!dialoguePanel.activeSelf) ShowDialogText();
        StopAllCoroutines();
        AudioClip animalese = ReturnAnimalese(dialog);
        
        StartCoroutine(TypeSentence(_dialoguesSo._dialAndSound[_currentLine]._dialogs, dialog._dialAndSound[_currentLine]._sound, animalese));
    }

    public AudioClip ReturnAnimalese(DialoguesScriptable dialog)
    {
        AudioClip ui;
        if (dialog._animalese.Length > 1) ui = dialog._animalese[UnityEngine.Random.Range(0, dialog._animalese.Length)];
        else ui = dialog._animalese[0];
        return ui;
    }

    public IEnumerator EnemySoundInGameDialog(TextMeshProUGUI txt, string sentence, AudioClip audioClip = null)
    {
        //txt.text = "";
        foreach (var letters in sentence)
        {
            //txt.text += letters;
            ServiceLocator.Get().PlaySound(audioClip);
            if (letters != ' ') yield return new WaitForSeconds(speedTextInGame);
            else yield return new WaitForSeconds(spaceSpeedTextInGame);
        }
    }

    private void ShowDialogText() => dialoguePanel.SetActive(true);
}
