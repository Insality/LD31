using UnityEngine;

public class Spinning: MonoBehaviour {
    private float _spinDir = 1;

    private void Start() {
        if (Random.Range(0, 2) == 0){
            _spinDir = -1;
        }
    }

    private void Update() {
        transform.Rotate(new Vector3(0, 0, 1), _spinDir);
    }
}