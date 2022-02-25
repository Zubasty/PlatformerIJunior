using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _score = 0;

    public event Action<int> SetScore;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            SetScore?.Invoke(_score);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            Score++;
            coin.Die();
        }
    }
}
