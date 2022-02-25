using System;
using UnityEngine;

public class PlayerLive : MonoBehaviour
{
    public event Action Died;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}
