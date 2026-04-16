using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 2f;
    [SerializeField] private int _enemyPoints = 100;
    [SerializeField] private GameObject _explosionPrefab;

    private float _halfEnemyWidth;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _halfEnemyWidth = _spriteRenderer.bounds.extents.x;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _enemySpeed);

        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);

            transform.position = new Vector3(randomX, Camera.main.orthographicSize + 1f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            GameManager.Instance.EnemyDestroyed(_enemyPoints, collision.gameObject.tag);
            Destroy(gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if (collision.gameObject.CompareTag("Laser"))
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);

            collision.gameObject.transform.position = new Vector3(randomX, Camera.main.orthographicSize + 1f, 0f);
        }
        
        
    }


}
