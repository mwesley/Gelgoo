using UnityEngine;
using System.Collections;

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

    RaycastHit2D hit;

    public bool objectHook;

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

        _hookLayer = LayerMask.NameToLayer("Tiles");
        Debug.Log(_hookLayer.value);
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
            if(dist > 10f && !IsHooked)
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
            CheckLineOfSight();
        }
        if (Input.GetButtonUp("Fire1") && IsGrappled)
        {
            Destroy(_hook);
            Destroy(_player.GetComponent<SpringJoint2D>());
            IsGrappled = false;
            line.enabled = false;
            IsHooked = false;
        }
    }

    void GrappleHook()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = new Vector2(mousePos.x - _player.transform.position.x, mousePos.y - _player.transform.position.y);
        IsGrappled = true;
        _hook = Instantiate(hookPrefab, _player.transform.position, Quaternion.identity) as GameObject;
        _hook.GetComponent<Rigidbody2D>().AddForce(dir * 5, ForceMode2D.Impulse);

    }

    void DrawLine()
    {
        line.enabled = true;
        line.SetPosition(0, _player.transform.position);
        line.SetPosition(1, _hook.transform.position);
    }

    void CheckLineOfSight()
    {
        Vector2 dir = new Vector2(_player.transform.position.x - _hook.transform.position.x, _player.transform.position.y - _hook.transform.position.y);
        hit = Physics2D.Linecast(_player.transform.position, _hook.transform.position);
        Vector2 hitPoint = hit.point;
        HookedOnObject(hitPoint);

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
    }
}
