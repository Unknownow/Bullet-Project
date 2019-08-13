using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTController : MonoBehaviour {

    [SerializeField]
    private float _forceValue;

    [SerializeField]
    private float _minVelocityTrigger;

    private bool _isExploded;
    private bool _canExplode;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        _canExplode = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isExploded)
            return;
        if (collision.CompareTag("Body Part"))
        {
            ExplosionController temp = collision.GetComponent<ExplosionController>();
            if (temp)
            {
                temp.OnCollideExplosion(transform.position, _forceValue);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isExploded)
            return;
        if (collision.CompareTag("Body Part"))
        {
            ExplosionController temp = collision.GetComponent<ExplosionController>();
            if (temp)
            {
                temp.ReturnToNormalState();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Terrain") && _canExplode)
        {
            TriggerBomb();
            _canExplode = false;
            StartCoroutine(DestroyTNTCrate());
        }        
    }

    private void Update()
    {
        if (!_canExplode && Mathf.Abs(rb2d.velocity.y) > _minVelocityTrigger)
            _canExplode = true;
    }

    IEnumerator DestroyTNTCrate()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public void TriggerBomb()
    {
        _isExploded = true;
    }
}
