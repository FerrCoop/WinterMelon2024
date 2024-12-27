using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : Unit
{
    [SerializeField] private float damage;
    [SerializeField] private float secondsPerAttack;

    IDamagable target;
    float attackCooldown;

    private void Update()
    {
        attackCooldown += Time.deltaTime;

        if (target != null)
        {
            if (attackCooldown > secondsPerAttack)
            {
                Attack();
            }
        }
        else
        {
            Collider2D[] _targets = Physics2D.OverlapBoxAll(this.transform.position
                + new Vector3(0.5f * range * teamData.Direction, 0f, 0f), new Vector3(0.5f * range, 5f, 0f), 0f, teamData.EnemyLayer);
            if (_targets.Length > 0)
            {
                //try find target
                foreach (Collider2D _target in _targets)
                {
                    if (_target.GetType() == typeof(BaseManager))
                    {
                        target = _target.GetComponent<IDamagable>();
                        break;
                    }
                    else if (_target.GetType() == typeof(Building) &&
                        ((target != null && target.GetType() != typeof(Building)) || target == null))
                    {
                        target = _target.GetComponent<IDamagable>();
                    }
                    else
                    {
                        target = _target.GetComponent<Unit>();
                    }
                }
            }
            else
            {
                //advance
                transform.Translate(new Vector3(teamData.Direction * maxSpeed * SpeedMultiplier * Time.deltaTime, 0f));
            }
        }
    }

    private void Attack()
    {
        target.Damage(damage * DamageMultiplier);
        attackCooldown = 0f;
    }
}
