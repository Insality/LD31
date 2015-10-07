using UnityEngine;

public class WallAppear: MonoBehaviour {
    public float AppearTime;
    private float _curAppearTime;
    private Vector3 _goalPos;
    public bool IsAppeared;

    public GameObject Marker;
    private ParticleSystem _psystem;
    private Vector3 _startPos;

    private void Start() {
        IsAppeared = false;
        _curAppearTime = AppearTime;
        _startPos = transform.position;
        _goalPos = _startPos + (new Vector3(0, 0) - _startPos)/4;
    }

    // Update is called once per frame
    private void Update() {
        _curAppearTime += Time.deltaTime;
        if (_curAppearTime > AppearTime)
            _curAppearTime = AppearTime;


        if (IsAppeared){
            Vector3 pos = Vector3.Lerp(_startPos, _goalPos, _curAppearTime/AppearTime);
            transform.position = pos;
        }
        else{
            Vector3 pos = Vector3.Lerp(_goalPos, _startPos, _curAppearTime/AppearTime);
            transform.position = pos;
        }
    }

    public void Toogle() {
        _curAppearTime = -0.5f;
        IsAppeared = !IsAppeared;
        if (IsAppeared){
            SpawnMarker();
        }
    }

    private void SpawnMarker() {
        Instantiate(Marker, _goalPos/3*2, transform.localRotation);
    }

    public void ToogleOn() {
        _curAppearTime = -0.5f;
        IsAppeared = true;
        SpawnMarker();
    }

    public void ToogleOff() {
        _curAppearTime = -0.5f;
        IsAppeared = false;
    }
}