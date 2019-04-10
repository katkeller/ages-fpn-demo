using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private GameObject light;

    [SerializeField]
    private float timer;

    [Tooltip("The maximum time that the light will be left randomly off or on.")]
    [SerializeField]
    private float secondsOnMax = 5.0f, secondsOffMax = 1.0f;

    private void Start()
    {
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        light.SetActive(true);
        timer = Random.Range(0.1f, secondsOnMax);
        yield return new WaitForSeconds(timer);
        light.SetActive(false);
        timer = Random.Range(0.1f, secondsOffMax);
        yield return new WaitForSeconds(timer);
        StartCoroutine(FlickeringLight());
    }
}
