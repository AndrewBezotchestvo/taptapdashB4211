using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _exitButton;

    private bool _isPlaying;
    private bool _isPaused;

    private void Start()
    {
        _isPlaying = false;
        _isPaused = false;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            Time .timeScale = 0f;
        }

        _pausePanel.SetActive(_isPaused);
        _exitButton.SetActive(!_isPlaying);
    }
}
