using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SplashScreenScript : MonoBehaviour
{
    private CanvasGroup logo;
    private RectTransform logoRect;
    [SerializeField] RectTransform mask;

    private float alphaValue = 1f;

    private Tween fadeTween;
    private Tween scaleTween;
    private Tween maskScaleTween;
    private Sequence splashScreenSequence;
    //private bool loadNewScene = false;

    private void Start()
    {
        logo = GetComponent<CanvasGroup>();
        logoRect = GetComponent<RectTransform>();

        logo.alpha = alphaValue;
        logoRect.transform.localScale = Vector3.zero;
        int maskHeight = 190;
        mask.sizeDelta = new Vector2(mask.sizeDelta.x, -maskHeight);

        StartCoroutine(LogoAnimation());
    }

    IEnumerator LogoAnimation()
    {
        yield return new WaitForSeconds(1f);
        FadeInandScaleIn();
        yield return new WaitForSeconds(8.5f);
        SceneManager.LoadScene(1);
    }
    void FadeInandScaleIn()
    {
        splashScreenSequence = DOTween.Sequence();
        if (scaleTween != null)
        {
            scaleTween.Kill(false);
        }
        float _duration = 1.0f;
        alphaValue = 0f;
        Vector2 newMaskHeight = new Vector2(mask.sizeDelta.x, 0);

        scaleTween = logoRect.DOScale(2, _duration);
        splashScreenSequence.Append(scaleTween.SetEase(Ease.OutBack));
        _duration = 5.0f;
        maskScaleTween = mask.DOSizeDelta(newMaskHeight, _duration);
        splashScreenSequence.Append(maskScaleTween);
        fadeTween = logo.DOFade(alphaValue, 0.15f);
        fadeTween.SetLoops(20, LoopType.Yoyo);
        splashScreenSequence.Join(fadeTween.SetEase(Ease.Flash));
        splashScreenSequence.AppendInterval(1.0f);
        alphaValue = 0f;
        splashScreenSequence.Append(logo.DOFade(alphaValue, 1f));
    }
}
   

