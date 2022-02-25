using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimator : MonoBehaviour
{
    private const string VelocityXKey = "VelocityX";
    private const string VelocityYKey = "VelocityY";

    private Animator _animator;
    private PlayerMover _mover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<PlayerMover>();
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
        _animator.SetFloat(VelocityXKey, Mathf.Abs(velocity.x));
        _animator.SetFloat(VelocityYKey, velocity.y);
    }
}
