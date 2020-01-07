using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject sfx;
    public AudioClip shootSFX;
    public float fireRate;
    public float bulletSpeed;
    public bool player;
    public GameObject bullet;
    public Transform[] bulletSpawn;

    private float fireTimer = 0f;
    private Transform mainCam;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").transform;
    }
    
    private void Update()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if(fireTimer <= 0)
        {
            if (bullet != null && sfx != null && shootSFX != null)
            {
                for (int i = 0; i < bulletSpawn.Length; i++)
                {
                    Vector3 spawnPos = new Vector3(bulletSpawn[i].position.x, bulletSpawn[i].position.y, 0);
                    GameObject firedBullet = Instantiate(bullet, spawnPos, bullet.transform.rotation);
                    GameObject shootSFXObj = Instantiate(sfx, spawnPos, Quaternion.identity);
                    firedBullet.transform.parent = mainCam;
                    shootSFXObj.transform.parent = mainCam;
                    firedBullet.GetComponent<BulletController>().Player = player;
                    firedBullet.GetComponent<BulletController>().Speed = bulletSpeed;
                    AudioSource sfxSource = shootSFXObj.GetComponent<AudioSource>();
                    sfxSource.clip = shootSFX;
                    sfxSource.Play();
                    Destroy(shootSFXObj, 3f);

                    if (player && this.name == "_Player1")
                    {
                        firedBullet.GetComponent<BulletController>().Damage = GameObject.Find("Game Control").GetComponent<GameControl>().P1Damage;
                    }
                }
            }
            else
                Debug.LogError("No bullet object selected");
            fireTimer = fireRate;
        }
    }
}
