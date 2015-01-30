using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed;
    public bool _grounded;
    private float _xFactor;

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        InputMovement();
        Jump();

        if(!_grounded)
            BouncyGel.Bounce();
        if(!_grounded && rigidbody2D.velocity.y == 0)
        {
            _grounded = true;
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
            _xFactor = x * MoveSpeed * 0.75f;
            rigidbody2D.AddForce(new Vector2 (_xFactor, 0));
            if(rigidbody2D.velocity.x >= 10f)
            {
                rigidbody2D.velocity = new Vector2(10f, rigidbody2D.velocity.y);
            }
            Debug.Log(rigidbody2D.velocity.x);
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
}
