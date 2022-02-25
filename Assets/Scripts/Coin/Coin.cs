using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taked;

    public void Die()
    {
        Taked?.Invoke(this);
        Destroy(gameObject);
    }
}
