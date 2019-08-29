using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager {

    [SerializeField]
    private GameObject _gun;

    [SerializeField]
    private float _gunForce;

    private void Start()
    {
        _gun.GetComponent<Gun>().SetGunForce(_gunForce);
    }

    public GameObject GetGun()
    {
        return _gun;
    }

    public void OnPlayerDie()
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<LevelPrefabController>().OnPlayerKilled();
        _gun.GetComponent<Gun>().OnPlayerDie();
    }
}
