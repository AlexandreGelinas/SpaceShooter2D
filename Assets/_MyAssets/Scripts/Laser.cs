using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _diffFromPlayerSpeed = 5;
    private float _laserSpeed = 15f;

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        _laserSpeed = player.PlayerSpeed + _diffFromPlayerSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);

        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
}
