using UnityEngine;
using System.Collections;
using System;
public class RandomBackgroundScript : MonoBehaviour
{

    // Use this for initialization

    private float Width, Scale, Difference;
    public SpriteRenderer Background1, Background2;
    public Sprite[] Backgrounds = new Sprite[3];

    void Start()
    {
        int index = UnityEngine.Random.Range(0, Backgrounds.Length);
        Background1.sprite = Backgrounds[index];
        Background2.sprite = Backgrounds[index];


        Sprite sprite = Background1.sprite;
        if (sprite == null) return;


        Background1.transform.localScale = new Vector3(1, 1, 1);
        Background2.transform.localScale = new Vector3(1, 1, 1);

        var width = sprite.bounds.size.x;
        var height = sprite.bounds.size.y;

        float ScreenHeight = Camera.main.orthographicSize * 2;
        float ScreenWidth = ScreenHeight / Screen.height * Screen.width * 3f;

        float ScaleX = ScreenWidth / width;
        float ScaleY = ScreenHeight / height;
        Background1.transform.localScale = new Vector3(ScaleX, ScaleY);
        Background1.transform.localPosition = new Vector3(0, 0, 10f);


        Width = sprite.bounds.size.x;
        Scale = ScaleX;
        if(Screen.width < 500) Difference = Width * Scale - 0.5f;
        else Difference = Width * Scale - 1f;

        Background2.transform.localScale = new Vector3(ScaleX, ScaleY);
        Background2.transform.localPosition = new Vector3(Difference, 0, 10f);
    }
    
    void OnBecameInvisible()
    {
        transform.position = new Vector3(transform.position.x + Difference * 2, 0, 10f);
    }
    

    void Update()
    {
        transform.Translate(-Time.deltaTime, 0, 0);
    }
}
