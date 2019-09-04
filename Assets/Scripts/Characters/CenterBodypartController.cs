using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBodypartController : MonoBehaviour {
    
    protected float _minVelToDie;
    protected bool _isDead;
    protected bool _isDynamic;

    private void Start()
    {
        _minVelToDie = transform.parent.GetComponent<CharacterManager>().GetMinVelToDie();
    }

    public void OnCharacterDie(Transform transformOfCharacter)
    {
        if (_isDead)
            return;
        Debug.Log("DIE!");
        _isDead = true;
        _isDynamic = true;
        transform.parent.GetComponent<CharacterManager>().OnCharacterDie();
        for (int i = 0; i < transformOfCharacter.childCount; i++)
        {
            Transform bodyPart = transformOfCharacter.GetChild(i);
            BodyPartController bodyPartController;
            Rigidbody2D bodyPartRB2D;
            if (bodyPartController = bodyPart.GetComponent<BodyPartController>())
            {
                bodyPartController.ReturnJointToNormalState();
            }
            if(bodyPartRB2D = bodyPart.GetComponent<Rigidbody2D>())
            {
                bodyPartRB2D.bodyType = RigidbodyType2D.Dynamic;
                bodyPartRB2D.gravityScale = transform.parent.GetComponent<CharacterManager>().GetGravityScaleForBodyParts();
            }
            OnCharacterDie(bodyPart); 
        }
    }

    public void OnChangeCharacterToDynamic(Transform transformOfCharacter)
    {
        if (_isDead || _isDynamic)
            return;
        _isDynamic = true;
        for (int i = 0; i < transformOfCharacter.childCount; i++)
        {
            Transform bodyPart = transformOfCharacter.GetChild(i);
            BodyPartController bodyPartController;
            Rigidbody2D bodyPartRB2D;
            if (bodyPartController = bodyPart.GetComponent<BodyPartController>())
            {
                bodyPartController.ReturnJointToNormalState();
            }
            if (bodyPartRB2D = bodyPart.GetComponent<Rigidbody2D>())
            {
                bodyPartRB2D.bodyType = RigidbodyType2D.Dynamic;
                bodyPartRB2D.gravityScale = transform.parent.GetComponent<CharacterManager>().GetGravityScaleForBodyParts();
            }
            OnChangeCharacterToDynamic(bodyPart);
        }
    }

    public float GetMinVelToDie()
    {
        return _minVelToDie;
    }
}
