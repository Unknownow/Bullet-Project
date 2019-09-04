using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : CharacterGroundCheck {

    private bool _isFriendlyFire;

    private void Start()
    {
        _isFriendlyFire = transform.parent.GetComponent<PlayerManager>().GetIsFriendlyFire();
        _isGrounded = true;
        _groundCheckCollider = gameObject.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_isGrounded && _isFriendlyFire)
        {
            OnNotGrounded();
        }       
    }

}
