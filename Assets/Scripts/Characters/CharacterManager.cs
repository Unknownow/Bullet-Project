using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    private bool _isDead;

    public void OnCharacterDie()
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<LevelPrefabDetail>().OnEnemyKilled();
    }
}
