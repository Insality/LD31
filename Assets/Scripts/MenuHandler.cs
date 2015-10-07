using UnityEngine;

public class MenuHandler: MonoBehaviour {
    public GameObject GameHint;
    // Use this for initialization
    private void Start() {
        GameHint.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space)){
            StartGame();
        }

        if (Input.GetKeyUp(KeyCode.Escape)){
            ExitGame();
        }

        if (Input.GetKeyDown(KeyCode.H)){
            ShowHint();
        }

        if (Input.GetKeyUp(KeyCode.H)){
            HideHint();
        }
    }

    private void ShowHint() {
        GameHint.SetActive(true);
    }

    private void HideHint() {
        GameHint.SetActive(false);
    }

    public void StartGame() {
        Application.LoadLevel("Main");
    }

    public void ExitGame() {
        Application.Quit();
    }
}