using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float _tutorialWaitTime;

    [SerializeField] private GameObject _placementField;
    [SerializeField] private GameObject _ballLauncher;
    [SerializeField] private GameObject _tutorialArt;
    [SerializeField] private GameObject _tutorialText;

    [SerializeField] private SpriteRenderer[] _nonImportantItems;
    [SerializeField] private SpriteRenderer[] _specialItems;
    [SerializeField] private GameObject[] _additionalObjects;

    [SerializeField] private SpriteRenderer _backgroundImage;
    [SerializeField] private SpriteRenderer _borderImage;

    [SerializeField] private Color _darken = new Color(0.45f, 0.45f, 0.45f);
    [SerializeField] private Color _normal = new Color(1, 1, 1,0);

    private bool _isTutorialActive;
   
    public void ToggleTutorial()
    {
        _isTutorialActive = !_isTutorialActive;

        if(_isTutorialActive)
        {
            TutorialPopUp();
        }
        else
        {
            SetTutorialStateFalse();
        }
    }

    private void TutorialPopUp()
    {
        GameplayManagers.Instance.State.OnTutorialStartEvent?.Invoke();

        Time.timeScale = 0f;

        _placementField.SetActive(true);
        _ballLauncher.SetActive(false);
        _tutorialArt.SetActive(true);
        _tutorialText.SetActive(true);

        _backgroundImage.color = _darken;
        //_borderImage.color = _darken; we dont need that

        for(int i = 0; i < _nonImportantItems.Length; ++i)
        {
            if (_nonImportantItems[i] == null) continue;
            _nonImportantItems[i].color = _darken;
        }

        for(int i = 0; i < _specialItems.Length; ++i)
        {
            if (_specialItems[i] == null) continue;
            _specialItems[i].color = new Color(_specialItems[i].color.r, _specialItems[i].color.g, _specialItems[i].color.b, 0.25f);
        }

        for(int i = 0; i < _additionalObjects.Length; ++i)
        {
            if (_additionalObjects[i].gameObject == null) continue;
            _additionalObjects[i].SetActive(true);
        }
    }

    public void SetTutorialStateFalse()
    {
        Time.timeScale = 1f;

        _placementField.SetActive(false);
        _ballLauncher.SetActive(true);
        _tutorialArt.SetActive(false);
        _tutorialText.SetActive(false);

        GameplayManagers.Instance.Ball.ShooterBegin();

        _backgroundImage.color = _normal;

        for (int i = 0; i < _nonImportantItems.Length; ++i)
        {
            if (_nonImportantItems[i] == null) continue;
            _nonImportantItems[i].color = _normal;
        }

        for (int i = 0; i < _specialItems.Length; ++i)
        {
            if (_specialItems[i] == null) continue;
            _specialItems[i].color = new Color(_specialItems[i].color.r, _specialItems[i].color.g, _specialItems[i].color.b, 1f);
        }

        for (int i = 0; i < _additionalObjects.Length; ++i)
        {
            if (_additionalObjects[i] == null) continue;
            _additionalObjects[i].gameObject.SetActive(false);
        }

        GameplayManagers.Instance.State.OnTutorialEndEvent?.Invoke();
    }
}
