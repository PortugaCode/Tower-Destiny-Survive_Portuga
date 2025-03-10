using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{

    [SerializeField] private float textureWidth;
    [SerializeField] private float speed;

    public void SetSpeed(float value)
    {
        this.speed = value;
    }

    private void Start()
    {
        SetUpTexture();
    }

    private void SetUpTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void Scroll()
    {
        float delta = -speed * Time.deltaTime;
        transform.position += new Vector3(delta, 0, 0f);
    }

    private void CheckReset()
    {
        if((Mathf.Abs(transform.position.x) - textureWidth) > 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        Scroll();
        CheckReset();
    }
}
