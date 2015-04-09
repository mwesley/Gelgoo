using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _hook;
    private float _dist;
    private SpringJoint2D _grappleRope;

    private Grapple _grappleScript;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _hook = this.gameObject;
        _grappleScript = _player.GetComponent<Grapple>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpAndDownRope();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float dist = Vector2.Distance(_hook.transform.position, _player.transform.position);
        _hook.GetComponent<Rigidbody2D>().isKinematic = true;
        if (!_grappleRope)
        {
            _grappleRope = _player.AddComponent<SpringJoint2D>();
            _grappleRope.enableCollision = true;
            _grappleRope.connectedAnchor = Vector3.zero;
            _grappleRope.connectedBody = this.GetComponent<Rigidbody2D>();
            _grappleRope.distance = dist;
            _grappleRope.dampingRatio = 5f;
            _grappleScript.IsHooked = true;
        }
    }

    void MoveUpAndDownRope()
    {
        float y =Input.GetAxis("Vertical");
        if(y > 0)
        {
            _grappleRope.distance -= Time.deltaTime * 3;
        } 
        else if(y < 0)
        {
            _grappleRope.distance += Time.deltaTime * 3;
        }
    }
}
