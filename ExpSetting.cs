using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class ExpSetting : MonoBehaviour
{
    private float exp = 50;
    private float nowExp = 0f;
    private int level = 1;

    public GameObject expBar;

    public GameObject levelUpUi;

    public TextMeshProUGUI levelText;

    public GameObject canvas;

    public List<GameObject> levelUps = new List<GameObject>();
    
    private List<GameObject> uiDestroys = new List<GameObject>();

    private void Start()
    {
         levelText.text = "Your Level : " + level;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "5exp")
        {
            SetScale(5,collider2D.transform,collider2D.gameObject);
        }

        if (collider2D.gameObject.tag == "15exp")
        {
            SetScale(15,collider2D.transform,collider2D.gameObject);
        }

        if (collider2D.gameObject.tag == "25exp")
        {
            SetScale(25,collider2D.transform,collider2D.gameObject);
        }
    }

    private void SetScale(int exps,Transform vec, GameObject destoryActor)
    {
        nowExp += exps;
        GameObject obj = Instantiate(levelUpUi,vec.position,levelUpUi.transform.rotation);
        TextMesh textMesh = obj.GetComponent<TextMesh>();
        textMesh.text = "+"+exps+"exp!";
        Destroy(destoryActor);
        LevelUp();
        Vector2 scale = expBar.transform.localScale;
        expBar.transform.localScale = new Vector2((nowExp / exp),scale.y);
    }
    public List<GameObject> Tops = new List<GameObject>();
    public GameObject bullet;
    private int CountPlusAllDam = 0;
    public float pluseallDamageLv = 0;
    public void PlusAllDam()
    {
        if(CountPlusAllDam == 5)
        {
            finshLevelUps[0] = true;
        }
        else
        {
            CountPlusAllDam++;
            pluseallDamageLv++;
            for(int i=0; i < Tops.Count;i++)
            {
                Tops[i].GetComponent<Top_Rotation_Attack>().pluseTopDam ++;
            }
            bullet.GetComponent<launch>().plusBaltDam++;
        }
        Time.timeScale = 1;
    }

    public List<bool> finshLevelUps = new List<bool> {true,true,true,true}; //levelUps 리스트 순서가 기준.
    private void LevelUp()
    {
        if(nowExp >= exp && levelUps.Count > 0)
        {
            for (int i = finshLevelUps.Count - 1; i >= 0; i--)
            {
                if (finshLevelUps[i])
                {
                    levelUps.RemoveAt(i);
                    finshLevelUps.RemoveAt(i);
                }
            }

            level += 1;
            nowExp -= exp;
            exp += exp / 1.5f / level;
            levelText.text = "Your Level : " + level;
            Time.timeScale = 0;
            Debug.Log(levelUps.Count);
            for(int i = 0;i<= 2;i++)
            {
                RectTransform rect;

                int rand = Random.Range(0,levelUps.Count);

                GameObject instance;

                instance = Instantiate(levelUps[rand],new Vector2(0,0),levelUps[rand].transform.rotation);

                rect = instance.GetComponent<RectTransform>();

                instance.transform.parent = canvas.transform;

                RectTransform canvasRect;
                canvasRect = canvas.GetComponent<RectTransform>();

                rect.position = new Vector2(canvasRect.position.x + i * 5 - 3f,canvasRect.position.y);
                rect.localScale = new Vector2(1,1.25f);

                uiDestroys.Add(instance);
            }
        }
        else
        {
            Debug.Log("만렙");
        }
    }

    public void Des()
    {
        for (int i = 0; i <= uiDestroys.Count - 1;i++)
        {
            Destroy(uiDestroys[i]);
        }
    }
}
