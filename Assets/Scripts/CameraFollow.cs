using UnityEngine;

public class CameraFollow: MonoBehaviour {
    public GameObject target;

    private void Start() {
    }

    private void LateUpdate() {
        //	    transform.localRotation = target.transform.localRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target.transform.localRotation, 0.2f);
        
    }
}