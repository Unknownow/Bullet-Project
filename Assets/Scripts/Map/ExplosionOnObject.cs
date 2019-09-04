using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnObject : MonoBehaviour {
    private bool _hasExploded;

    void Start()
    {
        _hasExploded = false;
    }

    private void AddExplosiveForceToObject(Vector3 explosiveSourcePos, float forceValue)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - explosiveSourcePos) * forceValue, ForceMode2D.Impulse);
    }

    public void OnCollideExplosion(Vector2 explosionSorcePos, float forceValue)
    {
        if (_hasExploded)
            return;
        _hasExploded = true;
        AddExplosiveForceToObject(explosionSorcePos, forceValue);
    }

    public void ReturnToNormalState()
    {
        if (_hasExploded)
            _hasExploded = false;
    }
}
