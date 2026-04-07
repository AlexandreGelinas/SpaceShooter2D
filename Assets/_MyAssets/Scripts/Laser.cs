using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserSpeed = 15f;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);

        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
}
