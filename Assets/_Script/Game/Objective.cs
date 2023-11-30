using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static Objective instance;
    private Transform currentTarget;
    [SerializeField] private Transform marker;
    [SerializeField] private float offset = -90f;

    [SerializeField] private Transform[] targets;
    int index;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        currentTarget = targets[0];
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = currentTarget.position - marker.position;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + offset;
        marker.rotation = Quaternion.Euler(0, angle, 0);
    }
    public void Updatetarget()
    {
        index += 1;
        currentTarget = targets[index];
    }

}
