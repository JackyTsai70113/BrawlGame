using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMusic : MonoBehaviour
{
    public AudioClip loseAudio;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusic());
    }

    // Update is called once per frame
    IEnumerator PlayMusic()
    {
        AudioSource.PlayClipAtPoint(
            loseAudio, Camera.main.transform.position);
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<SceneLoader>().LoadLobbyScene();
    }
}