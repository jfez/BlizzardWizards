using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGun : MonoBehaviour
{
    public GameObject pistol;
    public GameObject machineGun;
    public GameObject shotGun;
    public AudioSource change;

    private bool pistoleActive;
    private bool machineGunActive;
    private bool shotGunActive;

    private int indexGun;
    private int numberGuns = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pistol.SetActive(true);
        pistoleActive = true;

        machineGun.SetActive(false);
        machineGunActive = false;

        shotGun.SetActive(false);
        shotGunActive = false;

        indexGun = 1;

        //change = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //if tiene el arma disponible

        if (Input.GetKeyDown(KeyCode.Alpha1) && !pistoleActive)
        {
            pistol.SetActive(true);
            pistoleActive = true;

            machineGun.SetActive(false);
            machineGunActive = false;

            shotGun.SetActive(false);
            shotGunActive = false;

            indexGun = 1;

            change.Play();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && !machineGunActive)
        {
            pistol.SetActive(false);
            pistoleActive = false;

            machineGun.SetActive(true);
            machineGunActive = true;

            shotGun.SetActive(false);
            shotGunActive = false;

            indexGun = 2;

            change.Play();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && !shotGunActive)
        {
            pistol.SetActive(false);
            pistoleActive = false;

            machineGun.SetActive(false);
            machineGunActive = false;

            shotGun.SetActive(true);
            shotGunActive = true;

            indexGun = 3;

            change.Play();
        }

        if (Input.mouseScrollDelta.y > 0 && indexGun < numberGuns)
        {
            indexGun++;

            if (indexGun == 1)
            {
                pistol.SetActive(true);
                pistoleActive = true;

                machineGun.SetActive(false);
                machineGunActive = false;

                shotGun.SetActive(false);
                shotGunActive = false;

                change.Play();
            }

            else if (indexGun == 2)
            {
                pistol.SetActive(false);
                pistoleActive = false;

                machineGun.SetActive(true);
                machineGunActive = true;

                shotGun.SetActive(false);
                shotGunActive = false;

                change.Play();
            }

            else if (indexGun == 3)
            {
                pistol.SetActive(false);
                pistoleActive = false;

                machineGun.SetActive(false);
                machineGunActive = false;

                shotGun.SetActive(true);
                shotGunActive = true;

                change.Play();
            }
        }

        else if (Input.mouseScrollDelta.y < 0 && indexGun > 1)
        {
            indexGun--;

            if (indexGun == 1)
            {
                pistol.SetActive(true);
                pistoleActive = true;

                machineGun.SetActive(false);
                machineGunActive = false;

                shotGun.SetActive(false);
                shotGunActive = false;

                change.Play();
            }

            else if (indexGun == 2)
            {
                pistol.SetActive(false);
                pistoleActive = false;

                machineGun.SetActive(true);
                machineGunActive = true;

                shotGun.SetActive(false);
                shotGunActive = false;

                change.Play();
            }

            else if (indexGun == 3)
            {
                pistol.SetActive(false);
                pistoleActive = false;

                machineGun.SetActive(false);
                machineGunActive = false;

                shotGun.SetActive(true);
                shotGunActive = true;

                change.Play();
            }
        }

    }
}
