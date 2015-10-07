using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour {
    public static float BestTime;
    public static int DeathCounter;
    public static float TimePlayed;
    public int CurLevel;
    public Text FinalText;
    public Text FinalHint;
    public AudioClip[] LevelAnounce;
    public AudioClip LevelFailed;
    public AudioClip GameOverClip;
    public AudioClip AgainClip;
    public Text LevelText;

    public float LevelTime;
    public AudioClip LevelWin;
    public Text TimeText;

    public string[] GameOverPhrases;

    public GameObject Turret;
    public GameObject[] Walls;
    private bool isGameEnd;
    private bool isWin;
    private int nextLevelTime;
    private float restTime;
    private float wallSwapTime;

    public GameObject Hint;
    private static bool ShowOnce = false;

    private void Start() {
        LevelTime = 0;
        BestTime = PlayerPrefs.GetFloat("besttime", 0);
        DeathCounter = PlayerPrefs.GetInt("death", 0);
        TimePlayed = PlayerPrefs.GetFloat("timeplayed", 0);

        CurLevel = 0;
        restTime = 0;
        wallSwapTime = 4f;
        nextLevelTime = 20;
        Time.timeScale = 1;

        FinalText.enabled = false;
        FinalHint.enabled = false;
        FinalText.GetComponentInParent<Image>().enabled = false;

        AudioSource.PlayClipAtPoint(LevelAnounce[CurLevel], transform.position);

        GetComponent<AudioSource>().Play();
        ShowHint();
    }

    private void ShowHint() {
        if (!ShowOnce){
            ShowOnce = true;
            Hint.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void HideHint() {
        Hint.SetActive(false);
    }

    void OnDestroy() {
        PlayerPrefs.SetFloat("besttime", BestTime);
        PlayerPrefs.SetInt("death", DeathCounter);
        PlayerPrefs.SetFloat("timeplayed", TimePlayed);
    }

    private void Update() {
        LevelTime += Time.deltaTime;
        TimePlayed += Time.deltaTime;

        if (LevelTime >= 120){
            if (!isWin){
                isWin = true;
                Turret.GetComponent<TurretLogic>().Destroy();
                StopGame();
            }
        }

        wallSwapTime -= Time.deltaTime;
        if (wallSwapTime <= 0)
            wallSwapTime = 0;

        if (nextLevelTime < LevelTime){
            UpgradeLevel();
        }

        if ((CurLevel == 2 || CurLevel == 3) && wallSwapTime == 0){
            wallSwapTime = 3f;

            if (Random.Range(0, 2) == 0){
                Walls[0].GetComponent<WallAppear>().Toogle();
                Walls[2].GetComponent<WallAppear>().Toogle();
            }
            else{
                Walls[1].GetComponent<WallAppear>().Toogle();
                Walls[3].GetComponent<WallAppear>().Toogle();
            }
        }

        TimeText.text = string.Format("TIME: {0:F2}", LevelTime);
        LevelText.text = "LEVEL " + CurLevel;

        UpdateControl();
    }

    private void UpdateControl() {

        if (ShowOnce){
            if (Input.anyKey){
                HideHint();
            }
        }
         
        if (Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 1;
            Application.LoadLevel("MenuLD31");
        }

        if (isGameEnd){
            if (Input.GetKeyDown(KeyCode.R)){
                AudioSource.PlayClipAtPoint(AgainClip, transform.position);
                Time.timeScale = 1;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void UpgradeLevel() {
        CurLevel++;
        if (CurLevel <= 5){
            AudioSource.PlayClipAtPoint(LevelAnounce[CurLevel], transform.position);
        }
        Turret.GetComponent<TurretLogic>().UpgradeLevel();

        if (CurLevel == 1){
            foreach (GameObject wall in Walls){
                wall.GetComponent<WallAppear>().Toogle();
            }
        }

        if (CurLevel == 2){
            foreach (GameObject wall in Walls){
                wall.GetComponent<WallAppear>().Toogle();
            }
        }
        if (CurLevel == 4){
            foreach (GameObject wall in Walls){
                wall.GetComponent<WallAppear>().ToogleOn();
            }
        }
        if (CurLevel == 5){
            foreach (GameObject wall in Walls){
                wall.GetComponent<WallAppear>().ToogleOff();
            }
        }

        nextLevelTime += 20;
    }

    private void StopGame() {
        AudioSource.PlayClipAtPoint(LevelWin, transform.position);
        GetComponent<AudioSource>().Stop();

        BestTime = 120f;

        isGameEnd = true;
        isWin = true;
        Time.timeScale = 0;
        TimeText.enabled = false;
        LevelText.enabled = false;
        FinalText.enabled = true;
        FinalText.GetComponentInParent<Image>().enabled = true;
        FinalHint.enabled = true;



        FinalText.text = string.Format("CONGRATULATIONS!!!\nYOU BEAT THE GAME!\nDIED TOTAL {0} TIMES\nTIME TOTAL: {1} min\n\nESC TO EXIT\nR TO RESTART", DeathCounter, (int)(TimePlayed/60));
        FinalHint.text = "Type 'Cardinalo Defetus' in comments on Ludum Dare to show you amazing skills!";

    }

    public void Looser() {
        AudioSource.PlayClipAtPoint(LevelFailed, transform.position, 0.7f);
        AudioSource.PlayClipAtPoint(GameOverClip, transform.position);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanTp = false;

        DeathCounter++;
        GetComponent<AudioSource>().Stop();

        if (BestTime < LevelTime){
            BestTime = LevelTime;
        }

        isGameEnd = true;
        Time.timeScale = 0;
        TimeText.enabled = false;
        LevelText.enabled = false;
        FinalText.enabled = true;
        FinalText.GetComponentInParent<Image>().enabled = true;

        FinalText.text = (string.Format("TIME: {0:F2}\nBEST: {1:f2}\n\n\nESC TO EXIT\nR TO RESTART", LevelTime, BestTime));

        FinalHint.enabled = true;
        FinalHint.text = GameOverPhrases[Random.Range(0, GameOverPhrases.Length)];
    }
}