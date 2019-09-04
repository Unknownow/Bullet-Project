using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBoxController : MonoBehaviour {
    [SerializeField]
    private float _timeBeforeDestroy;

    private bool _isDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            OnWoodenBoxDestroyed();
        }
    }

    IEnumerator DestroyWoodenBox()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);
        Destroy(gameObject);
    }

    public void OnWoodenBoxDestroyed()
    {
        if (_isDestroy)
            return;
        _isDestroy = true;
        StartCoroutine(DestroyWoodenBox());
    }
}
