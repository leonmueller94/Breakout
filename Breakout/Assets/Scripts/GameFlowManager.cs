using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private bool _showCursor = false;

    [Header("Game Settings")]
    [SerializeField] private int _health = 3;
    [SerializeField] private List<Level> _levels = new List<Level>();

    [Header("References")]
    [SerializeField] private GameObject _ball = null;
    [SerializeField] private GameObject _paddle = null;

    private DeadZoneTrigger _deadZoneTrigger = null;

    private static GameFlowManager _instance;
    public static GameFlowManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetCursorVisible();

        _deadZoneTrigger = DeadZoneTrigger.Instance;
        _deadZoneTrigger.BallHitDeadZone += OnBallHitDeadZone;
    }

    private void SetCursorVisible()
    {
        Cursor.visible = _showCursor;
    }

    private void OnBallHitDeadZone()
    {
        DecreaseHealth(amount: 1);
        ParentBallToPaddle();
    }

    private void ParentBallToPaddle()
    {
        _ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector3 resetBallPos = new Vector3(_paddle.transform.position.x, -2.75f, 0f);
        _ball.transform.position = resetBallPos;
        _ball.transform.parent = _paddle.transform;
    }

    private void DecreaseHealth(int amount)
    {
        _health -= amount;
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    private void OnDisable()
    {
        if (_deadZoneTrigger != null)
        {
            _deadZoneTrigger.BallHitDeadZone -= OnBallHitDeadZone;
        }
    }



}
