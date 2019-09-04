using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTEffectZoneController : MonoBehaviour {

    private float _forceValueOnBody;
    private float _forceValueOnBox;


    private bool _isExploded;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isExploded)
            return;
        if (collision.CompareTag("Body Part"))
        {
            ExplosionOnBodyController temp = collision.GetComponent<ExplosionOnBodyController>();
            if (temp)
            {
                temp.OnCollideExplosion(transform.position, _forceValueOnBody);
            }
        }
        if (collision.CompareTag("Stone"))
        {
            collision.GetComponent<ExplosionOnObject>().OnCollideExplosion(transform.position, _forceValueOnBox);
        }
        if (collision.CompareTag("Wooden Box"))
        {
            collision.GetComponent<WoodenBoxController>().OnWoodenBoxDestroyed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isExploded)
            return;
        if (collision.CompareTag("Body Part"))
        {
            ExplosionOnBodyController temp = collision.GetComponent<ExplosionOnBodyController>();
            if (temp)
            {
                temp.ReturnToNormalState();
            }
        }
        if (collision.CompareTag("Stone"))
        {
            collision.GetComponent<ExplosionOnObject>().ReturnToNormalState();
        }
        
    }

    /*
     * Public functions below
     */

    public void InitValue(float forceValueOnBody, float forceValueOnBox)
    {
        this._forceValueOnBody = forceValueOnBody;
        this._forceValueOnBox = forceValueOnBox;
    }

    public void OnTriggerBomb()
    {
        _isExploded = true;
    }
}
