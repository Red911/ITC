using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPos : MonoBehaviour
{
    [SerializeField]private bool _dialogSpawned;
    public bool DialogSpawned { get => _dialogSpawned; set => _dialogSpawned = value; }
}
