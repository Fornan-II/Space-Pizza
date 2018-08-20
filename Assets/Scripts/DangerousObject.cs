using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObject : MonoBehaviour {

    protected Collider2D _col;

    public float DamageAmount = 1.0f;
    public float KnockbackMultiplier = 20.0f;
    public float rotateSpeed = 30.0f;

    protected static float damageCoolDown = 1.0f;
    protected bool canDamage = true;

    // Use this for initialization
    protected virtual void Start()
    {
        _col = gameObject.GetComponent<Collider2D>();
        if (!_col)
        {
            Debug.LogError(name + "'s DangerousObject does not have a collider!");
        }
    }

    protected virtual void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(canDamage)
        {
            Vector2 kbVector = collision.transform.position - gameObject.transform.position;
            kbVector = KnockbackMultiplier * kbVector.normalized;
            collision.rigidbody.AddForce(kbVector, ForceMode2D.Impulse);

            Pawn collisionPawn = collision.gameObject.GetComponent<Pawn>();
            if (collisionPawn)
            {
                collisionPawn.TakeDamage(DamageAmount);
            }

            StartCoroutine(PutDamageOnCoolDown());
        }
    }

    protected virtual IEnumerator PutDamageOnCoolDown()
    {
        float coolDown = damageCoolDown;
        canDamage = false;
        while(coolDown > 0.0f)
        {
            yield return null;
            coolDown -= Time.deltaTime;
        }
        canDamage = true;
    }
}
