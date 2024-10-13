using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSet : MonoBehaviour
{

    [SerializeField]CinemachineImpulseSource impulseSource;

    public float Health = 100f;
    public float MaxHealth = 100f;

    public GameObject health_bar;
    public GameObject back_health;

    public List<Image> brokenGlassUi = new List<Image>();
    public float countAlpha = 0;
     public Image restartbutton;
    public Text dietsxt;
    public GameObject diebutton;

    private bool canTouch = true;

    public void HealthUp()
    {
        if(MaxHealth <  241)
        {
            MaxHealth += 40f;
            Health += 20f;
            SetHealth();
            Time.timeScale = 1;
        }
        else
        {
            this.GetComponent<ExpSetting>().finshLevelUps[1] = true;
            Time.timeScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster1" && canTouch)
        {
            canTouch = false;
            Health -= 5;
            SetDamage();
        }
        if (collision.gameObject.tag == "Monster2" && canTouch)
        {
            canTouch = false;
            Health -= 25;
            SetDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "MonsterBullet" && canTouch)
        {
            canTouch = false;
            Health -= 15;
            Destroy(collider2D.gameObject);
            SetDamage();
        }
    }

    public void SetDamage()
    {
        if (Health >=1)
            {
                SetHealth();
                impulseSource.GenerateImpulse();
            }
            else
            {
                health_bar.transform.localScale = new Vector3(0,0,0);
                back_health.transform.localScale = new Vector3(0,0,0);
                StartCoroutine(ChangeAlpha());  
            }
            StartCoroutine(CanTouchReset());
    }
    private IEnumerator ChangeAlpha()
    {
        if(countAlpha <= 50)
        {
            brokenGlassUi[0].color = new Color(255,255,255,countAlpha);
            yield return new WaitForSeconds(0.025f);
            brokenGlassUi[1].color = new Color(255,255,255,countAlpha);
            StartCoroutine(ChangeAlpha());  
            countAlpha++;
            impulseSource.GenerateImpulse();
        }
        else if (countAlpha == 51)
        {       
            brokenGlassUi[2].color = new Color(255,255,255,255);
            yield return new WaitForSeconds(3f);
            StartCoroutine(ChangeAlpha());
            countAlpha++;
        }
        else
        {
            yield return new WaitForSeconds(3f);
            diebutton.transform.parent = brokenGlassUi[0].transform.parent;
            restartbutton.color = new Color(restartbutton.color.r,restartbutton.color.g,restartbutton.color.b,255);
            dietsxt.color = new Color(dietsxt.color.r,dietsxt.color.g,dietsxt.color.b,255);
            diebutton.GetComponent<RectTransform>().anchoredPosition  = new Vector2(0,0);
        }
    }


    private void SetHealth()
    {
        health_bar.transform.localScale = new Vector3(Health / MaxHealth,health_bar.transform.localScale.y,health_bar.transform.localScale.z);
        HealthBackground();
    }

    private void HealthBackground()
    {
        if(back_health.transform.localScale.x <= health_bar.transform.localScale.x)
        {
            back_health.transform.localScale = new Vector3(health_bar.transform.localScale.x,back_health.transform.localScale.y,back_health.transform.localScale.z);
        }
        else
        {
            back_health.transform.localScale = new Vector3(back_health.transform.localScale.x - 0.1f,back_health.transform.localScale.y,back_health.transform.localScale.z);
            StartCoroutine(Enumerator());
        }
    }

    private IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(0.075f);
        HealthBackground();
    }

    private IEnumerator CanTouchReset()
    {
        yield return new WaitForSeconds(0.05f);
        canTouch = true;
    }

}