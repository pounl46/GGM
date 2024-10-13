using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BossStart : MonoBehaviour
{
    [SerializeField]CinemachineImpulseSource impulseSource;
    public bool BossAttackStart = false;
    public float BossHealth = 10000;
    public GameObject player;
    public GameObject waveObj;
    public GameObject fallowObj;
    public GameObject particle;
    public GameObject healthBar;
    public TextMeshProUGUI healthText;
    public Volume volume;
    public Image whiteImg;
    public Image endImage;

    public GameObject Down;
    private bool die = false;
    private bool StopAll = false;
    private LensDistortion lensDistortion;
    private float alpha = 0;
    private IEnumerator alphas()
    {
        yield return new WaitForSeconds(.5f);
        alpha += .1f;
        whiteImg.color = new Color(1,1,1,alpha);
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
            {
                lensDistortion.intensity.value -= alpha;
            }
        if(alpha < 1)
        {
            StartCoroutine(alphas());
        }else
        {
            endImage.color = new Color(1,1,1,1);
            if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
            {
                lensDistortion.intensity.value = 0;
                StopAll = true;
                GetComponent<AudioSource>().Stop();
            }
        }
    }

    void Update()
    {
        if(BossAttackStart)
        {
            BossAttackStart = false;
            StartCoroutine(Started());
        }
        if(BossHealth > 0)
        {
            healthBar.GetComponent<RectTransform>().transform.localScale = new Vector2(BossHealth / 10000,1);
            healthText.text = "Boss HP : " + BossHealth;
        }
        else if(BossHealth <= 0 && !die)
        {
            healthBar.GetComponent<RectTransform>().transform.localScale = new Vector2(0,1);
            healthText.text = " ";
            die = true;
            Time.timeScale = .5f;
            whiteImg.color = new Color(1,1,1,alpha);
            GetComponent<AudioSource>().pitch = .5f;
            StartCoroutine(alphas());
        }
    }

    private IEnumerator Started()
    {
        impulseSource.GenerateImpulse(.25f);
        yield return new WaitForSeconds(2);
        StartCoroutine(Wave());
    }

    public IEnumerator Wave()
    {
        impulseSource.GenerateImpulse(.25f);
        yield return new WaitForSeconds(.5f);
        GameObject waveskill = Instantiate(waveObj,new Vector3(transform.position.x,6,transform.position.z),Quaternion.Euler(90,0,0));
        waveskill.GetComponent<skillRound>().impulseSources = impulseSource;
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fallow());
    }
    public IEnumerator Fallow()
    {
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i < 5;i++)
        {
            impulseSource.GenerateImpulse(.25f);
            yield return new WaitForSeconds(.15f);
            GameObject fallows = Instantiate(fallowObj,new Vector3(transform.position.x + i + i + i + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles = Instantiate(particle,new Vector3(transform.position.x + i + i + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            fallows.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows.GetComponent<AudioSource>().Play();
            Destroy(particles,1f);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(Wave2());
    }
    public IEnumerator Wave2()
    {
        for(int i = 0; i<3;i++)
        {
            impulseSource.GenerateImpulse(.25f);
            yield return new WaitForSeconds(.25f);
            GameObject waveskill = Instantiate(waveObj,new Vector3(transform.position.x,6,transform.position.z),Quaternion.Euler(90,0,0));
            waveskill.GetComponent<skillRound>().impulseSources = impulseSource;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(FallowPlseWave());
    }
    public IEnumerator FallowPlseWave()
    {
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i < 7;i++)
        {
            impulseSource.GenerateImpulse(.25f);
            yield return new WaitForSeconds(.25f);
            GameObject fallows = Instantiate(fallowObj,new Vector3(transform.position.x + i + i + i + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles = Instantiate(particle,new Vector3(transform.position.x + i + i + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject fallows2 = Instantiate(fallowObj,new Vector3(transform.position.x - i - i - i - i - i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles2 = Instantiate(particle,new Vector3(transform.position.x - i - i - i - i,19,transform.position.z),Quaternion.Euler(90,0,0));
            fallows.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows2.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows2.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows.GetComponent<AudioSource>().Play();
            Destroy(particles,1f);
            Destroy(particles2,1f);
        }
        impulseSource.GenerateImpulse(.25f);
        yield return new WaitForSeconds(.5f);
        GameObject waveskill = Instantiate(waveObj,new Vector3(transform.position.x,6,transform.position.z),Quaternion.Euler(90,0,0));
        waveskill.GetComponent<skillRound>().impulseSources = impulseSource;
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fallow2());
    }
    public IEnumerator Fallow2()
    {
        yield return new WaitForSeconds(.1f);
        for(int i = 0; i < 10;i++)
        {
            impulseSource.GenerateImpulse(.25f);
            yield return new WaitForSeconds(.15f);
            GameObject fallows = Instantiate(fallowObj,new Vector3(transform.position.x + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles = Instantiate(particle,new Vector3(transform.position.x + i + i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject fallows2 = Instantiate(fallowObj,new Vector3(transform.position.x - i - i,19,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles2 = Instantiate(particle,new Vector3(transform.position.x - i - i,19,transform.position.z),Quaternion.Euler(90,0,0));
            fallows.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows2.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows2.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows.GetComponent<AudioSource>().Play();
            Destroy(particles,1f);
            Destroy(particles2,1f);
        }
        for(int i = 0; i < 10;i++)
        {
            impulseSource.GenerateImpulse(.25f);
            yield return new WaitForSeconds(.15f);
            GameObject fallows = Instantiate(fallowObj,new Vector3(transform.position.x,19 + i + i,transform.position.z),Quaternion.Euler(90,0,0));
            GameObject particles = Instantiate(particle,new Vector3(transform.position.x,19 + i + i,transform.position.z),Quaternion.Euler(90,0,0));
            fallows.GetComponent<rotationselfSkills>().impulseSources = impulseSource;
            fallows.GetComponent<rotationselfSkills>().LookAtObj = player.transform;
            fallows.GetComponent<AudioSource>().Play();
            Destroy(particles,1f);
        }
        yield return new WaitForSeconds(3f);
        this.transform.position -= new Vector3(0,25,0);
        StartCoroutine(Cooltime());
    }
    IEnumerator Cooltime()
    {
        for (int i = 0; i<10;i++)
        {
            yield return new WaitForSeconds(1f);
            impulseSource.GenerateImpulse(.25f);
            Instantiate(Down,new Vector3(player.transform.position.x,5.6f,player.transform.position.z),Quaternion.Euler(90,0,0));
            if(i == 9)
            {
                if (BossHealth > 0)
                {
                    if(!StopAll)
                    {
                        StartCoroutine(Fallow());
                        this.transform.position += new Vector3(0,25,0);
                    }
                }
            }
        }
    }
}
