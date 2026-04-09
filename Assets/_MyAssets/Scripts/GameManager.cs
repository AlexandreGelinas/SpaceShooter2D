using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _playerScore = 0;

    public event EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;
    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public string DestroyedObjectTag; // tag de l'objet qui à détruit l'ennemi
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Un deuxième GameManager tente d'être crée!");
        }
    }

    public void EnemyDestroyed(int p_enemyPoints, string p_gameObjectTag)
    {
        if(p_gameObjectTag == "Laser")
        {
            _playerScore += p_enemyPoints;
            Debug.Log($"Score du joueur: {_playerScore}");
        }

        OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs
        {
            DestroyedObjectTag = p_gameObjectTag
        });
    }
}
