using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private IntVariable _playerHealth = null;
    [SerializeField] private TMPro.TextMeshProUGUI _healthText = null;

    void Start()
    {
        UpdatePlayerHealthText(_playerHealth.RuntimeValue);
        _playerHealth.RuntimeValueChanged += OnRuntimeValueChanged;
    }

    private void UpdatePlayerHealthText(int health)
    {
        _healthText.text = $"Lives left: {health}";
    }

    private void OnDisable()
    {
        _playerHealth.RuntimeValueChanged -= OnRuntimeValueChanged;
    }

    private void OnRuntimeValueChanged(int health)
    {
        UpdatePlayerHealthText(health);
    }
}
