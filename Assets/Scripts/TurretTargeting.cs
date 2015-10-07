using UnityEngine;

public class TurretTargeting: MonoBehaviour {
    public int FarAngle;
    public int RotationSpeed;

    private TurretLogic _logic;
    private Animator anim;
    private Transform target;
    public GameObject targetObject;


    private void Start() {
        anim = GetComponent<Animator>();
        target = targetObject.transform;
        _logic = GetComponent<TurretLogic>();
    }

    private void Update() {
        if (_logic.UpgradeTime == 0){
            LookAtPlayer(_logic.DeltaTargeting);
        }
    }

    public void LookAtPlayer(int deltaAngle) {
        float farCoef = 1f;

        Quaternion targetRot = target.localRotation;
        targetRot *= Quaternion.Euler(0, 0, deltaAngle);

        float angleDelta = Quaternion.Angle(transform.localRotation, targetRot);
        if (angleDelta > FarAngle){
            farCoef = 3f;
        }

        farCoef *= _logic.SpeedTargetCoef;

        Quaternion rot = Quaternion.RotateTowards(transform.localRotation, targetRot,
            RotationSpeed*Time.deltaTime*farCoef);
        rot.x = 0;
        rot.y = 0;
        transform.rotation = rot;
    }
}