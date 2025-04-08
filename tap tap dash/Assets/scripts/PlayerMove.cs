using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    [SerializeField] private float _minDieSpeed;
    [SerializeField] private float _maxDieSpeed;

    private Rigidbody _rb;

    private Vector3 _movement;

    private bool _direction;

    private bool _isGround;

    private float _currentSpeed;
    private Vector3 _previosPîsition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _direction = true;
        _movement = transform.forward;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            _direction = !_direction;

            StartCoroutine(rotate(_direction));
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _isGround = false;
            _movement.y = _jumpForce;
        }

        if (_direction)
        {
            _movement.z = _playerSpeed;
            _movement.x = 0;
        }
        else
        {
            _movement.x = -_playerSpeed;
            _movement.z = 0;
        }

        if (_currentSpeed > _maxDieSpeed || _currentSpeed < _minDieSpeed)
        {
            if (transform.position.z > -5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }

    }

    private void FixedUpdate()
    {
        _currentSpeed = (transform.position - _previosPîsition).magnitude / Time.fixedDeltaTime;
        _previosPîsition = transform.position;

        _rb.MovePosition(transform.position + _movement * Time.fixedDeltaTime);

        if (!_isGround)
        {
            _movement.y -= _gravityScale * Time.fixedDeltaTime;
        }
        else
        {
            _movement.y = 0;
        }
    }

    IEnumerator rotate(bool direction)
    {
        for (int i = 0; i <= 90; i++)
        {
            if (direction)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, -90 + i, transform.localRotation.z);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, -i, transform.localRotation.z);
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}
