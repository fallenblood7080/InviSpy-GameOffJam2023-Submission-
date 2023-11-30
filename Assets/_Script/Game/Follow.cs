using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = new(target.position.x, transform.position.y, target.position.z);
    }
}
