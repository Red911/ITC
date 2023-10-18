using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLine : MonoBehaviour
{
    [SerializeField]
    private Vector3 _dialogDir;
    public Vector3 DialogDir { get => _dialogDir; set => _dialogDir = value; }
    
    [SerializeField]
    private float _dialogSpeed = 50f;

    private RectTransform _rectTransform;

    private DialogPos _dialogPos;

    public DialogPos DialogPos { get => _dialogPos; set => _dialogPos  = value; }

    private float _timeBeforeDispawn = 1f;
    public float TimeBeforeDispawn { get => _timeBeforeDispawn; set => _timeBeforeDispawn = value; }

    private Vector3 dialogPos;

    private Animator _dialogAnim;


    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _dialogAnim = this.GetComponent<Animator>();
        
        dialogPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rectTransform.position = Vector3.MoveTowards(this.transform.position, _rectTransform.position + _dialogDir, _dialogSpeed * Time.deltaTime);
        if (_timeBeforeDispawn < 0)
        {
            _dialogAnim.SetBool("Disappear", true);
            Destroy(this.gameObject, _dialogAnim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else _timeBeforeDispawn -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        _dialogPos.DialogSpawned = false;
    }
}
