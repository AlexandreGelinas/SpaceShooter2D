using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private Image _imgPlayerLives;
    [SerializeField] private Sprite[] _playerLivesSpritesArray;

    Player _player;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        ChangeLivesDisplayImage();
        GameManager.Instance.OnEnemyDestroyed += GameManager_OnEnemyDestroyed;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _txtScore.text = $"Pointage : {GameManager.Instance.PlayerScore}";
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDestroyed -= GameManager_OnEnemyDestroyed;
    }

    private void GameManager_OnEnemyDestroyed(object sender, GameManager.OnEnemyDestroyedEventArgs e)
    {
        if (e.DestroyedObjectTag == "Laser")
        {
            UpdateScore();
        }
        else if (e.DestroyedObjectTag == "Player")
        {
            ChangeLivesDisplayImage();
        }
    }

    private void ChangeLivesDisplayImage()
    {
        _imgPlayerLives.sprite = _playerLivesSpritesArray[_player.PlayerLife];
    }
}
