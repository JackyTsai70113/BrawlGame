using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{ 
    public float WaitingTimeForAudioClip;
    public SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitSecondsForOpening());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitSecondsForOpening()
    {
        yield return new WaitForSeconds(WaitingTimeForAudioClip);
        sceneLoader.LoadNextScene();
    }
}
