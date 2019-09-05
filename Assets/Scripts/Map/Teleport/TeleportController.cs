using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {
    enum direction
    {
        In,
        Out
    };

    [SerializeField]
    private direction _direction;

    [SerializeField]
    private float _angle;

    [SerializeField]
    private float _radiusForBulletOut;

    [SerializeField]
    private TeleportController[] _wayOut;

    [SerializeField]
    private GameObject _bulletPrefab;

    private void OnDrawGizmos()
    {
        if (_direction == 0)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, GetPositionToSpawnBullet(_angle));
    }

    private Vector2 GetPositionToSpawnBullet(float angle)  //remember to convert to radiant
    {
        angle *= Mathf.Deg2Rad;
        return transform.position + (transform.right * Mathf.Cos(angle) + transform.up * Mathf.Sin(angle)) * _radiusForBulletOut;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_direction == direction.Out)
            return;
        if(other.CompareTag("Bullet")){
            foreach (TeleportController item in _wayOut)
            {
                item.ShootBullet(other.GetComponent<Rigidbody2D>().velocity.magnitude);
            }
            Destroy(other.gameObject);
        }
    }

    public void ShootBullet(float velocityMagnitude){
        Vector2 newBulletSpawnPosition = GetPositionToSpawnBullet(_angle);
        Vector2 newBulletDirection =  newBulletSpawnPosition - (Vector2) transform.position;

        GameObject bullet = Instantiate(_bulletPrefab, newBulletSpawnPosition, Quaternion.FromToRotation(Vector2.up, newBulletDirection));
        bullet.GetComponent<BulletController>().Shoot(newBulletDirection.normalized * velocityMagnitude);
    }
}
