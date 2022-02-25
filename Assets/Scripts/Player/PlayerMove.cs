using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private const float MIN_MOVE_DISTANCE = 0.001f;
    private const float SHELL_RADIUS = 0.01f;

    [SerializeField, Range(0,1)] private float _minGroundNormalY = 0.65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _powerJump;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private KeyCode _keyJump;
    [SerializeField] private KeyCode _keyRight;
    [SerializeField] private KeyCode _keyLeft;

    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rb2d;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    public event Action<Vector2> UpdatedVelocity;

    void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        _rb2d.velocity = new Vector2();
        float x = 0;

        if (Input.GetKey(_keyRight))
        {
            x = 1;
        }
        else if (Input.GetKey(_keyLeft))
        {
            x = -1;
        }
        _targetVelocity = new Vector2(x * _speed, 0);

        if (Input.GetKey(_keyJump) && _grounded)
            _velocity.y = _powerJump;
        UpdatedVelocity?.Invoke(new Vector2(x, _velocity.y));
    }

    void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;
        _grounded = false;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);      
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MIN_MOVE_DISTANCE)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + SHELL_RADIUS);
            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }
            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }
                float modifiedDistance = _hitBufferList[i].distance - SHELL_RADIUS;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        _rb2d.position = _rb2d.position + move.normalized * distance;
    }
}