using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BallShooter : MonoBehaviour
{
    private Vector3 currentRotation;
    [SerializeField] private GameObject _visuals;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _flipTime;

    [SerializeField] private Vector3 _turnEnd1;
    [SerializeField] private Vector3 _turnEnd2;

    [SerializeField] private GameObject _ballShootPoint;
    [SerializeField] private Vector3 _startRotation;


    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = _startRotation;
        AssignEvents();
        StartCoroutine(Rotation());
        StartCoroutine(Rotate2());
    }
    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetBallDeactiveEvent().AddListener(ShowBallShooter);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(HideBallShooter);
    }

    public Vector2 ShootBallDir()
    {
        UniversalManager.Instance.Sound.PlaySFX("Launch");
        //SoundManager.Instance.PlaySFX("Launch");
        return (_ballShootPoint.transform.position - transform.position).normalized;
    }

    private IEnumerator Rotation()
    {
        while (true)
        {
            //transform.position += transform.forward * PlaneSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, _turnEnd1, _rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Rotate2()
    {
        while (true)
        {
            yield return new WaitForSeconds(_flipTime);
            _rotateSpeed *= -1;
        }
    }

    private void ShowBallShooter()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectIn(_visuals, .5f, null);
        //GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    private void HideBallShooter()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectOut(_visuals, .5f, null);
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public Vector3 GetBallShootPoint()
    {
        return _ballShootPoint.transform.position;
    }
}
