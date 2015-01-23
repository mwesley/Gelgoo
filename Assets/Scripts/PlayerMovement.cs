using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;

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
        rigidbody2D.velocity = new Vector2(x * moveSpeed, rigidbody2D.velocity.y);
    }
}
