using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int _health = 0;
    [SerializeField] private AudioSource _brickAudioSource = null;
    
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseHealth(amount: 1);
        PlayAudioClipOnBrickHit(collision);
    }

    private void PlayAudioClipOnBrickHit(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && _brickAudioSource != null)
        {
            _brickAudioSource.Play();
        }
    }

    private void DecreaseHealth(int amount)
    {
        _health -= amount;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
