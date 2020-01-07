using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private bool player = true;
    private int damage;
    private float speed;
    private Bounds camBounds;
    private Transform mainCam;
    private GameObject gameController;
    private ScoreControl scoreControl;
    private int curPen = 0;

    public GameObject deathParticles;
    public GameObject sfx;
    public AudioClip explosionSFX;
    public Color primaryColor;
    [Range(0, 10)]
    public int maxPen;

    public bool Player { set => player = value; }
    public float Speed { set => speed = value; }
    public int Damage { set => damage = value; }

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").transform;
        camBounds = mainCam.Find("Bounds").GetComponent<SpriteRenderer>().bounds;
        gameController = GameObject.Find("Game Control");
        scoreControl = GameObject.Find("Score Control").GetComponent<ScoreControl>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mainCamPos = mainCam.position;
        float lowX = mainCam.position.x - (camBounds.size.x / 2);
        float highX = mainCam.position.x + (camBounds.size.x / 2);
        float lowY = mainCam.position.y - (camBounds.size.y / 2);
        float highY = mainCam.position.y + (camBounds.size.y / 2);

        if (transform.position.x < lowX || transform.position.x > highX || transform.position.y < lowY || transform.position.y > highY)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (player)
            transform.Translate(new Vector2((1 * speed) * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2((-1 * speed) * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CockPit")
        {
            Transform mainParent = collision.transform.parent.parent;
            if (mainParent.GetComponent<EnemyAI>() && player)
            {
                EnemyController enemyController = mainParent.GetComponent<EnemyController>();
                if (enemyController.health - damage <= 0)
                {
                    Debug.Log("Destroy enemy: " + mainParent.name);
                    EnemyAI AI = mainParent.gameObject.GetComponent<EnemyAI>();
                    if (AI.aiName == "Striker") //Check for Striker
                    {
                        scoreControl.AddScore(1000);
                        DestroyObject(mainParent);
                    }
                    else if (AI.aiName == "Spreet") //Check for Spreet
                    {
                        scoreControl.AddScore(2500);
                        DestroyObject(mainParent);
                    }
                    else if (AI.aiName == "S.Spirit") //Check for S.Spirit
                    {
                        scoreControl.AddScore(5000);
                        DestroyObject(mainParent);
                    }
                }
                else { enemyController.health -= damage; }
                HitObject(mainParent);
            }
            else if(mainParent.GetComponent<PlayerController>() && !player)
            {
                PlayerController playerController = mainParent.gameObject.GetComponent<PlayerController>();
                Debug.Log("Hit player: " + mainParent.name);
                HitObject(mainParent);
                playerController.DestroyPlayer();
            }
        }
        else if(collision.tag == "Bullet" && player)
        {
            Debug.Log("Hit Bullet");
            HitObject(collision.transform);
            collision.GetComponent<BulletController>().DestroyBullet();
        }
    }

    private void DestroyObject(Transform toDestroy)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (toDestroy.GetComponent<EnemyAI>())
            deathParticles.GetComponent<ParticleSystem>().startColor = toDestroy.GetComponent<EnemyAI>().primaryColor;
        else
            deathParticles.GetComponent<ParticleSystem>().startColor = Color.cyan;
#pragma warning restore CS0618 // Type or member is obsolete
        
        GameObject deathObj = Instantiate(deathParticles, toDestroy.position, deathParticles.transform.rotation);
        GameObject sfxObj = Instantiate(sfx, toDestroy.position, Quaternion.identity);
        AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
        sfxObj.transform.parent = mainCam;
        deathObj.transform.parent = mainCam;
        sfxSource.clip = explosionSFX;
        sfxSource.Play();
        Destroy(sfxObj, 3f);
        Destroy(deathObj, 5f);
        Destroy(toDestroy.gameObject);
    }

    private void DestroyBullet()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        deathParticles.GetComponent<ParticleSystem>().startColor = primaryColor;
#pragma warning restore CS0618 // Type or member is obsolete

        GameObject deathObj = Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);
        deathObj.transform.parent = mainCam;
        Destroy(deathObj, 5f);
        Destroy(gameObject);
    }

    private void HitObject(Transform objectHit)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (objectHit.GetComponent<EnemyAI>())
            deathParticles.GetComponent<ParticleSystem>().startColor = objectHit.GetComponent<EnemyAI>().primaryColor;
        else
            deathParticles.GetComponent<ParticleSystem>().startColor = Color.cyan;
#pragma warning restore CS0618 // Type or member is obsolete

        GameObject deathObj = Instantiate(deathParticles, objectHit.position, deathParticles.transform.rotation);
        GameObject sfxObj = Instantiate(sfx, objectHit.position, Quaternion.identity);
        AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
        sfxObj.transform.parent = mainCam;
        deathObj.transform.parent = mainCam;
        sfxSource.clip = explosionSFX;
        sfxSource.Play();
        Destroy(sfxObj, 3f);
        Destroy(deathObj, 5f);
        curPen++;

        if(curPen >= maxPen)
        {
            DestroyBullet();
        }
    }
}
