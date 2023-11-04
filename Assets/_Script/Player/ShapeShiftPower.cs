using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class ShapeShiftPower : MonoBehaviour
{

    private bool is_shapshifting = false;
    private PlayerMovement playerMovementScript;
    [SerializeField] private Vector3 smallSize;
    [SerializeField] private float timeLimitForBeingSmall;
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // if(InputManager.GetInstance.IsChangeSizePressed && !is_shapshifting)
        // {
        //     is_shapshifting = true;
        //     StartCoroutine(Shrinking());
        // }

        if(Input.GetKey(KeyCode.E) && !is_shapshifting)
        {
            is_shapshifting = true;
            StartCoroutine(Shrinking());
        }

        // if(is_shapshifting)
        // {
        //     playerMovementScript.gameObject.SetActive(true);
        // }

        // if(!is_shapshifting)
        // {
        //     playerMovementScript.gameObject.SetActive(true);
        // }
    }

    IEnumerator Shrinking(float waitForSeconds = 0.4f)
    {
        yield return new WaitForSeconds(waitForSeconds); //Wait for 0.4 second
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, smallSize, waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, smallSize, waitForSeconds);

        yield return new WaitForSeconds(timeLimitForBeingSmall);
        StartCoroutine(Expanding());

        yield return null;
    }

    IEnumerator Expanding(float waitForSeconds = 0.4f)
    {
        yield return new WaitForSeconds(waitForSeconds); //Wait for 0.0.5 second
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(1f, 1f, 1f), waitForSeconds);
        yield return new WaitForSeconds(waitForSeconds);
        gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, new Vector3(1f, 1f, 1f), waitForSeconds);
        is_shapshifting = false;
    
        yield return null;
    }
}
