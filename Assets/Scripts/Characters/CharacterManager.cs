using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [SerializeField]
    protected float _minVelToDie;

    [SerializeField]
    protected float _gravityScaleForBodyParts;

    protected bool _isDead;

    public void OnCharacterDie()
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<LevelPrefabController>().OnEnemyKilled();
    }

    public float GetMinVelToDie()
    {
        return _minVelToDie;
    }

    public float GetGravityScaleForBodyParts()
    {
        return _gravityScaleForBodyParts;
    }
}
