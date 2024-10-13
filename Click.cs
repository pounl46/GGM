using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Click : MonoBehaviour
{   
    public GameObject bullet;

    public GameObject spawnVec;

    public float delay = 0.25f;

    private bool canShoot = true;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            Instantiate(bullet,spawnVec.transform.position,transform.rotation);
            canShoot = false;
            StartCoroutine(enumerator());
        }
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}