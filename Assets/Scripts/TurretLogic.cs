 using UnityEngine;

public class TurretLogic: MonoBehaviour {
    public GameObject Cannon;

    private TurretFiring _cannonFiring;
    public int DeltaTargeting = 0;
    public float SpeedTargetCoef = 1;

    private const float SwitchTacticCooldown = 5f;
    private int _turretLevel;
    public Sprite[] UpgradeCannonImages;
    public Sprite[] UpgradeTurretImages;

    private float _lastSplitAngle = 40;
    private PlayerMovement _playerMovement;
    private int _curTactic;
    private float _curTacticCooldown;
    public AudioClip Explosive;
    private ParticleSystem _particles;

    private CameraShake _shaker;
    public float UpgradeTime;

    private void Start() {
        _cannonFiring = Cannon.GetComponent<TurretFiring>();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (_playerMovement == null){
            Debug.LogError("Player is not founded");
        }

        _shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

        _curTacticCooldown = SwitchTacticCooldown;
        _particles = GetComponent<ParticleSystem>();
        UpgradeTime = 3f;
        _curTactic = Random.Range(0, 3);
    }

    private void Update() {
        UpdateTactic();

        if (UpgradeTime > 0){
            _particles.Emit(1000);
        }
    }

    private void UpdateTactic() {
        _curTacticCooldown -= Time.deltaTime;
        if (_curTacticCooldown <= 0){
            _curTacticCooldown = SwitchTacticCooldown;
            _curTactic += Random.Range(1, 3);
            _curTactic %= 3;
            Debug.Log("Choose tactic " + _curTactic);
        }

        UpgradeTime -= Time.deltaTime;
        if (UpgradeTime <= 0)
            UpgradeTime = 0;

        if (UpgradeTime == 0){
            if (_turretLevel == 0){
                if (_curTactic == 0)
                    Level0FireSniper();
                if (_curTactic == 1)
                    Level0FireForward();
                if (_curTactic == 2)
                    Level0FireSplit();
            }

            if (_turretLevel == 1){
                if (_curTactic == 0)
                    Level1Laser();
                if (_curTactic == 1)
                    Level1FireSplit();
                if (_curTactic == 2)
                    Level1Triple();
            }

            if (_turretLevel == 2){
                if (_curTactic == 0)
                    Level2Chaotic();
                if (_curTactic == 1)
                    Level2Circle();
                if (_curTactic == 2)
                    Level2Wave();
            }

            if (_turretLevel == 3){
                if (_curTactic == 0)
                    Level3DoubleCircle();
                if (_curTactic == 1)
                    Level3SplitCircle();
                if (_curTactic == 2)
                    Level3WaveBack();
            }

            if (_turretLevel == 4){
                if (_curTactic == 0)
                    Level4LaserFire();
                if (_curTactic == 1)
                    Level4RandomSector();
                if (_curTactic == 2)
                    Level4SniperRush();
            }

            if (_turretLevel == 5){
                if (_curTactic == 0)
                    Level5ChaoticOmega();
                if (_curTactic == 1)
                    Level5QuadCircle();
                if (_curTactic == 2)
                    Level5WaveOmega();
            }
        }
    }

