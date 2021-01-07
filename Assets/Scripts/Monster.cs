﻿using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private Sprite _deadSprite;
    [SerializeField]
    private ParticleSystem _particleSystem;

    private bool _hasDied;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieOnCollision(collision))
        {
            Die();
        }
    }

    private bool ShouldDieOnCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts [0].normal.y < -0.5)
            return true;

        return false;
    }

    private void Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
    }
}
