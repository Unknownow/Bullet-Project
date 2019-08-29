using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionChangeController : MonoBehaviour {
    [SerializeField]
    private int _maxDirectionsChangesOfTheBullet;

    [SerializeField]
    private float _radiusForBulletOut;

    [SerializeField]
    private float[] _angleOfEachBullet;

    [SerializeField]
    private GameObject _bulletPrefab;

    private Vector2 _oldVelocity;
    private Vector2[] _startingPos;
    private Collider2D _collider2D;

    private void Start()
    {
        _collider2D = gameObject.GetComponent<Collider2D>();
        _startingPos = new Vector2[_angleOfEachBullet.Length];
        for(int i = 0; i < _angleOfEachBullet.Length; i++)
        {
            _startingPos[i] = GetPositionToSpawnBullet(_angleOfEachBullet[i] * Mathf.Deg2Rad);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < _angleOfEachBullet.Length; i++)
        {
            Gizmos.DrawLine(transform.position, GetPositionToSpawnBullet(_angleOfEachBullet[i] * Mathf.Deg2Rad));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            int currentDirectionChangesCount = collision.GetComponent<BulletController>().GetCountOfDirectionChanges();
            if (currentDirectionChangesCount >= _maxDirectionsChangesOfTheBullet)
                return;

            _oldVelocity = collision.GetComponent<Rigidbody2D>().velocity;
            ChangeBulletDirection(currentDirectionChangesCount);
            Destroy(collision.gameObject);
        }
    }

    private Vector2 GetPositionToSpawnBullet(float angle)
    {
        return transform.position + (transform.right * Mathf.Cos(angle) + transform.up * Mathf.Sin(angle)) * _radiusForBulletOut;
    }

    private void ChangeBulletDirection(int currentDirectionChangesCount)
    {
        StartCoroutine(TurnOffCollider());
        currentDirectionChangesCount += 1;
        for(int i = 0; i < _angleOfEachBullet.Length; i++)
        {
            Vector2 direction = GetPositionToSpawnBullet(_angleOfEachBullet[i] * Mathf.Deg2Rad) - (Vector2)transform.position;
            GameObject bullet = Instantiate(_bulletPrefab, _startingPos[i], Quaternion.FromToRotation(Vector2.up, direction));

            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.SetCountOfDirectionChanges(currentDirectionChangesCount);
            bulletController.Shoot(direction.normalized * _oldVelocity.magnitude);
        }
    }

    IEnumerator TurnOffCollider()
    {
        _collider2D.enabled = false;
        yield return new WaitForSeconds(.1f);
        _collider2D.enabled = true;
    }
}
