using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grapple : MonoBehaviour
{
    public float startWidth = 0.05f;
    public float endWidth = 0.05f;

    LineRenderer line;

    private GameObject _player;
    private GameObject _hook;
    public GameObject hookPrefab;

    public bool IsGrappled;
    public bool IsHooked;
    float dist;
    float x;
    float y;
    float theta;

    LayerMask _hookLayer;

    public RaycastHit2D hit;

    public bool objectHook;

    private HookScript _hookScript;

    public List<Vector2> ropePoints;

    private float _cachedDir = 0;
    public Vector2 hookPosition;

    // Use this for initialization
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.SetWidth(startWidth, endWidth);
        line.SetVertexCount(2);
        line.material.color = Color.red;
        line.enabled = false;

        _player = GameObject.FindWithTag("Player");
        IsHooked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !IsGrappled)
        {
            Debug.Log("Grappling");
            GrappleHook();
        }
        if (IsGrappled)
        {
            DrawLine();
            dist = Vector2.Distance(_hook.transform.position, _player.transform.position);
            if(dist > 7.5f && !IsHooked)
            {
                Destroy(_hook);
                Destroy(_player.GetComponent<SpringJoint2D>());
                IsGrappled = false;
                line.enabled = false;
            }
            Vector2 dir = (_hook.transform.position - _player.transform.position);
            dir.Normalize();
            x = dir.x;
            y = dir.y;
            theta = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);
            //CheckLineOfSight();
        }
        if (Input.GetButtonUp("Fire1") && IsGrappled)
        {
            Destroy(_hook);
            Destroy(_player.GetComponent<SpringJoint2D>());
            line.enabled = false;
            IsHooked = false;
            ropePoints.Clear();
            Debug.Log("Done!");
            hookPosition = _hook.transform.position;
            IsGrappled = false;
            ResetLine();
        }
        WrapRope();

        if(!IsGrappled)
        {
            ResetLine();
            ropePoints.Clear();
        }
    }

    void GrappleHook()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = new Vector2(mousePos.x - _player.transform.position.x, mousePos.y - _player.transform.position.y);
        IsGrappled = true;
        _hook = Instantiate(hookPrefab, _player.transform.position, Quaternion.identity) as GameObject;
        _hook.GetComponent<Rigidbody2D>().AddForce(dir * 5, ForceMode2D.Impulse);
        _hookScript = _hook.GetComponent<HookScript>();

    }

    void DrawLine()
    {
        if (ropePoints.Count == 0)
        {
            line.SetVertexCount(2);
            line.SetPosition(1, _hook.transform.position);
        }
        line.SetPosition(0, _player.transform.position);
        line.enabled = true;
    }

    /*void CheckLineOfSight()
    {
        Vector2 dir = new Vector2(_player.transform.position.x - _hook.transform.position.x, _player.transform.position.y - _hook.transform.position.y);
        hit = Physics2D.Linecast(_player.transform.position, _hook.transform.position);
        Vector2 hitPoint = hit.point;
        //HookedOnObject(hitPoint);

        if (hit.transform.tag != "Hook")
        {
            //_hook.transform.position = hit.point;
            objectHook = true;
        }
        else
        {
            line.SetVertexCount(2);
            objectHook = false;
        }
    }

    void HookedOnObject(Vector2 hitPoint)
    {
        if (objectHook)
        {
            line.SetVertexCount(3);
            line.SetPosition(1, hitPoint);
            line.SetPosition(2, _hook.transform.position);
        }
    }*/

    void WrapRope()
    {
        hit = Physics2D.Linecast(_player.transform.position, _hook.transform.position);
        if (ropePoints.Count == 0)
        {
            RaycastHit2D wrapTest = Physics2D.Linecast(_player.transform.position, _hook.transform.position);
            if (wrapTest.transform.tag != "Hook")
            {
                AddPoint(wrapTest.point, ropePoints.Count +1);
                Vector2 prevPoint = ropePoints[ropePoints.Count - 1];
                wrapTest = Physics2D.Linecast(_player.transform.position, prevPoint);
            }
        }
        else if(ropePoints.Count == 1)
        {
            Vector2 playerToPoint = new Vector2(ropePoints[0].x - _player.transform.position.x, ropePoints[0].y - _player.transform.position.y);
            Vector2 pointToHook = new Vector2(_hook.transform.position.x - ropePoints[0].x, _hook.transform.position.y - ropePoints[0].y);
            Debug.DrawRay(_player.transform.position, playerToPoint, Color.red);
            Debug.DrawRay(ropePoints[0], pointToHook, Color.green);
            CompareVectors(playerToPoint, pointToHook);

        }
    }

    void AddPoint(Vector2 hitPoint, int index)
    {
        ropePoints.Add(hitPoint);
        line.SetVertexCount(index +2);
        line.SetPosition(index, hitPoint);
        line.SetPosition(index +1, _hook.transform.position);
        SpringJoint2D grapplePoint = _player.GetComponent<SpringJoint2D>();
        float dist = Vector2.Distance(_player.transform.position, hit.point);
        grapplePoint.distance = dist;
        grapplePoint.connectedBody = null;

        grapplePoint.connectedAnchor = hitPoint;

    }

    void CompareVectors(Vector2 A, Vector2 B)
    {
        float dir = Mathf.Atan2(A.y, A.x) - Mathf.Atan2(B.y, B.x);
        //Debug.Log(dir);
        if (_cachedDir < 0 && dir > 0)
        {
            RemoveLastPoint();
            _cachedDir = 0;
        }
        else if (_cachedDir > 0 && dir < 0)
        {
            RemoveLastPoint();
            _cachedDir = 0;
        }
        else
        {
            _cachedDir = dir;
        }
    }

    void RemoveLastPoint()
    {
        Debug.Log("Removing");
        ropePoints.RemoveAt(0);
        SpringJoint2D grapplePoint = _player.GetComponent<SpringJoint2D>();
        float dist = Vector2.Distance(_player.transform.position, _hook.transform.position)- 1f;
        grapplePoint.distance = dist;
        grapplePoint.connectedBody = _hook.GetComponent<Rigidbody2D>();

        grapplePoint.connectedAnchor = Vector2.zero;

    }

    void ResetLine()
    {
        line.SetVertexCount(2);
        line.SetPosition(1, hookPosition);
        line.SetPosition(0, _player.transform.position);
        line.enabled = false;
    }
}
