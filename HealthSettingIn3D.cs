using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSettingIn3D : MonoBehaviour
{
    public float NowHealth = 100f;
    public float MaxHealth = 100f;

    public List<Image> brokenGlassUi = new List<Image>();
     public Image restartbutton;
    public Text dietsxt;
    public GameObject diebutton;

    public RectTransform rect;
    public TextMeshProUGUI text;
    public GameObject timerui;
    private void Update()
    {
        if(NowHealth >0)
        {
            rect.transform.localScale = new Vector2(NowHealth / 100,1);
            text.text = NowHealth + "/" + MaxHealth;
        }
        else
        {
            rect.transform.localScale = new Vector2(0,0);
            timerui.GetComponent<CountTime>().Die3D = true;
        }
    }
}
