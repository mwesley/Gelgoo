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
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _tileMask = LayerMask.GetMask("Tiles");
    }

    // Update is called once per frame
    void Update()
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
        if (downBounceRay.collider != null && downBounceRay.collider.tag == "Bouncy" && !_bouncing)
        {
            _player.rigidbody2D.AddForce(new Vector2(0, -_y * 2.1f), ForceMode2D.Impulse);
            _bouncing = true;
        }
        
    }
}
