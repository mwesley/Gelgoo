using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    public RaycastHit2D GroundRay;

    private float _xFactor;
    private LayerMask _tileMask;
    private Rigidbody2D _playerBody;
    private bool _grounded;

    float x;


    // Use this for initialization
    void Awake()
    {
        _tileMask = LayerMask.GetMask("Tiles");
        _grounded = true;
        _playerBody = this.GetComponent<Rigidbody2D>();
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

        GroundRay = Physics2D.Raycast(this.transform.position, new Vector2(0, -0.5f), 0.75f, _tileMask.value);

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
            this._playerBody.fixedAngle = true;
            this.transform.rotation = Quaternion.identity;
            Jump();
        }
    }

    private void InputMovement()
    {
        float x = Input.GetAxis("Horizontal");
        if (_grounded)
        {
            _playerBody.velocity = new Vector2(x * MoveSpeed, _playerBody.velocity.y);
        }
        else if (!_grounded)
        {
            _xFactor = x * MoveSpeed;
            _playerBody.AddForce(new Vector2(_xFactor, 0));
            if (_playerBody.velocity.x >= 10f)
            {
                _playerBody.velocity = new Vector2(10f, _playerBody.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _playerBody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BouncyGel.Bounce();
    }

}
