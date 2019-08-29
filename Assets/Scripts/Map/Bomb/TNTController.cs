using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTController : MonoBehaviour {

    [SerializeField]
    private float _forceValueOnBody;

    [SerializeField]
    private float _forceValueOnBox;

    [SerializeField]
    private float _minVelocityTrigger;

    [SerializeField]
    private float _delayTimeBeforeDestroy;

    private bool _isExploded;
    private bool _canExplode;
    private Rigidbody2D rb2d;
    private TNTEffectZoneController _effectZone;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        _effectZone = transform.GetChild(0).GetComponent<TNTEffectZoneController>();
        _effectZone.InitValue(_forceValueOnBody, _forceValueOnBox);
        _canExplode = false;
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
        if (!_canExplode && Mathf.Sqrt(Mathf.Pow(rb2d.velocity.x, 2) + Mathf.Pow(rb2d.velocity.y, 2)) > _minVelocityTrigger)
            _canExplode = true;
    }

    IEnumerator DestroyTNTCrate()
    {
        yield return new WaitForSeconds(_delayTimeBeforeDestroy);
        Destroy(gameObject);
    }

    /*
     * Public Functions Below
     */

    public void TriggerBomb()
    {
        _isExploded = true;
        _effectZone.OnTriggerBomb();
        StartCoroutine(DestroyTNTCrate());
    }
}
