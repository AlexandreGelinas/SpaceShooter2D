using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Propriétés joueur")]
    [SerializeField] private float _playerSpeed = 8f;
    [SerializeField] private float _maxHeight = 1f;

    [Header("Propriétés attaques")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.2f;

    private InputSystem_Actions _inputSystem_Actions;

    private float _minX, _minY, _maxX, _maxY;

    private SpriteRenderer _spriteRenderer;

    private float _canFire = -1f;
    private bool _isFiring = false;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        _spriteRenderer= GetComponent<SpriteRenderer>();

        _inputSystem_Actions = new InputSystem_Actions();
        _inputSystem_Actions.Player.Enable();
        _inputSystem_Actions.Player.Attack.started += _ => _isFiring = true;
        _inputSystem_Actions.Player.Attack.canceled+= _ => _isFiring = false;

        float halfPlayerWidth = _spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = _spriteRenderer.bounds.extents.y;

        _minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlayerWidth;
        _maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlayerWidth;

        _minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfPlayerHeight;
        _maxY = _maxHeight = halfPlayerHeight;

        //_inputSystem_Actions.Player.Attack.performed += Attack_performed;
    }

    //private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    if (_canFire < Time.time)
    //    {
    //        Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.9f, 0), Quaternion.identity);
    //        _canFire = Time.time + _fireRate;
    //    }

        
    //}

    private void OnDestroy()
    {
        _inputSystem_Actions.Player.Disable();
        //_inputSystem_Actions.Player.Attack.performed-= Attack_performed;
    }

    private void Update()
    {
        PlayerMovement();

        if(_isFiring && _canFire < Time.time)
        {
            // Instancier dans le jeu le prefab du laser
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.9f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    private void PlayerMovement()
    {
        Vector2 direction2D = _inputSystem_Actions.Player.Move.ReadValue<Vector2>();
        direction2D.Normalize();

        transform.Translate(direction2D * Time.deltaTime * _playerSpeed);

        float clampedX = Mathf.Clamp(transform.position.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(transform.position.y, _minY, _maxY);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
