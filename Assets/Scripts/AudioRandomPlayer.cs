using UnityEngine;

public class AudioRandomPlayer: MonoBehaviour {
    public AudioClip[] clips;
    // Use this for initialization
    private void Start() {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], transform.position);
    }

    // Update is called once per frame
    private void Update() {
    }
}