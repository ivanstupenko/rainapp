using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] ParticleSystem dropsParticleSystem;
    [SerializeField] AudioSource[] dropsSources;
    [SerializeField] AnimationCurve dropCurve;
    [SerializeField] AudioSource rainSource;
    [SerializeField] AnimationCurve rainCurve;
    [SerializeField] float loopLength = 60f;
    [SerializeField] float rainDelay = 20f;
    [Space]
    [SerializeField] GameObject soundPrefab;
    [SerializeField] float soundVolume;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    private int dropsCount = 0;
    private int sourceIndex = -1;
    float[] dropsVolumes;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySound(rainSource, rainDelay));
        dropsVolumes = new float[dropsSources.Length];
        for (var i=0; i < dropsSources.Length; i++)
        {
            dropsVolumes[i] = dropsSources[i].volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dropsParticleSystem.particleCount > dropsCount)
        {
            sourceIndex++;
            if (sourceIndex >= dropsSources.Length)
            {
                sourceIndex = Random.Range(0, dropsSources.Length);
                dropsSources[sourceIndex].pitch = Random.Range(0.8f, 2f);
            }
            dropsSources[sourceIndex].Play();
        }
        dropsCount = dropsParticleSystem.particleCount;

        var arg = (Time.time % loopLength) / loopLength;
        var dropsVolume = dropCurve.Evaluate(arg);
        for (var i = 0; i < dropsSources.Length; i++)
        {
            dropsSources[i].volume = dropsVolume * dropsVolumes[i];
        }
        rainSource.volume = rainCurve.Evaluate(arg);

        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        //{
        //    var go = Instantiate(soundPrefab);
        //    var s = go.GetComponent<AudioSource>();
        //    s.volume = soundVolume;
        //    s.pitch = Random.Range(minPitch, maxPitch);
        //    Destroy(go, 2f);
        //}
    }

    IEnumerator PlaySound(AudioSource s, float delay)
    {
        yield return new WaitForSeconds(delay);
        s.Play();
    }
}
