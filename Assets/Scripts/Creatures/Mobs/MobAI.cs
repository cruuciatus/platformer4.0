using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private LayerCheckComponent _vision;
    [SerializeField] private LayerCheckComponent _canAttack;
    [SerializeField] private float _alarmDelay = 0.5f;
    [SerializeField] private float _attackCoolDown = 1f;
    [SerializeField] private float _missHeroCoolDown = 0.5f;

    private Coroutine _current;
    private GameObject _target;

    private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

    private SpawnListComponent _particles;
    private Creature _creature;
    private Animator _animator;
    private bool _isDead;
    private Patrol _patrol;


    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
        _patrol = GetComponent<Patrol>();
    }
    private void Start()
    {
        StartState(_patrol.DoPatrol());

    }


    public void OnHeroInVision(GameObject go)
    {
        if (_isDead) return;
        _target = go;
        StartState(AgroToHero());
    }

    private IEnumerator AgroToHero()
    { 
        LookAtHero();
        _particles.Spawn("Exclamation");
        yield return new WaitForSeconds(_alarmDelay);

        StartState(GoToHero());
    }

    public void LookAtHero()
    {
       var direction = GetDirectionToTarget();
        _creature.SetDirection(Vector2.zero);
        _creature.UpdateSpriteDirection(direction);
    }

    private IEnumerator GoToHero()
    {
        while (_vision.IsTouchingLayer)
        {
            if (_canAttack.IsTouchingLayer)
            {

                StartState(Attack());
            }
            else

            {
                SetDirectionToTarget();
            }

            yield return null;
        }

        _creature.SetDirection(Vector2.zero);
        _particles.Spawn("MissHero");
        yield return new WaitForSeconds(_missHeroCoolDown);

        StartState(_patrol.DoPatrol());
    }

    private IEnumerator Attack()
    {
        while (_canAttack.IsTouchingLayer)
        {
            _creature.Attack();
            yield return new WaitForSeconds(_attackCoolDown);
        }
        StartState(GoToHero());
    }

    private void SetDirectionToTarget()
    {

        var direction = GetDirectionToTarget();
        _creature.SetDirection(direction);
    }

    private Vector2 GetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
      //  direction.y = 0f;
        return direction.normalized;

    }

  

    private void StartState(IEnumerator coroutine)
    {

        _creature.SetDirection(Vector2.zero);
        if (_current != null)
        {
            StopCoroutine(_current);
        }
        _current = StartCoroutine(coroutine);

    }

    public void OnDie()

    {
        _creature.SetDirection(Vector2.zero);
        _isDead = true;
        _animator.SetBool(IsDeadKey, true);
        if (_current != null)
        {
            StopCoroutine(_current);
        }
       
    }
}
