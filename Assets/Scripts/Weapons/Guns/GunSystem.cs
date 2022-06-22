using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EZCameraShake;

public class GunSystem : MonoBehaviour
{
    [Header("Main Variables")]
    public int damage;
    public float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    public int magazineSize, ammoCapacity, bulletsPerTap;
    public bool allowButtonHold;

    //Recoil Variables
    //public Vector2 upRecoil;
    //private Vector2 shootingPointRotation;
    //private Vector2 cameraRotation;
   
    //Private Variables
    int bulletsLeft, bulletsShot, ammoLeft, ammoLeftTemp;
    bool shooting, readyToShoot, reloading;

    [Header("References")]
    public Camera cam;
    public GameObject shootingPoint;
    public RaycastHit rayHit;
    //public LayerMask whatIsEnemy;

    //Graphics
    public GameObject bulletImpactGraphic;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    public TextMeshProUGUI text;


    [Header("Cam Shake Variables")] 
    public float camShakeMagnitude;
    public float camShakeRougness;
    public float camShakeFadeInTime;
    public float camShakeFadeOutTime;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        ammoLeft = ammoCapacity;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        

        //setText
        text.SetText(bulletsLeft + "/" + ammoLeft);
    }

    private void MyInput() 
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && ammoLeft > 0) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        //float x = Random.Range(-spread, spread);
        //float y = Random.Range(-spread, spread);

        //Recoil
        //shootingPoint.transform.localEulerAngles 

        //Calculate Direction with Spread
        Vector3 direction = shootingPoint.transform.forward; //+ new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(shootingPoint.transform.position, direction, out rayHit, range)) //falta: , whatIsEnemy
        {

            //if (rayHit.collider.CompareTag("Enemy")) rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
        }

        //Animation
        animator.SetTrigger("Shot");

        //Particles
        if (rayHit.point != new Vector3 (0, 0, 0)) Instantiate(bulletImpactGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        muzzleFlash.Play();

        //ShakeCamera
        CameraShaker.GetInstance("Main Camera").ShakeOnce(camShakeMagnitude, camShakeRougness, camShakeFadeInTime, camShakeFadeOutTime);


        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        ammoLeftTemp = ammoLeft;
        ammoLeftTemp -= (magazineSize - bulletsLeft);
        if (ammoLeftTemp < 0) ammoLeftTemp = 0;

        if (magazineSize <= ammoLeft || bulletsLeft + ammoLeft >= magazineSize) bulletsLeft = magazineSize;
        else bulletsLeft += ammoLeft;

        ammoLeft = ammoLeftTemp;

        reloading = false;
    }

    public void AddAmmo()
    {
        if (ammoLeft + magazineSize < ammoCapacity) ammoLeft += magazineSize;
        else ammoLeft = ammoCapacity;
    }

    public bool Maxbullets()
    {
        if (ammoLeft == ammoCapacity) return true;
        else return false;
    }
}
