using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour {

    [SerializeField]
    private float _initAngle;

    private float _upperAngle;
    private float _lowerAngle;
    private HingeJoint2D _bodyJoint;

	void Start () {
        _bodyJoint = gameObject.GetComponent<HingeJoint2D>();
        if (!_bodyJoint)
            return;
        _upperAngle = _bodyJoint.limits.max;
        _lowerAngle = _bodyJoint.limits.min;
        ChangeJointLimitBaseOnRotation();
        SetJointLimit(_initAngle, _initAngle);
    }

    private void SetJointLimit(float upper, float lower)
    {
        JointAngleLimits2D temp = new JointAngleLimits2D();
        temp.max = upper;
        temp.min = lower;
        _bodyJoint.limits = temp;
    }

    private void ChangeJointLimitBaseOnRotation()
    {
        float angleRotation = transform.rotation.eulerAngles.z;
        if (angleRotation > 180)
            angleRotation -= 360;
        _initAngle += angleRotation;
        _upperAngle += angleRotation;
        _lowerAngle += angleRotation;
    }

    public void ReturnJointToNormalState() {
        if (_bodyJoint != null)
            SetJointLimit(_upperAngle, _lowerAngle);
    }
}
