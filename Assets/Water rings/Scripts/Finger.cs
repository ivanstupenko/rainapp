using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] GameObject lineWavesPrefab;
    [SerializeField] GameObject smallSplashPrefab;
    [SerializeField] Transform spot;
    [SerializeField] float spotReduction = 0.99f;

    private Camera cam;
    private GameObject lineWaves;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spot.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;

            spot.localScale = Vector3.one;
            lineWaves = Instantiate(lineWavesPrefab, transform);
            Instantiate(smallSplashPrefab, transform);
        }

        if (Input.GetMouseButton(0))
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineWaves.transform.parent = null;
            Destroy(lineWaves.gameObject, 5f);
            Instantiate(smallSplashPrefab, transform);
        }

        if (!Input.GetMouseButton(0))
        {
            spot.localScale *= spotReduction;
        }
    }
}
