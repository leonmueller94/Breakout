using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private List<Brick> _bricks = new List<Brick>();

    public event Action<Level> LevelCompleted;

    private void Start()
    {
        PopulateBricksList();
    }

    private void PopulateBricksList()
    {
        var bricks = GetComponentsInChildren<Brick>();
        foreach (var brick in bricks)
        {
            brick.OnBrickDestroyed = () => RemoveBrickFromList(brick);
            _bricks.Add(brick);
        }

        CheckIfLevelCompleted();
}

    private void RemoveBrickFromList(Brick brick)
    {
        if (_bricks.Contains(brick))
        {
            _bricks.Remove(brick);
        }

        CheckIfLevelCompleted();
    }

    private void CheckIfLevelCompleted()
    {
        if (_bricks.Count <= 0)
        {
            LevelCompleted?.Invoke(this);
        }
    }
}
