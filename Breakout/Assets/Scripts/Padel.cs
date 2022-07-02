using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padel : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool _showCursor = false;
    [SerializeField] private float _startForce = 4f;
    [SerializeField] private AudioSource _paddleAudioSource = null;

    [Header("Component Refernces")]
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private Rigidbody2D _ballRb = null;

    private CapsuleCollider2D _padelCollider = null;
    private Vector2 _screenBounds = Vector2.zero;

    private void Start()
    {
        SetCursorVisible();
        GetComponentRefs();
        CalculateScreenBounds();
    }

    private void SetCursorVisible()
    {
        Cursor.visible = _showCursor;
    }

    private void GetComponentRefs()
    {
        _padelCollider = GetComponent<CapsuleCollider2D>();
    }

    private void CalculateScreenBounds()
    {
        if (_mainCamera == null)
        {
            return;
        }

        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
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
        if (collision.gameObject.tag == "Ball" && _paddleAudioSource != null)
        {
            _paddleAudioSource.time = 0.35f;
            _paddleAudioSource.Play();
        }
    }
}
