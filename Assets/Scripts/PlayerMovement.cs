using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed;
    public bool _grounded;
    public RaycastHit2D GroundRay;

    private float _xFactor;
    private LayerMask _tileMask;

	// Use this for initialization
    void Awake()
    {
        _tileMask = LayerMask.GetMask("Tiles");
        _grounded = true;
    }
	
	// Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        InputMovement();
        Jump();
        

        GroundRay = Physics2D.Raycast(this.transform.position, new Vector2(0,-0.5f), 0.75f, _tileMask.value);

        if (GroundRay.collider != null)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }

    }

    private void InputMovement()
    {
        float x = Input.GetAxis("Horizontal");
        if (_grounded)
        {
            rigidbody2D.velocity = new Vector2(x * MoveSpeed, rigidbody2D.velocity.y);
        }
        else if(!_grounded)
        {
            _xFactor = x * MoveSpeed;
            rigidbody2D.AddForce(new Vector2 (_xFactor, 0));
            if(rigidbody2D.velocity.x >= 10f)
            {
                rigidbody2D.velocity = new Vector2(10f, rigidbody2D.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && _grounded)
        {
            rigidbody2D.AddForce(new Vector2 (0f, 7.5f), ForceMode2D.Impulse);
            _grounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BouncyGel.Bounce();
    }

}
