using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInsideCollisionController : MonoBehaviour {
    [SerializeField]
    private float _forceApplyToBodyPart;


    private Rigidbody2D _parentRigidbody2D;

    private void Start()
    {
        _parentRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Body Part"))
        {
            BodyPartController thisBodyPart = collision.GetComponent<BodyPartController>();
            CenterBodypartController centerBody = thisBodyPart.GetCenterBodyPart();
            centerBody.OnCharacterDie(centerBody.transform.parent);
            thisBodyPart.ApplyForceToThisBodyPart(_parentRigidbody2D.velocity, _forceApplyToBodyPart);
        }

        if (collision.CompareTag("Bomb"))
        {
            collision.GetComponent<TNTController>().TriggerBomb();
        }

    }
}
