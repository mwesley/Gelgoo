using UnityEngine;

public class GelGun : MonoBehaviour
{
    public float ShootSpeed;
    public bool Controller;
    public GameObject[] GelArray;
    //public GameObject gelBit;

    private Vector2 _shootDir;
    private Vector3 _mousePos;
    private float _x;
    private float _y;
    private float _z;
    private int _i;

    // Use this for initialization
    void Start()
    {
        _i = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Controller)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mousePos.magnitude);
            _z = Mathf.Atan2((_mousePos.y - transform.position.y), (_mousePos.x - transform.position.x)) * Mathf.Rad2Deg;
        }
        else if (Controller)
        {
            _x = Input.GetAxis("RightX");
            _y = Input.GetAxis("RightY");
            _z = Mathf.Atan2(-_y, _x) * Mathf.Rad2Deg;
        }

        transform.eulerAngles = new Vector3(0, 0, _z);
        ShootGel();
        CycleAmmo();

    }

    private void ShootGel()
    {
        if (Input.GetButton("Fire1") || Input.GetAxis("Right Trigger") != 0)
        {
            float wobbleFactor = Random.Range(-1.0f, 1.0f);
            if (!Controller)
            {
                _shootDir = _mousePos - transform.position;
            }
            else if (Controller)
            {
                _shootDir = new Vector2(_x, -_y);
            }

            GameObject gelbit = Instantiate(GelArray[_i], transform.position, Quaternion.identity) as GameObject;
            Vector2 shootDirNormal = _shootDir.normalized;
            gelbit.rigidbody2D.velocity = new Vector2(shootDirNormal.x * ShootSpeed, (shootDirNormal.y * ShootSpeed) + wobbleFactor);
        }

    }

    private void CycleAmmo()
    {
        if (Input.GetButtonDown("CycleRight"))
        {
            if (_i >= GelArray.Length - 1)
            {
                _i = 0;
            }
            else
            {
                _i++;
            }
        }
    }
}
