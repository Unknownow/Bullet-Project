using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBodypartController : MonoBehaviour {
    protected bool _isDead;
    protected bool _isDynamic;

    public void OnCharacterDie(Transform transformOfCharacter)
    {
        if (_isDead)
            return;
        _isDead = true;
        _isDynamic = true;
        transform.parent.GetComponent<CharacterManager>().OnCharacterDie();
        for (int i = 0; i < transformOfCharacter.childCount; i++)
        {
            Transform bodyPart = transformOfCharacter.GetChild(i);
            if (bodyPart.GetComponent<BodyPartController>() != null)
            {
                bodyPart.GetComponent<BodyPartController>().ReturnJointToNormalState();
            }
            bodyPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
            if (bodyPart.GetComponent<BodyPartController>() != null)
            {
                bodyPart.GetComponent<BodyPartController>().ReturnJointToNormalState();
            }
            bodyPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            OnChangeCharacterToDynamic(bodyPart);
        }
    }
}
