using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrigger : MonoBehaviour
{
    private BoxCollider2D _collider;
    private static DeadZoneTrigger _instance;

    public static DeadZoneTrigger Instance { get { return _instance; } }

    public event Action BallHitDeadZone;

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

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collider != null && collision.gameObject.tag == "Ball")
        {
            BallHitDeadZone?.Invoke();
        }
    }
}
