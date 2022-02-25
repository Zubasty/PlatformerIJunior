using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
public class ScoreVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _startText;

    private PlayerScore _player;

    private void Awake()
    {
        _player = GetComponent<PlayerScore>();
        _text.text = "Очки: " + _player.Score;
    }

    private void OnEnable()
    {
        _player.SetScore += OnSetScore;
    }

    private void OnDisable()
    {
        _player.SetScore -= OnSetScore;        
    }

    private void OnSetScore(int score)
    {
        _text.text = _startText + score;
    }
}
