using UnityEngine;
using System.Collections;

public class GelGun : MonoBehaviour {

    private Vector3 mousePos;
    private float z;
    public GameObject gelBit;
    private Vector2 shootDir;
    public float shootSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos.magnitude);
        z = Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, z);

        ShootGel();

	
	}

    private void ShootGel()
    {
        if (Input.GetButton("Fire1"))
        {
            float x = shootDir.x; float y = shootDir.y;
            float wobbleFactor = Random.Range(-0.5f, 0.5f);
            shootDir = mousePos - transform.position;
            GameObject go = Instantiate(gelBit, transform.position, Quaternion.identity) as GameObject;
            x = Mathf.Clamp(x, -3.0f, 3.0f);
            y = Mathf.Clamp(y, -3.0f, 3.0f);
            Debug.Log(x + " + " + y);
            go.rigidbody2D.velocity = new Vector2(x * shootSpeed, (y + wobbleFactor) * shootSpeed);
        }
    
    }
}
