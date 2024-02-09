using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] float DriftSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NaturalDrift());
    }
    private IEnumerator NaturalDrift()
    {
        while (true)
        {
            transform.position += new Vector3(0, DriftSpeed, 0) * Time.deltaTime;
            yield return null;
        }

    }
}
