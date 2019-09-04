using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenterBodypartController : CenterBodypartController {
    [SerializeField]
    protected RotateHandByTouch _handGun;

    public new void OnCharacterDie(Transform transformOfCharacter)
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<CharacterManager>().OnCharacterDie();
        _handGun.enabled = false;
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
            }
            OnCharacterDie(bodyPart);
        }
    }
}
