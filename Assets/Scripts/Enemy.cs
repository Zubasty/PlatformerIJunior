using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private Platform _platformPatrol;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;
    private Direction _direction = Direction.Right;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_direction == Direction.Left && _platformPatrol.LeftPosition.position.x >= transform.position.x)
        {
            _direction = Direction.Right;
        }

        if(_direction == Direction.Right && _platformPatrol.RightPosition.position.x <= transform.position.x)
        {
            _direction = Direction.Left;
        }
        float target = _direction == Direction.Right ?
            _platformPatrol.RightPosition.position.x : _platformPatrol.LeftPosition.position.x;
        float maxDelta = _speed * Time.deltaTime;
        float x = Mathf.MoveTowards(transform.position.x, target, maxDelta);
        _rigidbody2D.MovePosition(new Vector3(x, transform.position.y, transform.position.z));
    }
}
