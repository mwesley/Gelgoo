using UnityEngine;
using System.Collections;

public class BouncyGel : MonoBehaviour
{
    private static GameObject _player;
    private static float _y;
    private static float _x;
    private static LayerMask _tileMask;
    private static bool _bouncing;
    private float _bounceTimer;


    // Use this for initialization
    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _tileMask = LayerMask.GetMask("Tiles");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        _y = _player.rigidbody2D.velocity.y;
        _x = _player.rigidbody2D.velocity.x;

        if(_bouncing)
        {
            _bounceTimer += Time.deltaTime;
            if(_bounceTimer >= 0.5f)
            {
                _bouncing = false;
                _bounceTimer = 0f;
            }
        }
    }

    public static void Bounce()
    {
        

        RaycastHit2D downBounceRay = Physics2D.Raycast(_player.transform.position, new Vector2(0,-1), 0.75f, _tileMask.value);
        RaycastHit2D upBounceRay = Physics2D.Raycast(_player.transform.position, new Vector2(0, 1), 0.75f, _tileMask.value);
        RaycastHit2D leftBounceRay = Physics2D.Raycast(_player.transform.position, new Vector2(-1, 0), 0.75f, _tileMask.value);
        RaycastHit2D rightBounceRay = Physics2D.Raycast(_player.transform.position, new Vector2(1, 0), 0.75f, _tileMask.value);

        if (downBounceRay.collider != null && downBounceRay.collider.tag == "Bouncy" && !_bouncing)
        {
            _player.rigidbody2D.velocity = new Vector2 (_x, -_y);
            _bouncing = true;
        }
        if (upBounceRay.collider != null && upBounceRay.collider.tag == "Bouncy" && !_bouncing)
        {
            _player.rigidbody2D.velocity = new Vector2(_x, -_y);
            _bouncing = true;
        }
        if (leftBounceRay.collider != null && leftBounceRay.collider.tag == "Bouncy")
        {
            Debug.Log("Bounce!");
            _player.rigidbody2D.velocity = new Vector2(-_x, 5);
            _bouncing = true;
        }
        if (rightBounceRay.collider != null && rightBounceRay.collider.tag == "Bouncy")
        {
            Debug.Log("Bounce!");
            _player.rigidbody2D.velocity = new Vector2(-_x, 5);
            _bouncing = true;
        }
        
    }
}
