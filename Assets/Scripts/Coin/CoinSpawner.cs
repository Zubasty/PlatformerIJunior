using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointsForSpawn;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _countCoins;
    [SerializeField] private float _offsetY;

    private Dictionary<Transform, Coin> _coinsSpawned = new Dictionary<Transform, Coin>();

    private void OnValidate()
    {
        if(_countCoins > _pointsForSpawn.Count)
        {
            _countCoins = _pointsForSpawn.Count;
            Debug.LogError("Количество монеток не может быть больше количества точек спавна");
        }
        if (_countCoins < 0)
        {
            _countCoins = 0;
            Debug.LogError("Количество монеток не может быть отрицательным");
        }
    }

    private void Start()
    {
        for(int i = 0; i< _countCoins; i++)
        {
            Spawn();
        }
    }

    private void OnDisable()
    {
        foreach(var pair in _coinsSpawned)
        {
            pair.Value.Taked -= OnTakedCoin;
        }
    }

    private Coin Spawn()
    {
        Transform point = _pointsForSpawn[Random.Range(0, _pointsForSpawn.Count)];
        _pointsForSpawn.Remove(point);
        Coin coin = Instantiate(_coinPrefab, new Vector3(point.position.x, 
            point.position.y + _offsetY, point.position.z), Quaternion.identity);
        _coinsSpawned.Add(point, coin);
        coin.Taked += OnTakedCoin;
        return coin;
    }

    private void OnTakedCoin(Coin coin)
    {
        Transform point = null;

        foreach(var pointCoin in _coinsSpawned)
        {
            if(pointCoin.Value == coin)
            {
                point = pointCoin.Key;
                break;
            }
        }
        _coinsSpawned.Remove(point);
        Spawn();
        _pointsForSpawn.Add(point);       
    }
}
