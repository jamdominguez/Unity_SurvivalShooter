using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour {
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public Slider ammoSlider;
    public int maxBullets = 100;
    public Image enemyImg;
    public Slider enemyHealthSlider;
    public Light pointLight;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    int currentBullets;


    void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        currentBullets = maxBullets;
        StartCoroutine(ReloadAmmo());
        enemyImg.enabled = false;
        enemyHealthSlider.gameObject.SetActive(false);
    }

    IEnumerator ReloadAmmo() {
        float reloadTime = 2 * timeBetweenBullets;
        while(true) {
            yield return new WaitForSeconds(reloadTime);
            if(currentBullets < maxBullets) {
                currentBullets++;
                ammoSlider.value = currentBullets;
            }
        }
    }


    void Update() {
        timer += Time.deltaTime; // time from last shoot

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && !EventSystem.current.IsPointerOverGameObject(-1)) {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }
    }


    public void DisableEffects() {
        gunLine.enabled = false;
        gunLight.enabled = false;
        pointLight.enabled = false;
    }


    void Shoot() {
        if (currentBullets <= 0) return;

        currentBullets--;
        ammoSlider.value = currentBullets;

        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;
        pointLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null) {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                enemyImg.sprite = enemyHealth.icon;
                enemyHealthSlider.value = 100 * enemyHealth.currentHealth / enemyHealth.startingHealth; // health in % becasuse each enemy has differtent health
                EnemyUIVisibility(true);
                StopCoroutine("ScheduleHideEnemyUI");
                StartCoroutine("ScheduleHideEnemyUI");
            } else {
                EnemyUIVisibility(false);
            }
            gunLine.SetPosition(1, shootHit.point);
        } else {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    IEnumerator ScheduleHideEnemyUI() {
        yield return new WaitForSeconds(2f);
        EnemyUIVisibility(false);
    }

    void EnemyUIVisibility(bool show) {
        enemyImg.enabled = show;
        enemyHealthSlider.gameObject.SetActive(show);
    }
}
