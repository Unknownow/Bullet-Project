using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnBodyController : MonoBehaviour {


    private bool _hasExploded;
    private CenterBodypartController _bodyController;

    void Start()
    {
        _hasExploded = false;
        _bodyController = gameObject.GetComponent<CenterBodypartController>();
    }

    private void AddExplosiveForceToBodyPart(Transform bodyPart, Vector3 explosiveSorcePos, float forceValue)
    {
        for (int i = 0; i < bodyPart.childCount; i++)
        {
            Transform temp = bodyPart.GetChild(i);
            temp.GetComponent<Rigidbody2D>().AddForce((temp.position - explosiveSorcePos) * forceValue, ForceMode2D.Impulse);
            AddExplosiveForceToBodyPart(temp, explosiveSorcePos, forceValue);
        }
    }

    public void OnCollideExplosion(Vector2 explosionPos, float forceValue)
    {
        if (_hasExploded)
            return;
        _hasExploded = true;
        _bodyController.OnCharacterDie(transform.parent);
        AddExplosiveForceToBodyPart(transform.parent, explosionPos, forceValue);
    }

    public void ReturnToNormalState()
    {
        if (_hasExploded)
            _hasExploded = false;
    }
}
