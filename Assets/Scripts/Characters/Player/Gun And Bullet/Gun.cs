using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField]
    private int _numOfBullet;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _laserLine;

    [SerializeField]
    private Transform _gunPoint;

    [SerializeField]
    private float _force;

    [SerializeField]
    private float _bulletInitAngle;

    [SerializeField]
    private float _gunInitAngle;

    private float _bulletRotateAngle;
    private bool _isAiming;
    private bool _isFiring;

    // Use this for initialization
    void Start () {
        _bulletRotateAngle = _bulletInitAngle - _gunInitAngle;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isAiming = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isAiming = false;
            _isFiring = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (_isAiming)
        {
            _laserLine.SetActive(true);
        }
        else
        {
            _laserLine.SetActive(false);
        }
        if (_isFiring)
        {
            if(_numOfBullet > 0)
            {
                OnFiringBullet();
                _numOfBullet -= 1;
            }
            _isFiring = false;
        }
	}


    private void OnFiringBullet()
    {
        Quaternion tempAngle = transform.rotation;
        Vector3 temp = tempAngle.eulerAngles;
        temp.z -= _bulletRotateAngle;
        tempAngle = Quaternion.Euler(temp);
        GameObject bullet = Instantiate(_bulletPrefab, _gunPoint.position, tempAngle);
        bullet.GetComponent<BulletController>().Shoot(_force);
    }

    public void SetNumOfBullet(int bullets)
    {
        this._numOfBullet = bullets;
    }
}

