using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour
{

    public Transform[] Backgrounds;
    public float Smoothing;

    private float[] _parallaxScales;
    private Transform _cam;
    private Vector3 _previousCamPos;


    void Awake()
    {
        _cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        _previousCamPos = _cam.position;
        _parallaxScales = new float[Backgrounds.Length];

        for(int i = 0; i < Backgrounds.Length; i++)
        {
            _parallaxScales[i] = Backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (_previousCamPos.x - _cam.position.x) * _parallaxScales[i];

            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
        }

        _previousCamPos = _cam.position;
        
    }
}
