using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float mLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        mLifeTime = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        mLifeTime -= Time.deltaTime;
        if (mLifeTime <= 0.0f) Destroy(gameObject);
        GetComponent<SpriteRenderer>().material.SetFloat("_FadeAmount", 1 - mLifeTime * 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
