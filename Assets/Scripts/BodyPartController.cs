using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour {

    [SerializeField]
    private float _initAngle;

    private float _upperAngle;
    private float _lowerAngle;
    private HingeJoint2D _bodyJoint;


	// Use this for initialization
	void Start () {
        _bodyJoint = gameObject.GetComponent<HingeJoint2D>();
        _upperAngle = _bodyJoint.limits.max;
        _lowerAngle = _bodyJoint.limits.min;
        SetJointLimit(_initAngle, _initAngle);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetJointLimit(_upperAngle, _lowerAngle);
        }
	}

    private void SetJointLimit(float upper, float lower)
    {
        JointAngleLimits2D temp = new JointAngleLimits2D();
        temp.max = upper;
        temp.min = lower;
        _bodyJoint.limits = temp;
    }

    private float AngleCal(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }
}
