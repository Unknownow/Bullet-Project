using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    [SerializeField]
    private float _minVelocityToKill;

    private Rigidbody2D _boxBody;

    private void Start()
    {
        _boxBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Body Part"))
        {
            if(Mathf.Sqrt(Mathf.Pow(_boxBody.velocity.x, 2) + Mathf.Pow(_boxBody.velocity.y, 2)) >= _minVelocityToKill)
            {
                CenterBodypartController centerBody = collision.transform.GetComponent<BodyPartController>().GetCenterBodyPart();
                centerBody.OnCharacterDie(centerBody.transform.parent);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Body Part"))
        {
            CenterBodypartController centerBody = collision.transform.GetComponent<BodyPartController>().GetCenterBodyPart();
            centerBody.OnChangeCharacterToDynamic(centerBody.transform.parent);
        }
    }
}
