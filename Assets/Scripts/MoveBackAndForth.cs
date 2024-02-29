using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackAndForth : MonoBehaviour
{
    [SerializeField] private Vector2[] MovePoints;
    [SerializeField] private float _speed;
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //check distance between platform and movepoint
        if (Vector3.Distance(transform.position, MovePoints[currentIndex]) < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= MovePoints.Length)
            {
                currentIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, MovePoints[currentIndex],
            _speed * Time.deltaTime);
    }
}
