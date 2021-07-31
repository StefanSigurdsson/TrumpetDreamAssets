using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private int _currentWaypointIndex;
    private Vector3 _lastPointPosition;
    private SpriteRenderer _spriteRenderer;

    public float MoveSpeed { get; set; }
    public Color Color;
    public Waypoint Waypoint;

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);


    // Start is called before the first frame update
    void Start()
    {
        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;

        _lastPointPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color = _spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPointPosition, MoveSpeed * Time.deltaTime);

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition <= 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
