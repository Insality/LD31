using UnityEngine;

public class PhaseBullet: MonoBehaviour {
    public int DeltaAngle = 0;
    public float Speed;

    private float lifeTime = 2f;

    private void Start() {
    }

    private void Update() {
        float angle = transform.localEulerAngles.z - 90 + DeltaAngle;
        angle *= Mathf.Deg2Rad;
        var dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        transform.position += dir*(Speed*Time.deltaTime);

        UpdateLifeTime();
    }

    private void UpdateLifeTime() {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0){
            gameObject.SetActive(false);
        }
    }
}