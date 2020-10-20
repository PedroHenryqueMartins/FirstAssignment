using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int _value;

    public int Value { get { return _value; } private set { _value = value; } }

    public int Collect()
    {
        gameObject.SetActive(false);
        return Value;
    }

}
