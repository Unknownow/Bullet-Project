using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField]
    private float _maxDistanceToCastRay = 3;

    [SerializeField]
    private LayerMask _reflectLayer;

    [SerializeField]
    private float _liveTime = 10;

    private Rigidbody2D _bulletBody;
    private Vector2 _direction;
    private int _countOfDirectionChanges;

    private void Start()
    {
        _bulletBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = DrawRayToReflect();
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_liveTime);
        Destroy(gameObject);
    }

    private Vector2 DrawRayToReflect()
    {
        Vector2 direction;
        Ray2D ray = new Ray2D(transform.position, _bulletBody.velocity);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _maxDistanceToCastRay, _reflectLayer);
        if (hit)
        {
            direction = Vector2.Reflect(_bulletBody.velocity, hit.normal);
        }
        else
        {
            direction = _bulletBody.velocity;
        }
        return direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.up = _direction;
        _bulletBody.velocity = _direction;
    }

    /*
     * Public functions below
     */

    public void Shoot(float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
        StartCoroutine(DestroyBullet());
    }

    public void Shoot(Vector2 startVelocity)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = startVelocity;
        StartCoroutine(DestroyBullet());
    }

    public int GetCountOfDirectionChanges()
    {
        return _countOfDirectionChanges;
    }

    public void SetCountOfDirectionChanges(int num)
    {
        _countOfDirectionChanges = num;
    }
}
