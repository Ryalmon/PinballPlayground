using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] float DriftSpeed;
    /// <summary>
    /// Gives the placeable objects a natural upward drift
    /// will probably need to be make less smooth/perfect. It visually feels very unnatural. 
    /// </summary>
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
    /// <summary>
    /// This is supposed to destroy the drifting objects when they hit the killbox above the game
    /// Unfortunately it doesn't work. 
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox"))
        {
            Destroy(gameObject);
        }
    }
}
