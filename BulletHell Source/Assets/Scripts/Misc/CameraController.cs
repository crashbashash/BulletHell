using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCam;
    private SpriteRenderer camBounds;
    private Transform mapSpawn;
    public bool paused;

    [Range(0,20)]
    public float camSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
        camBounds = transform.Find("Bounds").GetComponent<SpriteRenderer>();
        mapSpawn = transform.Find("Map Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused)
            transform.Translate(new Vector3(camSpeed * Time.deltaTime, 0, 0));
        UpdateCam2Apsect();
    }

    void UpdateCam2Apsect()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = camBounds.bounds.size.x / camBounds.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            mainCam.orthographicSize = camBounds.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            mainCam.orthographicSize = camBounds.bounds.size.y / 2 * differenceInSize;
        }
    }
}
