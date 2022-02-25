using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerAnimator : MonoBehaviour
{
    private const string VELOCITY_X_KEY = "VelocityX";
    private const string VELOCITY_Y_KEY = "VelocityY";

    private Animator _animator;
    private PlayerMove _mover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<PlayerMove>();
    }

    private void OnEnable()
    {
        _mover.UpdatedVelocity += OnUpdatedVelocity;
    }

    private void OnDisable()
    {
        _mover.UpdatedVelocity -= OnUpdatedVelocity;
    }

    private void OnUpdatedVelocity(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        _animator.SetFloat(VELOCITY_X_KEY, Mathf.Abs(velocity.x));
        _animator.SetFloat(VELOCITY_Y_KEY, velocity.y);
    }
}
