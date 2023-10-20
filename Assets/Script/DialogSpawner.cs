using Game;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpawner : MonoBehaviour
{
    [SerializeField, BoxGroup("Dialogs")] private float _maxCdSpawn = 2f;
    [SerializeField, BoxGroup("Dialogs")] private float _cdSpawn;
    [SerializeField, BoxGroup("Dialogs")] private float _dialogDispawnTime = 3f;
    [SerializeField, BoxGroup("Dialogs")] private GameObject _dialogGO;
    [SerializeField, BoxGroup("Dialogs")] private DialogPos[] _spawnPos;
    [SerializeField, BoxGroup("Dialogs")] private int _lineIndex = 0;
    
    public static EnemyController enemy;

    [SerializeField, BoxGroup("Thoughts")] private float _maxThoughtsCdSpawn = 2f;
    [SerializeField, BoxGroup("Thoughts")] private float _thoughtsCdSpawn;
    [SerializeField, BoxGroup("Thoughts")] private float _thoughtsDispawnTime = 3f;
    [SerializeField, BoxGroup("Thoughts")] private GameObject _thoughtsGO;
    [SerializeField, BoxGroup("Thoughts")] private DialogPos[] _thoughtsSpawnPos;
    [SerializeField, BoxGroup("Thoughts")] private int _thoughtsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _cdSpawn = _maxCdSpawn;
        _thoughtsCdSpawn = _maxThoughtsCdSpawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy == null) return;
        //dialog spawn
        if (_cdSpawn < 0)
        {
            int index;
            int allSpawned = 0;
            do
            {
                index = Random.Range(0, _spawnPos.Length);
                allSpawned++;
                if (allSpawned >= _spawnPos.Length) return;
            }
            while (_spawnPos[index].DialogSpawned);

            _cdSpawn = _maxCdSpawn;
            GameObject dialog = Instantiate(_dialogGO, _spawnPos[index].transform);
            DialogLine y = dialog.GetComponent<DialogLine>();
            TextMeshProUGUI enemyTxt = dialog.GetComponent<TextMeshProUGUI>();
            enemyTxt.text = enemy._enemyDial[enemy.CurrentPhase]._dialog._dialAndSound[_lineIndex]._dialogs;    
            AudioClip animalese = enemy.TheDialog.ReturnAnimalese(enemy._enemyDial[enemy.CurrentPhase]._dialog);
            StartCoroutine(enemy.TheDialog.EnemySoundInGameDialog(enemyTxt, enemy._enemyDial[enemy.CurrentPhase]._dialog._dialAndSound[_lineIndex]._dialogs, animalese));
            if (_lineIndex >= enemy._enemyDial[enemy.CurrentPhase]._dialog._dialAndSound.Length - 1) _lineIndex = 0;
            else _lineIndex++;
            y.DialogPos = _spawnPos[index];
            y.TimeBeforeDispawn = _dialogDispawnTime;
            y.DialogDir = new Vector3(Random.Range(-1, 1.1f), Random.Range(-1, 1.1f));
            _spawnPos[index].DialogSpawned = true;
        }
        else _cdSpawn -= Time.deltaTime;

        //thought spawn
        if (_thoughtsCdSpawn < 0)
        {
            int index;
            int allSpawned = 0;
            do
            {
                index = Random.Range(0, _thoughtsSpawnPos.Length);
                allSpawned++;
                if (allSpawned >= _thoughtsSpawnPos.Length) return;
            }
            while (_thoughtsSpawnPos[index].DialogSpawned);

            GameObject thought = Instantiate(_thoughtsGO, _thoughtsSpawnPos[index].transform);
            _thoughtsSpawnPos[index].DialogSpawned = true;

            DialogLine y = thought.GetComponent<DialogLine>();
            TextMeshProUGUI ui = thought.GetComponent<TextMeshProUGUI>();
            ui.text = enemy._enemyDial[enemy.CurrentPhase]._dialog.emotions[_thoughtsIndex];
            enemy.TheDialog.EnemySoundInGameDialog(ui, enemy._enemyDial[enemy.CurrentPhase]._dialog.emotions[_thoughtsIndex], enemy.TheDialog.ReturnAnimalese(enemy._enemyDial[enemy.CurrentPhase]._dialog));
            y.DialogPos = _thoughtsSpawnPos[index];
            y.TimeBeforeDispawn = _thoughtsDispawnTime;
            y.DialogDir = new Vector3(Random.Range(-1, 1.1f), Random.Range(-1, 1.1f));

            if (_thoughtsIndex >= _thoughtsSpawnPos.Length - 1)_thoughtsIndex = 0;
            else _thoughtsIndex++;

            _thoughtsCdSpawn = _maxThoughtsCdSpawn;
        }
        else _thoughtsCdSpawn -= Time.deltaTime;



    }
    public void SetActiveDialogSpawner(bool setActive)
    {
        this.gameObject.SetActive(setActive);
    }

    public void ResetIndex()
    {
        _lineIndex = 0;
        _thoughtsIndex = 0;
    }
}
