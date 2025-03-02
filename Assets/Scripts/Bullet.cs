using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private Rigidbody _rigidbody;
    private int _currentWaypoint;
    
    public event Action<Bullet> BulletFall;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            if (_currentWaypoint == _waypoints.Length - 1)
                BulletFall?.Invoke(this);
            else
                _currentWaypoint++;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _currentWaypoint = 0;
        gameObject.SetActive(true);
    }
}