using UnityEngine;

public class GelTile : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
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
                this.GetComponent<ParticleSystem>().startColor = new Color32(0, 153, 210, 255);
                this.GetComponent<ParticleSystem>().Play();
                break;

            case "Sticky":
                this.GetComponent<ParticleSystem>().startColor = new Color32(255, 255, 255, 255);
                this.GetComponent<ParticleSystem>().Play();
                break;

            case "Water":
                this.GetComponent<ParticleSystem>().Stop();
                this.gameObject.tag = "Tile";
                break;


        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
