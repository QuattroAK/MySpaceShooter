using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class PulseButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform pulsed;
    [SerializeField] private float pulseScale = 0.8f;
    [SerializeField] private float pulseTime = 0.25f;

    private Sequence sequence = null;

    private void Awake()
    {
        if (pulsed == null)
        {
            pulsed = transform;
        }
    }

    private void Pulse()
    {
        if (this.pulsed == null) return;
        if (this.sequence != null && !this.sequence.IsComplete()) this.sequence.Kill();
        sequence = DOTween.Sequence();
        sequence
            .Append(this.pulsed.DOScale(this.pulseScale, this.pulseTime / 2f).SetEase(Ease.OutQuad))
            .Append(this.pulsed.DOScale(1f, this.pulseTime / 2f).SetEase(Ease.OutElastic))
            .AppendCallback(() => { this.sequence.Kill(); this.sequence = null; });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pulse();
    }

    private void OnDestroy()
    {
        if (sequence != null && sequence.IsActive())
            sequence.Kill();
    }
}