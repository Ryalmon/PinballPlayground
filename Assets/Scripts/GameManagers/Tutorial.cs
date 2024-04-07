using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    [SerializeField] private GameObject _placementField;
    [SerializeField] private GameObject _ballLauncher;
    [SerializeField] private GameObject _ContinueButton;
    [Space]

    [SerializeField] private SpriteRenderer _backgroundImage;
    [SerializeField] private SpriteRenderer _borderImage;
    [Space]

    [SerializeField] private Color _darken = new Color(0.45f, 0.45f, 0.45f);
    [SerializeField] private Color _normal = new Color(1, 1, 1);
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TutorialPopUp());
    }

    private IEnumerator TutorialPopUp()
    {
        yield return new WaitForSeconds(_waitTime);

        Time.timeScale = 0f;

        _ContinueButton.SetActive(true);
        _placementField.SetActive(true);
        _ballLauncher.SetActive(false);

        _backgroundImage.color = _darken;
        _borderImage.color = _darken;
    }

    public void SetTutorialStateFalse()
    {
        Time.timeScale = 1f;

        _ContinueButton.SetActive(false);
        _placementField.SetActive(false);
        _ballLauncher.SetActive(true);

        _backgroundImage.color = _normal;
        _borderImage.color = _normal;
    }
}
