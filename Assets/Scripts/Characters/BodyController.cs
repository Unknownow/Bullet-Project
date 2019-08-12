using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    [SerializeField]
    protected HandGunController _handGun;

    public void ChangeCharacterToDynamic(Transform body)
    {
        if (_handGun != null)
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
