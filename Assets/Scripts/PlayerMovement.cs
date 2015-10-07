using UnityEngine;

public class PlayerMovement: MonoBehaviour {
    public bool CanTp = true;
    public int Direction;
    public int Speed;
    public float TeleportCooldown;

    private float _teleportCurCooldown;

    private bool isSoundPlayed;

    private SpriteRenderer sreneder;
    public AudioClip teleportCD;

    private void Start() {
        Direction = 0;
        sreneder = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        UpdateContol();
    }

    private void UpdateContol() {
        GetComponent<Animator>().SetBool("isMove", false);
        Direction = 0;

        _teleportCurCooldown -= Time.deltaTime;
        if (_teleportCurCooldown < 0){
            if (!isSoundPlayed){
                isSoundPlayed = true;
                sreneder.color = Color.white;
                AudioSource.PlayClipAtPoint(teleportCD, transform.position, 0.8f);
            }
            _teleportCurCooldown = 0;
        }

        bool isLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool isRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (isLeft && !isRight){
            Move(true);
            Direction = -1;
        }

        if (isRight && !isLeft){
            Move(false);
            Direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
            if (_teleportCurCooldown == 0)
                Teleport();
        }
    }

    private void Teleport() {
        if (CanTp){
            isSoundPlayed = false;
            sreneder.color = Color.grey;
            _teleportCurCooldown = TeleportCooldown;
            GetComponent<AudioSource>().Play();
            transform.RotateAround(new Vector3(0, 0), new Vector3(0, 0, 1), 180);
        }
    }

    private void Move(bool isLeft) {
        GetComponent<Animator>().SetBool("isMove", true);
        float speed = Speed*Time.deltaTime;
        if (isLeft){
            speed *= -1;
        }
        transform.RotateAround(new Vector3(0, 0), new Vector3(0, 0, 1), speed);
    }

    private void LookAtCenter() {
        Quaternion rot = Quaternion.FromToRotation(transform.position, new Vector3(0, 0, 0));
        transform.localRotation = rot;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Wall"){
            FindObjectOfType<LevelManager>().Looser();
        }
    }
}