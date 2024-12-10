using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour, IPlaceable
{
    [SerializeField] float _forceMultiplier;
    [SerializeField] float _scoreMultiplier = 1;
    [Space]
    [SerializeField] float _destroyTime;
    [Space]
    [SerializeField] GameObject _visuals;

    private Animator _bumperAnimator;

    private void Start()
    {
        _bumperAnimator = GetComponent<Animator>();
        //If check just in case the animator is moved around.
        if(_bumperAnimator == null)
            _bumperAnimator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallPhysics storedBallPhysics = collision.gameObject.GetComponent<BallPhysics>();

        if (storedBallPhysics != null )
        {
            storedBallPhysics.OverrideBallForce(DetermineShootDirection(collision));
            GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Bumper, _scoreMultiplier);
            UniversalManager.Instance.Sound.PlaySFX("HitBumper");
            //SoundManager.Instance.PlaySFX("Bounce");
            if (_bumperAnimator == null) return;
            _bumperAnimator.SetTrigger("Hit");
        }
    }

    private Vector2 DetermineShootDirection(Collision2D collision)
    {
        return (collision.gameObject.transform.position - (Vector3)collision.contacts[collision.contactCount - 1].point).normalized * _forceMultiplier;
    }

    public void Placed()
    {
        //Nothing happens when placed
    }

    public void DestroyPlacedObject()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectOut(_visuals, _destroyTime,null);
        GameplayManagers.Instance.Fade.FadeGameObjectToRed(_visuals, _destroyTime);
        Destroy(gameObject,_destroyTime);
    }

}
