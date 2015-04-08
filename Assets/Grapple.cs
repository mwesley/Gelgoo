using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour
{
    public float startWidth = 0.05f;
    public float endWidth = 0.05f;

    LineRenderer line;

    private GameObject player;
    private GameObject hook;
    public GameObject hookPrefab;

    public bool isGrappled;
    float dist;
    float x;
    float y;
    float theta;

    // Use this for initialization
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.SetWidth(startWidth, endWidth);
        line.SetVertexCount(2);
        line.material.color = Color.red;
        line.enabled = false;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isGrappled)
        {
            Debug.Log("Grappling");
            GrappleHook();
        }
        if (isGrappled)
        {
            DrawLine();
            dist = Vector2.Distance(hook.transform.position, player.transform.position);
            if(dist > 10f)
            {
                Destroy(hook);
                Destroy(player.GetComponent<SpringJoint2D>());
                isGrappled = false;
                line.enabled = false;
            }
            Vector2 dir = (hook.transform.position - player.transform.position);
            dir.Normalize();
            x = dir.x;
            y = dir.y;
            theta = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);
        }
        if (Input.GetButtonUp("Fire1") && isGrappled)
        {
            Destroy(hook);
            Destroy(player.GetComponent<SpringJoint2D>());
            isGrappled = false;
            line.enabled = false;
        }
    }

    void GrappleHook()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = new Vector2(mousePos.x - player.transform.position.x, mousePos.y - player.transform.position.y);
        isGrappled = true;
        hook = Instantiate(hookPrefab, player.transform.position, Quaternion.identity) as GameObject;
        hook.rigidbody2D.AddForce(dir * 5, ForceMode2D.Impulse);

    }

    void DrawLine()
    {
        line.enabled = true;
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, hook.transform.position);

        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(hook.transform.position, new Vector2(-x, -y));
        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit2D hit = hits[i];
            Debug.Log("Hit " + hit.transform.name);
        }

    }
}
