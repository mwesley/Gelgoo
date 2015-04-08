using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed;
    public bool _grounded;
    public RaycastHit2D GroundRay;

    private float _xFactor;
    private LayerMask _tileMask;
    float x;


	// Use this for initialization
    void Awake()
    {
        _tileMask = LayerMask.GetMask("Tiles");
        _grounded = true;
    }

    void Start()
    {
    }
	
	// Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        InputMovement();

        GroundRay = Physics2D.Raycast(this.transform.position, new Vector2(0,-0.5f), 0.75f, _tileMask.value);

        if (GroundRay.collider != null)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }

        if (_grounded)
        {
            Jump();
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
        if(Input.GetButtonDown("Jump"))
        {
            rigidbody2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BouncyGel.Bounce();
    }

}
