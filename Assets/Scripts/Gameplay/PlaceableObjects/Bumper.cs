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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null )
        {
            collision.gameObject.GetComponent<BallPhysics>().OverrideBallForce(DetermineShootDirection(collision));
            GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Bumper, _scoreMultiplier);
            UniversalManager.Instance.Sound.PlaySFX("HitBumper");
            //SoundManager.Instance.PlaySFX("Bounce");
            Animator animator = GetComponent<Animator>();
            if (animator == null) animator = GetComponentInParent<Animator>();
            if (animator == null) return;
            animator.SetTrigger("Hit");
        }
    }

    private Vector2 DetermineShootDirection(Collision2D collision)
    {
        return (collision.gameObject.transform.position - (Vector3)collision.contacts[collision.contactCount - 1].point).normalized * _forceMultiplier;
    }

    public void Placed()
    {
        GetComponent<Drift>().enabled = true;
        GetComponent<Bumper>().enabled = true;
    }

    public void DestroyPlacedObject()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectOut(_visuals, _destroyTime,null);
        GameplayManagers.Instance.Fade.FadeGameObjectToRed(_visuals, _destroyTime);
        Destroy(gameObject,_destroyTime);
    }

}
