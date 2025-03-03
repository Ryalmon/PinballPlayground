using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggles : MonoBehaviour
{
    [SerializeField] private bool _isMusicToggle;
    [SerializeField] private Image _visuals;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    // Start is called before the first frame update
    void Start()
    {
        if(_isMusicToggle)
        {
            DetermineSprite(UniversalManager.Instance.Sound.DoesPlayMusic);
        }
        else
        {
            DetermineSprite(UniversalManager.Instance.Sound.DoesPlaySFX);
        }
    }

    public void ButtonPress()
    {
        if(_isMusicToggle)
        {
            UniversalManager.Instance.Sound.ToggleMusic();
            DetermineSprite(UniversalManager.Instance.Sound.DoesPlayMusic);
        }
        else
        {
            UniversalManager.Instance.Sound.ToggleSFX();
            DetermineSprite(UniversalManager.Instance.Sound.DoesPlaySFX);
        }
    }

    private void DetermineSprite(bool onOff)
    {
        _visuals.sprite = onOff ? _onSprite : _offSprite;
    }
}
