using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabController : MonoBehaviour {
    [SerializeField]
    private int _goldenBullet;

    [SerializeField]
    private int _normalBullet;

    [SerializeField]
    private int _numOfEnemy;

    [SerializeField]
    private GameObject _gun;

    private void Start()
    {
        _gun = transform.Find("Player").GetComponent<PlayerManager>().GetGun();

        _gun.GetComponent<Gun>().SetNumOfBullet(_normalBullet + _goldenBullet);
    }

    private void OnFinishStage()
    {
        Debug.Log("WIN!");
    }

    /*
     * public functions below
     */
    
    public void OnFiringBullet()
    {
        if (_normalBullet <= 0)
        {
            return;
        }

        if (_goldenBullet > 0)
        {
            _goldenBullet -= 1;
        }
        else
        {
            _normalBullet -= 1;
            if(_normalBullet <= 0)
                OnGameOver();
        }
    }

    public void OnGameOver()
    {
        Debug.Log("LevelPrefabController.OnGameOver()");
        //TODO: Insert gameover code here.
    }

    public void OnPlayerKilled()
    {
        Debug.Log("LevelPrefabController.OnPlayerKilled()");
        //TODO: Insert on player killed code here
    }

    public void OnEnemyKilled()
    {
        _numOfEnemy -= 1;
        if (_numOfEnemy <= 0)
            OnFinishStage();
    }

    public int GetStars()
    {
        return _normalBullet;
    }

}
