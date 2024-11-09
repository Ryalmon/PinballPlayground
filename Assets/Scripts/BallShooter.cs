using System.Collections;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject _visuals;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _flipTime;

    [SerializeField] private Vector3 _turnEnd1;
    [SerializeField] private Vector3 _turnEnd2;

    [SerializeField] private GameObject _ballShootPoint;
    [SerializeField] private Vector3 _startRotation;

    private Coroutine rotate1;
    private Coroutine rotate2;

    // Start is called before the first frame update
    public void Begin()
    {
        transform.eulerAngles = _startRotation;
        AssignEvents();
        rotate1 = StartCoroutine(Rotation());
        rotate2 = StartCoroutine(Rotate2());
    }
    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetBallDeactiveEvent().AddListener(ShowBallShooter);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(FireAnimation);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(HideBallShooter);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(StopRotation);
    }

    private void StopRotation()
    {
        StopCoroutine(rotate1);
        StopCoroutine(rotate2);
    }

    public Vector2 ShootBallDir()
    {
        UniversalManager.Instance.Sound.PlaySFX("BallLaunch");
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
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                //Mathf.Round(transform.eulerAngles.z / 90) * 90);
        }
    }


    public void FireAnimation()
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Fire");
    }

    private void ShowBallShooter()
    {
        _visuals.transform.eulerAngles = new Vector3(0, 0, 90);
        GameplayManagers.Instance.Fade.FadeGameObjectIn(_visuals, .5f, null);
        //GetComponentInChildren<SpriteRenderer>().enabled = true;
        _rotateSpeed = 90;
        rotate1 = StartCoroutine(Rotation());
        rotate2 = StartCoroutine(Rotate2());
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
