using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunController : MonoBehaviour {

    [SerializeField]
    private Camera _mainCam;



	// Use this for initialization
	void Start () {
		if(_mainCam == null)
        {
            _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        RotateHandByMouse();
	}

    

    private void RotateHandByMouse()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = AngleCal(mousePos, transform.position) + 90;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
    }

    private float AngleCal(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }
}
