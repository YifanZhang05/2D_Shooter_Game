using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMagicAttack : MonoBehaviour
{
    [SerializeField] private GameObject freezeEffect;
    public float freezeChance;
    private float freezeTime = 1f;

    public float radius;
    private float knockBack = 0;
    public float damage;

    // controls how long the effect circle will remain visible
    private float timer = 0f;
    private float duration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // set the size of display of effect area
        transform.localScale = new Vector2(radius*2, radius*2);

        // find all colliders in the magic radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Game.mousePos, radius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.tag == "Monster")
            {
                Monster monster = collider.GetComponent<Monster>();
                monster.monsterGetHit(new Vector2(0, 0), knockBack, damage);

                // randomly decide if freeze or not
                float randomValue = Random.Range(0.0f, 1.0f);
                if (randomValue < freezeChance)
                {
                    monster.freezeMonster(freezeTime, freezeEffect);
                }
                
            }
            else if (collider.tag == "Tree")
            {
                Tree tree = collider.GetComponent<Tree>();
                tree.treeLoseHealth(damage, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // the effect circle visible for duration seconds
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

