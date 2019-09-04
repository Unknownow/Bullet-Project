using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroundCheck : MonoBehaviour {

    [SerializeField]
    protected CenterBodypartController _centerBody;


    protected Collider2D _groundCheckCollider;
    protected bool _isGrounded;

    private void Start()
    {
        _isGrounded = true;
        _groundCheckCollider = gameObject.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_isGrounded)
        {
            OnNotGrounded();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded &= (!collision.CompareTag("Terrain") && !collision.CompareTag("Stone") && !collision.CompareTag("Wooden Box") && !collision.CompareTag("Bomb"));
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        _isGrounded |= (collision.CompareTag("Terrain") || collision.CompareTag("Stone") || collision.CompareTag("Wooden Box") || collision.CompareTag("Bomb"));
    }

    protected void OnNotGrounded()
    {
        _centerBody.OnChangeCharacterToDynamic(transform.parent);
        Destroy(gameObject);
    }

    /*
     * Public Functions Below
     */
}
