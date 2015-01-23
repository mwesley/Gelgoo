using UnityEngine;
using System.Collections;

public class GelBitScript : MonoBehaviour {


	// Use this for initialization
    void Start()
    {

    }

	// Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.tag = gameObject.tag;
        }
        Destroy(this.gameObject, 0.02f);

    }
}
