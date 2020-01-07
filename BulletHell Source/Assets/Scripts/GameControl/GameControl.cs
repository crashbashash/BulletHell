using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Range(30, 120)]
    public int fps;

    public GameObject player;

    private int curLevel = 0;

    private bool p1Dead = false;
    private int p1Damage = 1;
    private int p1Speed = 1;
    private int p1MultiShot = 1;
    private int p1MultiTarget = 1;
    private bool p2Dead = false;

    private const float deathTimer = 3f;
    private float p1DeathTimer = deathTimer;
    private float p2DeathTimer = deathTimer;
    private int p1Lives = 3;

    private GUIStyle blackStyle;
    private GUIStyle deathStyle;
    private FPSCounter fpsCounter;
    private Transform mainCam;
    private Transform playerSpawn;
    private EnemySpawner enemySpawner;
    private ScoreControl scoreControl;
    private float highScore = 0;

    public bool P1Dead { get => p1Dead; set => p1Dead = value; }
    public int P1Damage { get => p1Damage; set => p1Damage = value; }
    public int P1Speed { get => p1Speed; set => p1Speed = value; }
    public int P1MultiShot { get => p1MultiShot; set => p1MultiShot = value; }
    public int P1MultiTarget { get => p1MultiTarget; set => p1MultiTarget = value; }
    public bool P2Dead { get => p2Dead; set => p2Dead = value; }
    public int P1Lives { get => p1Lives; set => p1Lives = value; }
    public float HighScore { get => highScore; }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        blackStyle = new GUIStyle();
        blackStyle.normal.textColor = Color.black;

        deathStyle = new GUIStyle();
        deathStyle.normal.textColor = Color.red;
        deathStyle.fontSize = 15;

        fpsCounter = GetComponent<FPSCounter>();
        DontDestroyOnLoad(this.gameObject);
        if(GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region player spawn shit
        if (playerSpawn != null && player != null)
        {
            if (p1Dead) { if (p1DeathTimer <= 0) { SpawnPlayer(); } else { p1DeathTimer -= Time.deltaTime; } } //Shit for player getting dead
        }
        #endregion
        if(curLevel == 1 && p1Lives <= 0) //Game over man, game over
        {
            if (scoreControl.score > highScore)
                highScore = scoreControl.score;
            SceneManager.LoadScene(0);
        }
    }

    private void SpawnPlayer()
    {
        GameObject playerObj = Instantiate(player, playerSpawn.position, player.transform.rotation);
        playerObj.transform.parent = mainCam;
        playerObj.name = "_Player1";
        p1Dead = false;
        p1DeathTimer = deathTimer;
    }

    private void LateUpdate()
    {
        if(Application.targetFrameRate != fps)
            Application.targetFrameRate = fps;
    }

    private void OnLevelWasLoaded(int level)
    {
        curLevel = level;
        Debug.Log(level.ToString());
        if (level == 1)
        {
            P1Lives = 3;
            mainCam = GameObject.Find("Main Camera").transform;
            playerSpawn = mainCam.transform.Find("Player Spawn");
            scoreControl = GameObject.Find("Score Control").GetComponent<ScoreControl>();
            SpawnPlayer();
        }
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 500), "FPS: " + fpsCounter.CurFPS.ToString(), blackStyle);

        if (curLevel == 1) { GUI.Label(new Rect(10, 10, 100, 50), "Lives: " + p1Lives, blackStyle); }

        if (p1Dead && curLevel == 1) { GUI.Label(new Rect((Screen.width - 100) - 50, 10, 100, 50), "P1 Dead: " + p1DeathTimer.ToString("F2"), deathStyle); }
    }
}
