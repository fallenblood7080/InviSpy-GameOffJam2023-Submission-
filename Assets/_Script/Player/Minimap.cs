using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [Header("Minimap Rotations")]
    [SerializeField] private Transform playerRefernce;
    [SerializeField] private float playerOffset = 10f;

    private void Update() 
    {
        if (playerRefernce != null)
        {
            transform.position = new Vector3(playerRefernce.position.x, playerRefernce.position.y + playerOffset, playerRefernce.position.z);   
            transform.rotation = Quaternion.Euler(90f, playerRefernce.eulerAngles.y, 0f);
        }    
    }
}