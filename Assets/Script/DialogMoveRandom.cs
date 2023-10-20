using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMoveRandom : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    private Transform _target;
    [SerializeField] private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        FindNewTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, _target.position) > .1f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _target.position, _speed * Time.deltaTime);
        }
        else FindNewTarget();
    }

    private void FindNewTarget()
    {
        _target = _movePoints[Random.Range(0, _movePoints.Length)];
    }
}
