using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _pauseButton;

    [SerializeField] private Text score;
    [SerializeField] private Text record;

    [SerializeField] private Transform _player;

    private float _scoreValue;
    private float _recordValue;

    private bool _isPlaying;
    private bool _isPaused;

    private void Start()
    {
        _isPlaying = false;
        _isPaused = false;

        _scoreValue = 0;
        _recordValue = PlayerPrefs.GetFloat("Record", 0);

        score.text = _scoreValue.ToString();
        record.text = _recordValue.ToString();
    }

    private void Update()
    {
        if (-_player.position.x + _player.position.z >= 0 )
        {
            _scoreValue = -_player.position.x + _player.position.z;
        }
        if (_scoreValue > _recordValue)
        {
            _recordValue = Mathf.RoundToInt(_scoreValue);
            PlayerPrefs.SetFloat("Record", _recordValue);
            PlayerPrefs.Save();
        }

        score.text = Mathf.RoundToInt(_scoreValue).ToString();
        record.text = _recordValue.ToString();

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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangePause();
            }
        }
        else
        {
            Time.timeScale = 0f;
        }

        _pausePanel.SetActive(_isPaused);
        _exitButton.SetActive(!_isPlaying);

        _playButton.SetActive(!_isPlaying);
        _pauseButton.SetActive(false);
    }

    public void ChangePause()
    {
        _isPaused = !_isPaused;
    }
    public void Play()
    {
        _isPlaying = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
