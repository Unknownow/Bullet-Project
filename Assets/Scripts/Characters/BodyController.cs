using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    protected bool _isDead;

    public void ChangeCharacterToDynamic(Transform body)
    {
        if (_isDead)
            return;
        _isDead = true;
        transform.parent.GetComponent<CharacterManager>().OnCharacterDie();
        for (int i = 0; i < body.childCount; i++)
        {
            Transform bodyPart = body.GetChild(i);
            if (bodyPart.GetComponent<BodyPartController>() != null)
            {
                bodyPart.GetComponent<BodyPartController>().ReturnJointToNormalState();
            }
            bodyPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ChangeCharacterToDynamic(bodyPart);
        }
    }
}
