using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;


    [ContextMenu("Spawn")]
    public void Spawn()
    {

        SpawnInstance();

    }

    public GameObject SpawnInstance()
    {
        var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
        instantiate.transform.localScale = _target.lossyScale;
        instantiate.SetActive(true);

        return instantiate;
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }
}