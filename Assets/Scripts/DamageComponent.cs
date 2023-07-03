using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private int _hpDelta;
    

    public void ApplyHealthDelta(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ModifyHealth(_hpDelta);

        }
    }
    


    
}
