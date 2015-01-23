using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        InputMovement();
    }

    private void InputMovement()
    {
        float x = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(x * MoveSpeed, rigidbody2D.velocity.y);
    }
}
