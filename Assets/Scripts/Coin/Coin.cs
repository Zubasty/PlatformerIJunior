using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taken;

    public void Die()
    {
        Taken?.Invoke(this);
        Destroy(gameObject);
    }
}
