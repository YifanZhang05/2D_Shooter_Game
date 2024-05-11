using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    private float timer = 0f;
    private float duration = 0.1f;

    public void startExplosionEffect(float radius, Vector2 center, float duration)
    {
        ExplosionEffect explosion = Instantiate(gameObject, center, Quaternion.identity).GetComponent<ExplosionEffect>();
        explosion.transform.localScale = new Vector2(radius*2, radius*2);
        explosion.duration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
