using UnityEngine;

public class PlayerLogic: MonoBehaviour {
    private void Start() {
    }

    private void Update() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){
            FindObjectOfType<LevelManager>().Looser();
        }
    }
}