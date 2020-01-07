using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 20)]
    public float xSpeed;
    [Range(0, 20)]
    public float ySpeed;

    private bool isInvincible = false;
    private float invincibilityTime = 3f;
    private float invincibilityTimer = 0f;

    private GUIStyle blackStyle;

    public GameObject deathParticles;
    public GameObject sfx;
    public AudioClip explosionSFX;
    public Color primaryColor;

    private PlayerInput playerInput;
    private GunController gunController;
    private GlobalVars globalVars;
    private GameObject bullet;
    private Transform mainCam;

    public GameObject BulletGO { set => bullet = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gunController = GetComponent<GunController>();
        globalVars = GameObject.Find("Game Control").GetComponent<GlobalVars>();
        mainCam = GameObject.Find("Main Camera").transform;
        blackStyle = new GUIStyle();
        blackStyle.normal.textColor = Color.black;

        isInvincible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.Paused == false)
        {
            if (playerInput.Shoot)
            {
                gunController.Shoot();
            }
            if (isInvincible)
            {
                invincibilityTimer += Time.deltaTime;
                if (invincibilityTimer >= invincibilityTime) { invincibilityTimer = 0; isInvincible = false; } //Stop invincibility
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerInput.Paused == false)
        {
            Vector2 moveBy = new Vector2((playerInput.XAxis * xSpeed) * Time.deltaTime, (playerInput.YAxis * ySpeed) * Time.deltaTime);
            transform.Translate(moveBy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "CockPit")
        {
            Transform enemy = collision.transform.parent.parent;
            if (enemy.GetComponent<EnemyAI>().aiName == "S.Spirit" && enemy.GetComponent<EnemyAI>().ID == 0)
            {
                DestroyPlayer();
                DestroyEnemy(enemy);
            }
        }
    }

    public void DestroyPlayer()
    {
        if (!isInvincible)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            deathParticles.GetComponent<ParticleSystem>().startColor = primaryColor;
#pragma warning restore CS0618 // Type or member is obsolete

            GameObject deathObj = Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);
            GameObject sfxObj = Instantiate(sfx, transform.position, Quaternion.identity);
            AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
            sfxObj.transform.parent = mainCam;
            deathObj.transform.parent = mainCam;
            sfxSource.clip = explosionSFX;
            sfxSource.Play();
            Destroy(sfxObj, 3f);
            Destroy(deathObj, 5f);
            Destroy(gameObject);
            GameObject.Find("Game Control").GetComponent<GameControl>().P1Dead = true;
            GameObject.Find("Game Control").GetComponent<GameControl>().P1Lives--;
        }
    }

    private void DestroyEnemy(Transform enemy)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        deathParticles.GetComponent<ParticleSystem>().startColor = enemy.GetComponent<EnemyAI>().primaryColor;
#pragma warning restore CS0618 // Type or member is obsolete

        GameObject deathObj = Instantiate(deathParticles, enemy.position, deathParticles.transform.rotation);
        GameObject sfxObj = Instantiate(sfx, enemy.position, Quaternion.identity);
        AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
        sfxObj.transform.parent = mainCam;
        deathObj.transform.parent = mainCam;
        sfxSource.clip = explosionSFX;
        sfxSource.Play();
        Destroy(sfxObj, 3f);
        Destroy(enemy.gameObject);
        Destroy(deathObj, 5f);
    }

    private void OnGUI()
    {
        if (invincibilityTimer > 0)
        {
            GUI.Label(new Rect((Screen.width - 100) - 50, 10, 100, 50), "P1 Invincible: " + (invincibilityTime - invincibilityTimer).ToString("F2"), blackStyle);
        }
    }
}
