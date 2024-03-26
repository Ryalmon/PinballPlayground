using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour, IPlaceable
{
    [SerializeField] float _forceMultiplier;
    [Space]
    [SerializeField] float _destroyTime;
    [Space]
    [SerializeField] GameObject _visuals;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null )
        {
            Debug.DrawRay(collision.contacts[collision.contactCount-1].point, collision.gameObject.transform.position - (Vector3)collision.contacts[collision.contactCount-1].point, Color.green,5);

            collision.gameObject.GetComponent<BallPhysics>().OverrideBallForce(DetermineShootDirection(collision));
            GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Bumper);
            UniversalManager.Instance.Sound.PlaySFX("Bounce");
            //SoundManager.Instance.PlaySFX("Bounce");
            Animator animator = GetComponent<Animator>();
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
        Destroy(gameObject,_destroyTime);
    }

}
