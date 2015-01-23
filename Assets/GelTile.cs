using UnityEngine;

public class GelTile : MonoBehaviour
{
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
                this.particleSystem.startColor = new Color32(255, 255, 255, 255);
                this.particleSystem.Play();

                break;

            case "Water":
                this.particleSystem.Stop();
                this.gameObject.tag = "Tile";
                break;


        }
    }
}
