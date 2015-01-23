using UnityEngine;
using System.Collections;

public class GelGun : MonoBehaviour {

    private Vector3 mousePos;
    private float z;
    public GameObject gelBit;
    private Vector2 shootDir;
    public float shootSpeed;
    public bool controller;
    private float x;
    private float y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!controller)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mousePos.magnitude);
            z = Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg;     
        }
        else if (controller)
        {
            x = Input.GetAxis("RightX");
            y = Input.GetAxis("RightY");
            z = Mathf.Atan2(-y, x) * Mathf.Rad2Deg;
        }

        transform.eulerAngles = new Vector3(0, 0, z);
        ShootGel();

	
	}

    private void ShootGel()
    {
        if (Input.GetButton("Fire1") || Input.GetAxis("Right Trigger") != 0)
        {
            float wobbleFactor = Random.Range(-1.0f, 1.0f);
            if (!controller)
            {
                shootDir = mousePos - transform.position;
            }
            else if (controller)
            {
                shootDir = new Vector2(x, -y);
            }
            GameObject gelbit = Instantiate(gelBit, transform.position, Quaternion.identity) as GameObject;
            Vector2 shootDirNormal = shootDir.normalized;
            gelbit.rigidbody2D.velocity = new Vector2(shootDirNormal.x * shootSpeed, (shootDirNormal.y * shootSpeed) + wobbleFactor);
        }
    
    }
}
