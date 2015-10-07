using UnityEngine;

public class TurretFiring: MonoBehaviour {
    public float CurCooldown = 0;
    public GameObject LaserBeam;
    public GameObject PhaseBullet;
    public GameObject _turret;
    private Animator anim;

    private CameraShake cameraShake;
    public float cooldownLaser;
    public float cooldownPhaseBullet;

    private void Start() {
        anim = GetComponentInParent<Animator>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    private void Update() {
        CurCooldown -= Time.deltaTime;
        if (CurCooldown < 0){
            CurCooldown = 0;
        }
    }

    public void Fire(int deltaAngle, float speed=0) {
        cameraShake.ShakeTime = 0.4f;
        cameraShake.ShakeDist = 0.01f;
        Quaternion rot = _turret.transform.localRotation;

        int isBack = 1;
        if (-120 > deltaAngle && deltaAngle > -240){
            isBack = -1;
        }

        deltaAngle += Random.Range(-5, 5);
        Object bullet;
        if (isBack == 1){
            bullet = Instantiate(PhaseBullet, transform.position - transform.up*0.9f, rot);
        }
        else{
            bullet = Instantiate(PhaseBullet, transform.position - transform.up * -0.4f, rot);
        }

        PhaseBullet bulletScript = (bullet as GameObject).GetComponent<PhaseBullet>();
        if (speed != 0){
            bulletScript.Speed = speed;
        }

        bulletScript.DeltaAngle = deltaAngle;

        anim.SetTrigger("Fire");
        CurCooldown = cooldownPhaseBullet;
    }

    public void Laser() {
        Vector3 pos = transform.position;
        pos.z = -0.5f;
        Object laser = Instantiate(LaserBeam, pos, _turret.transform.localRotation);
        anim.SetTrigger("Laser");
        CurCooldown = cooldownLaser;
    }
}