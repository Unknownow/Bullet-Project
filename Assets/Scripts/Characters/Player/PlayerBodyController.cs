using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : BodyController {
    [SerializeField]
    protected RotateHandByTouch _handGun;

    public new void ChangeCharacterToDynamic(Transform body)
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<CharacterManager>().OnCharacterDie();
        _handGun.enabled = false;
        for (int i = 0; i < body.childCount; i++)
        {
            Transform temp = body.GetChild(i);
            if (temp.GetComponent<BodyPartController>() != null)
            {
                temp.GetComponent<BodyPartController>().ReturnJointToNormalState();
            }
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ChangeCharacterToDynamic(temp);
        }
    }
}
