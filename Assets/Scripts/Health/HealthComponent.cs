using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHP;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] public UnityEvent _onDie;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] public HealthChangeEvent _onChange;
    


    [SerializeField] private bool _immune;

    public int HP => _hp;
    public int MaxHP => _maxHP;

    public bool Immune
    {
        get => _immune; set => _immune = value;
    }

    public void Initialized(int HP, int MaxHP)
    {
        _hp = HP;
        _maxHP = MaxHP;
    }
    public void ModifyHealth(int healthDelta)
    {
       
        if (healthDelta < 0 && Immune) return;
        if (_hp <= 0) return;
        _hp += healthDelta;
        _onChange?.Invoke(_hp);

        if (healthDelta < 0)
        {
            _onDamage?.Invoke();
        }

        if (healthDelta > 0)
        {
            _onHeal?.Invoke();
        }

        if (_hp <= 0)
        {
            _onDie!?.Invoke();
        }

    }

#if UNITY_EDITOR
    [ContextMenu("Update Health")]
    private void UpdateHealth()
    {

        _onChange?.Invoke(_hp);
    }
#endif


   //public void SetHealth(int health)
  //  {
    //    _health = health;
   // }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {
       
    }
}