    private void Level0FireSplit() {
        DeltaTargeting = 0;
        SpeedTargetCoef = 1f;
        _lastSplitAngle -= Time.deltaTime*30;
        if (_lastSplitAngle < 5){
            _lastSplitAngle = 90;
        }
        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire((int) _lastSplitAngle);
            _cannonFiring.Fire(-(int) _lastSplitAngle);
        }
    }

    private void Level0FireForward() {
        DeltaTargeting = 0;
        SpeedTargetCoef = 1f;
        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire(0, 6.5f);
        }
    }

    private void Level0FireSniper() {
        DeltaTargeting = _playerMovement.Direction*70;
        SpeedTargetCoef = 1f;
        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire(0);
        }
    }

    private void Level1Laser() {
        DeltaTargeting = 0;
        SpeedTargetCoef = 0.35f;
        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Laser();
        }
    }

    private void Level1FireSplit() {
        _lastSplitAngle -= Time.deltaTime*45;
        if (_lastSplitAngle < 2){
            _lastSplitAngle = 60;
        }
        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting = 0;
            SpeedTargetCoef = 1.3f;
            _cannonFiring.Fire((int) _lastSplitAngle);
            _cannonFiring.Fire(-(int) _lastSplitAngle);
        }
    }

    private void Level1Triple() {
        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting = 0;
            SpeedTargetCoef = 1f;
            _cannonFiring.Fire(0);
            _cannonFiring.Fire(40);
            _cannonFiring.Fire(-40);
        }
    }

    private void Level2Chaotic() {
        if (_cannonFiring.CurCooldown == 0){
            SpeedTargetCoef = 1.4f;
            DeltaTargeting += Random.Range(-20, 80);
            _cannonFiring.Fire(0);
            _cannonFiring.CurCooldown = 0.03f;
        }
    }

    private void Level2Circle() {
        if (_cannonFiring.CurCooldown == 0){
            SpeedTargetCoef = 1.7f;
            DeltaTargeting += 20;
            _cannonFiring.Fire(-20);
            _cannonFiring.Fire(20);
        }
    }

    private void Level2Wave() {
        DeltaTargeting = 0;

        if (_cannonFiring.CurCooldown == 0){
            SpeedTargetCoef = 1f;
            _cannonFiring.Fire(-100);
            _cannonFiring.Fire(-70);
            _cannonFiring.Fire(-40);
            _cannonFiring.Fire(-20);
            _cannonFiring.Fire(0);
            _cannonFiring.Fire(20);
            _cannonFiring.Fire(40);
            _cannonFiring.Fire(70);
            _cannonFiring.Fire(100);
            _cannonFiring.CurCooldown = 0.5f;
        }
    }

    private void Level3SplitCircle() {
        SpeedTargetCoef = 1.5f;

        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting += 20;
            _cannonFiring.Fire(-30);
            _cannonFiring.Fire(30);
        }
    }

    private void Level3WaveBack() {
        SpeedTargetCoef = 1.2f;
        DeltaTargeting = 0;

        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire(-100);
            _cannonFiring.Fire(-70);
            _cannonFiring.Fire(-40);
            _cannonFiring.Fire(-20);
            _cannonFiring.Fire(0);
            _cannonFiring.Fire(20);
            _cannonFiring.Fire(40);
            _cannonFiring.Fire(70);
            _cannonFiring.Fire(100);

            // back
            _cannonFiring.Fire(-180);
            _cannonFiring.Fire(-160);
            _cannonFiring.Fire(-200);

            _cannonFiring.CurCooldown = 0.5f;
        }
    }

    private void Level3DoubleCircle() {
        SpeedTargetCoef = 1.5f;

        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting += 20;
            _cannonFiring.Fire(-180);
            _cannonFiring.Fire(0);
        }
    }

    private void Level4LaserFire() {
        SpeedTargetCoef = 0.4f;
        DeltaTargeting += 3;
        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire(-180);
            _cannonFiring.Fire(-190);
            _cannonFiring.Fire(-200);
            _cannonFiring.Fire(-170);
            _cannonFiring.Fire(-160);
            _cannonFiring.Laser();
        }
    }

    private void Level4SniperRush() {
        if (_cannonFiring.CurCooldown == 0){

            DeltaTargeting = _playerMovement.Direction * 70;
            SpeedTargetCoef = 1f;
            _cannonFiring.Fire(0);
            _cannonFiring.CurCooldown = 0.03f;
        }
    }

    private void Level4RandomSector() {
        if (_cannonFiring.CurCooldown == 0){
            SpeedTargetCoef = 1.5f;
            _cannonFiring.Fire(0);
            _cannonFiring.CurCooldown = 0.02f;
            DeltaTargeting = Random.Range(-35, 35);
        }
    }

    private void Level5QuadCircle() {
        SpeedTargetCoef = 1.3f;


        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting += 25;
            _cannonFiring.Fire(-180);
            _cannonFiring.Fire(90);
            _cannonFiring.Fire(-90);
            _cannonFiring.Fire(0);

            _cannonFiring.CurCooldown = 0.1f;
        }
    }

    private void Level5ChaoticOmega() {
        SpeedTargetCoef = 2f;
        if (_cannonFiring.CurCooldown == 0){
            DeltaTargeting += Random.Range(-90, 90);
            _cannonFiring.Fire(0);
            _cannonFiring.Fire(90);
            _cannonFiring.Fire(-90);

            _cannonFiring.CurCooldown = 0.06f;
        }
    }

    private void Level5WaveOmega() {
        SpeedTargetCoef = 1.8f;
        DeltaTargeting = Random.Range(-20, 20);

        if (_cannonFiring.CurCooldown == 0){
            _cannonFiring.Fire(0);
            _cannonFiring.Fire(10);
            _cannonFiring.Fire(20);
            _cannonFiring.Fire(30);
            _cannonFiring.Fire(40);
            _cannonFiring.Fire(50);
            _cannonFiring.Fire(70);
            _cannonFiring.Fire(90);
            _cannonFiring.Fire(110);
            _cannonFiring.Fire(-10);
            _cannonFiring.Fire(-20);
            _cannonFiring.Fire(-30);
            _cannonFiring.Fire(-40);
            _cannonFiring.Fire(-50);
            _cannonFiring.Fire(-70);
            _cannonFiring.Fire(-90);
            _cannonFiring.Fire(-110);

            _cannonFiring.CurCooldown = 0.5f;
        }
    }


    public void UpgradeLevel() {
        _shaker.ShakeTime = 1.5f;
        _shaker.ShakeDist = 0.05f;
        _turretLevel++;
        UpgradeTime += 2f;
        if (_turretLevel <= 5){
            GetComponent<SpriteRenderer>().sprite = UpgradeTurretImages[_turretLevel];

            Cannon.GetComponent<SpriteRenderer>().sprite = UpgradeCannonImages[_turretLevel];
        }
    }

    public void Destroy() {
        UpgradeTime = 30f;
        AudioSource.PlayClipAtPoint(Explosive, transform.position);
    }
}