using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{


    private Rigidbody2D body;

    public int speed = 7;
    public float plusSpeed = 0f;
    private Vector2 move;

    public float delay = .25f;
    public GameObject mouseObj;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Screen.SetResolution(1920, 1080, true);
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        move = new Vector2(x,y);
        body.velocity = move * (speed + plusSpeed);
        delay = mouseObj.GetComponent<Click>().delay;
    }   
    public GameObject mouse;
    public void PlusSpeed()
    {
        plusSpeed += 1;
        mouse.GetComponent<Click>().delay -= 0.025f;
        Time.timeScale = 1;
        if (plusSpeed >= 6)
        {
            mouse.GetComponent<Click>().delay = 0.01f;
            this.GetComponent<ExpSetting>().finshLevelUps[2] = true;
        }
    }
}
