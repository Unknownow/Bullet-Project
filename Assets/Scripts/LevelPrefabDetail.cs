using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabDetail : MonoBehaviour {
    [SerializeField]
    private int _goldenBullet;

    [SerializeField]
    private int _normalBullet;

    [SerializeField]
    private int _numOfEnemy;

    [SerializeField]
    private Gun _gun;

    private void Start()
    {
        _gun.SetNumOfBullet(_goldenBullet + _normalBullet);
    }

    public void OnEnemyKilled()
    {
        _numOfEnemy -= 1;
        if (_numOfEnemy <= 0)
            OnFinishStage();
    }

    private void OnFinishStage()
    {
        Debug.Log("WIN!");
    }

}
