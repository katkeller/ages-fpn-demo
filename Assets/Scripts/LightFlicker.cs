using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private GameObject light;

    [SerializeField]
    private float timer;

    private void Start()
    {
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        light.SetActive(true);
        timer = Random.Range(0.1f, 5);
        yield return new WaitForSeconds(timer);
        light.SetActive(false);
        timer = Random.Range(0.1f, 1);
        yield return new WaitForSeconds(timer);
        StartCoroutine(FlickeringLight());
    }
}
