using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Ground = nameof(Ground);

    private float _inputVelocity;

    private float _currentSpeed;

    private bool _isGrounded;

    private bool _isRight;

    [SerializeField] private float _normalSpeed;

    [SerializeField] private float _jumpForce;

    [SerializeField] private float _dashForce;

    [SerializeField] private float _rayDistance;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector2 _teleportCoordinates;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentSpeed = _normalSpeed;
    }

    private void Update()
    {
        _inputVelocity = Input.GetAxis(Horizontal) * _currentSpeed;

        _spriteRenderer.flipX = Input.GetAxis(Horizontal) < 0;

        var hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, _rayDistance, LayerMask.GetMask(Ground));

        if (hit.collider != null)
            _isGrounded = true;
        else
            _isGrounded = false;

        if (_isGrounded)
            JumpUpdate();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DashUpdate();
        }

#if UNITY_EDITOR
        DevToolsUpdate();
#endif
    }

    private void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var v = Vector2.up * _jumpForce;
            _rigidbody.AddForce(v);
        }
    }

    private void DashUpdate()
    {
        if (Input.GetAxis(Horizontal) < 0)
            _isRight = false;
        else
            _isRight = true;

        if(_isRight)
        {
            var v = Vector2.right * _dashForce;
            _rigidbody.AddForce(v);
        }
        else
        {
            var v = Vector2.left * _dashForce;
            _rigidbody.AddForce(v);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(_inputVelocity);
        _rigidbody.velocity = new Vector2(_inputVelocity * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

#if UNITY_EDITOR

    private void DevToolsUpdate()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            transform.position = _teleportCoordinates;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }
    }

#endif
}
