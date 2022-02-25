using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _count;
    [SerializeField] private float _offsetY;

    private Dictionary<Transform, Coin> _pointCoin = new Dictionary<Transform, Coin>();

    private void OnValidate()
    {
        if(_count > _points.Count)
        {
            _count = _points.Count;
            Debug.LogError("Количество монеток не может быть больше количества точек спавна");
        }
        if (_count < 0)
        {
            _count = 0;
            Debug.LogError("Количество монеток не может быть отрицательным");
        }
    }

    private void Start()
    {
        for(int i = 0; i< _count; i++)
        {
            Spawn();
        }
    }

    private void OnDisable()
    {
        foreach(var pair in _pointCoin)
        {
            pair.Value.Taken -= OnTaken;
        }
    }

    private Coin Spawn()
    {
        Transform point = _points[Random.Range(0, _points.Count)];
        _points.Remove(point);
        Coin coin = Instantiate(_prefab, new Vector3(point.position.x, 
            point.position.y + _offsetY, point.position.z), Quaternion.identity);
        _pointCoin.Add(point, coin);
        coin.Taken += OnTaken;
        return coin;
    }

    private void OnTaken(Coin coin)
    {
        Transform point = null;

        foreach(var pointCoin in _pointCoin)
        {
            if(pointCoin.Value == coin)
            {
                point = pointCoin.Key;
                break;
            }
        }
        _pointCoin.Remove(point);
        Spawn();
        _points.Add(point);       
    }
}
