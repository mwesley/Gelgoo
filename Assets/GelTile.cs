using UnityEngine;
using System.Collections;

public class GelTile : MonoBehaviour {

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
    void Update()
    {

        if (this.gameObject.tag != "Tile")
        {
            GelUpdate(this.gameObject.tag);
        }

    }

    private void GelUpdate(string tag)
    {
        switch (tag)
        {
            case "Bouncy":
                this.particleSystem.startColor = new Color32(0, 153, 210, 255);
                this.particleSystem.Play();

                break;

            case "Sticky":

                break;


        }
    }
}
