using UnityEngine;

public class MarkerScript: MonoBehaviour {
    public float Lifetime = 0.7f;
    private GameObject player;
    // Use this for initialization
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update() {
        Lifetime -= Time.deltaTime;
        if (Lifetime < 0){
            gameObject.SetActive(false);
        }

        transform.localRotation = player.transform.localRotation;
    }
}