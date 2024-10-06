using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private SpriteRenderer sprite;

    private float timeBtwAtt;
    public float startTimeBtwAtt;
    private float stunTimer;
    public float stunTimerDuration;

    //public Transform attPos;
    public GameObject attObj;
    //public LayerMask player;
    //public float range;
    //private int dmg;

    private bool messageSent = false;
    public bool stun = false;
    public bool attObjVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stun)
        {
            if (timeBtwAtt <= 0)
            {
                //Collider2D[] playersHit = Physics2D.OverlapCircleAll(attPos.position, range, player);

                //for (int i = 0; i < playersHit.Length; i++)
                //{
                //    Debug.Log("Player Hit!");
                //}
                StartCoroutine(EnemyAttack());

                timeBtwAtt = startTimeBtwAtt;
            }

            else
            {
                timeBtwAtt -= Time.deltaTime;
            }
        }

        else
        {
            if (stunTimer <= 0)
            {
                stun = false;
                sprite.color = Color.cyan;
            }

            else
            {
                stunTimer -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log("Take " + damage + " Dmg, health now at " + health);
            if (health <= 0)
            {
                sprite.color = Color.magenta;
            }
        }
    }

    private IEnumerator EnemyAttack()
    {
        attObj.SetActive (true);
        attObjVisible = true;
        yield return new WaitForSeconds(1);
        if (attObjVisible)
        {
            attObj.SetActive(false);
            attObjVisible = false;
        }
    }

    public void Stunned()
    {
        stun = true;
        stunTimer = stunTimerDuration;
        sprite.color = Color.yellow;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attPos == null)
    //        return;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attPos.position, range);
    //}
}
