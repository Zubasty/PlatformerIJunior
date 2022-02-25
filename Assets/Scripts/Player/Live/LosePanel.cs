using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LosePanel : MonoBehaviour
{
    [SerializeField] private PlayerLive _player;
    [SerializeField] private Button _restart;

    private CanvasGroup _panel;

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.Died += OnPanel;
        _restart.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _player.Died -= OnPanel;
        _restart.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPanel()
    {
        _panel.interactable = _panel.blocksRaycasts = true;
        _panel.alpha = 1;
    }
}
