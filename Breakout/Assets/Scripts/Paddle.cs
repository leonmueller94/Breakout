using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _startForce = 4f;
    [SerializeField] private AudioClip _paddleHitAudioClip = null;

    [Header("Component Refernces")]
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private Rigidbody2D _ballRb = null;

    private SoundManager _soundManager;
    private CapsuleCollider2D _padelCollider = null;
    private Vector2 _screenBounds = Vector2.zero;

    private void Start()
    {
        GetComponentRefs();
        CalculateScreenBounds();
        ParentBallToPaddle();
    }

    private void GetComponentRefs()
    {
        _padelCollider = GetComponent<CapsuleCollider2D>();
        _soundManager = SoundManager.Instance;
    }

    private void CalculateScreenBounds()
    {
        if (_mainCamera == null)
        {
            return;
        }

        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
    }

    private void ParentBallToPaddle()
    {
        if (_ballRb == null)
        {
            return;
        }

        Transform ball = _ballRb.transform;
        ball.parent = this.transform;
    }

    void Update()
    {
        StartBall();
        MovePadel();
    }

    private void MovePadel()
    {
        if (_mainCamera != null)
        {
            float offset = CalculateOffset();

            var mousePos = Input.mousePosition;
            mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            mousePos.x = Mathf.Clamp(mousePos.x, _screenBounds.x + offset, _screenBounds.x * -1 - offset);
            var transformPos = transform.position;
            transformPos.x = mousePos.x;
            transform.position = transformPos;
        }
    }

    private float CalculateOffset()
    {
        return 1f + (_padelCollider.size.x - 1f);
    }

    private void StartBall()
    {
        if (Input.GetMouseButton(0) && _ballRb.velocity == Vector2.zero)
        {
            _ballRb.transform.SetParent(null);
            var rndX = UnityEngine.Random.Range(-0.25f, 0.25f);
            _ballRb.AddForce(new Vector2(rndX, 1f) * _startForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayPaddleHitSoundOnCollission(collision);
    }

    private void PlayPaddleHitSoundOnCollission(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && _paddleHitAudioClip != null)
        {
            _soundManager.PlayEffectClip(_paddleHitAudioClip, startTime: 0.35f);
        }
    }
}
