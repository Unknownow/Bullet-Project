using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [SerializeField]
    private bool _isHolding = false;

    [SerializeField]
    private GameObject _laser;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float power = 10f;

    // Use this for initialization
    void Start () {
        _isHolding = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Drag();
        }
        else
        {
            if (_isHolding)
                Drop();
        }
    }

    private void Drag()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_isHolding)
        {
            float dist = Vector3.Distance(this.transform.position, cursorPosition);
            float maxRange = 30f;
            if (dist > maxRange)
                dist = maxRange;
            _laser.transform.localScale = new Vector3(1.0f, dist * 30, 0);
            Vector2 difference = cursorPosition - this.transform.position;
            float rotationDegreeToCursor = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -rotationDegreeToCursor - transform.rotation.z);
        }
        else
        {
            _isHolding = true;
            _laser.SetActive(true);
        }
    }

    private void Drop()
    {
        _isHolding = false;
        _laser.SetActive(false);

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dist = Vector2.Distance(this.transform.position, cursorPosition);
        Vector2 temp = new Vector2(cursorPosition.x - this.transform.position.x,cursorPosition.y - this.transform.position.y);
        temp = temp / dist;
        GameObject bullet = Instantiate(_bulletPrefab, this.transform.position, transform.rotation);
        bullet.GetComponent<BulletController>().Shoot(temp * power);
    }
}
