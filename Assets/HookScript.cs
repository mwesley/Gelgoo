using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour
{
    private GameObject player;
    private GameObject hook;
    private float dist;

    private SpringJoint2D grappleRope;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hook = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float dist = Vector2.Distance(hook.transform.position, player.transform.position);
        hook.rigidbody2D.isKinematic = true;
        if (!grappleRope)
        {
            grappleRope = player.AddComponent<SpringJoint2D>();
            grappleRope.connectedAnchor = Vector3.zero;
            grappleRope.connectedBody = this.rigidbody2D;
            grappleRope.distance = dist;
            grappleRope.dampingRatio = 5f;
        }
    }
}
