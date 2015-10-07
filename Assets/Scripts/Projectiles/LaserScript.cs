using UnityEngine;

public class LaserScript: MonoBehaviour {
    private GameObject _turret;
    private bool _isColliderAcitve;
    private float _lifeTime = 2.5f;

    private void Start() {
        _turret = GameObject.FindWithTag("Turret");
        if (_turret == null){
            Debug.LogError("Error in fingind Turret!");
        }
    }

    private void Update() {
        transform.localRotation = _turret.transform.localRotation;

        if (!_isColliderAcitve && _lifeTime < 1.65f){
            GetComponent<BoxCollider2D>().enabled = true;
            _isColliderAcitve = true;
        }

        UpdateLifeTime();
    }

    private void UpdateLifeTime() {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0){
            gameObject.SetActive(false);
        }
    }
}