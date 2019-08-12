using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {


    private bool _hasExploded;
    private BodyController _bodyController;

    void Start()
    {
        _hasExploded = false;
        _bodyController = gameObject.GetComponent<BodyController>();
    }

    private void AddExplosiveForceToBodyPart(Transform bodyPart, Vector3 explosionPos, float forceValue)
    {
        for (int i = 0; i < bodyPart.childCount; i++)
        {
            Transform temp = bodyPart.GetChild(i);
            temp.GetComponent<Rigidbody2D>().AddForce((temp.position - explosionPos) * forceValue, ForceMode2D.Impulse);
            AddExplosiveForceToBodyPart(temp, explosionPos, forceValue);
        }
    }

    public void OnCollideExplosion(Vector2 explosionPos, float forceValue)
    {
        if (_hasExploded)
            return;
        _hasExploded = true;
        _bodyController.ChangeCharacterToDynamic(transform.parent);
        AddExplosiveForceToBodyPart(transform.parent, explosionPos, forceValue);
    }

    public void ReturnToNormalState()
    {
        if (_hasExploded)
            _hasExploded = false;
    }
}
