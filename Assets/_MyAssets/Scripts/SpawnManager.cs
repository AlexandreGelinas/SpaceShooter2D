using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 5f;

    private float _halfEnemyWidth;
    private SpriteRenderer _spriteRenderer;
    private bool _isSpawning = true;
    private float _initialSpawnTime = 2f;

    private void Start()
    {
        _spriteRenderer = _enemyPrefab.GetComponent<SpriteRenderer>();
        _halfEnemyWidth = _spriteRenderer.bounds.extents.x;
        StartCoroutine(SpawnEnemyCoroutine());
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (_isSpawning) 
        { 
            yield return new WaitForSeconds(_initialSpawnTime);

            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfEnemyWidth,
                    Camera.main.orthographicSize * Camera.main.aspect - _halfEnemyWidth);
            Vector3 spawnPosition = new Vector3(randomX, Camera.main.orthographicSize + 1f, 0f);

            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_minTime, _maxTime));

        }
        
    }

    public void EndSpawning()
    {
        _isSpawning = false;
    }
}
