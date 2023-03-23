using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightsController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _lights;
    [SerializeField]
    private float _timeBetweenDisabling;

    public void BeginDisablingAllLights()
    {
        StartCoroutine(DisableAllLights());
    }

    IEnumerator DisableAllLights()
    {
        for(int i = 0; i < _lights.Length; i++)
        {
            _lights[i].SetActive(false);

            yield return new WaitForSeconds(_timeBetweenDisabling);
        }
    }
}
