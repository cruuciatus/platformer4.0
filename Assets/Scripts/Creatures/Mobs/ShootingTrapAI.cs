using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrapAI : MonoBehaviour
{
    [SerializeField] private LayerCheckComponent _vision;

    [Header("Melee")]
    [SerializeField] private CheckCircleOvetlap _meleeAttack;
    [SerializeField] private Cooldown _meleeCooldown;
    [SerializeField] private LayerCheckComponent _meleeCanAttack;

    [Header("Range ")]
    [SerializeField] private Cooldown _rangeCooldown;
    [SerializeField] private SpawnComponent _rangeAttack;
    private Animator _animator;

    private static readonly int Melee = Animator.StringToHash("melee");
    private static readonly int Range = Animator.StringToHash("range");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_vision.IsTouchingLayer)
        {  
            if (_meleeCanAttack.IsTouchingLayer)
            {
                if (_meleeCooldown.IsReady)
                {
                    MeleeAttack();
                }
                return;
            }
            if (_rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
    }

    private void RangeAttack()
    {
        _rangeCooldown.Reset();
        _animator.SetTrigger(Range);
    }

    private void MeleeAttack()
    {
        _meleeCooldown.Reset();
        _animator.SetTrigger(Melee);
    }

    public void OnRangeAttack()
    {
        _rangeAttack.Spawn();
    }
    public void OnMeleeAttack()
    {
        _meleeAttack.Check();
    }
}
