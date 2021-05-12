using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tec9Gun : MonoBehaviour
{
    [Header("OPTIONS")]
    float fireRate1;
    public float fireRate2;
    public float range;
    int totalBullet = 200;
    int magazineCapacity = 30;
    int remainingBullet;
    float gunPower = 25;
    public TextMeshProUGUI totalBulletText;
    public TextMeshProUGUI remainingBulletText;

    [Header("SOUNDS")]
    public AudioSource[] Sounds; //fire, outOfAmmo, releod

    [Header("EFFECTS")]
    public ParticleSystem[] Effects; //fire, bulletMark, blood

    [Header("GENERAL NAMES")]
    public Camera myCamera;
    public Animator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        remainingBullet = magazineCapacity;
        totalBulletText.text = totalBullet.ToString();
        remainingBulletText.text = magazineCapacity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            reloadControl();
        }
        if (characterAnimator.GetBool("reload"))
        {
            reloadTechnic();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > fireRate1 && remainingBullet != 0)
            {
                FireGun();
                fireRate1 = Time.time + fireRate2;
            }
            if (remainingBullet == 0)
            {
                Sounds[1].Play();
            }

        }

    }

    void FireGun()
    {
        remainingBullet--;
        remainingBulletText.text = remainingBullet.ToString();
        Effects[0].Play();
        Sounds[0].Play();

        RaycastHit hit;
        if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, range))
        {
            Instantiate(Effects[1], hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    void reloadControl()
    {
        if (remainingBullet < magazineCapacity && totalBullet != 0)
        {
            characterAnimator.Play("reload_magazine");

            if (!Sounds[2].isPlaying)
                Sounds[2].Play();
        }
    }

    void reloadTechnic()
    {
        if (remainingBullet == 0)
        {
            if (totalBullet <= magazineCapacity)
            {
                remainingBullet = totalBullet;
                totalBullet = 0;
            }
            else
            {
                totalBullet -= magazineCapacity;
                remainingBullet = magazineCapacity;
            }
        }
        else
        {
            if (totalBullet <= magazineCapacity)
            {
                int resultValue = remainingBullet + totalBullet;
                if (resultValue > magazineCapacity)
                {
                    remainingBullet = magazineCapacity;
                    totalBullet = resultValue - magazineCapacity;
                }
                else
                {
                    remainingBullet += totalBullet;
                    totalBullet = 0;
                }
            }
            else
            {
                int existBullet = magazineCapacity - remainingBullet;
                totalBullet -= existBullet;
                remainingBullet = magazineCapacity;
            }
        }
        totalBulletText.text = totalBullet.ToString();
        remainingBulletText.text = magazineCapacity.ToString();

        characterAnimator.SetBool("reload", false);
    }

}
