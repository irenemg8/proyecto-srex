using UnityEngine;
using UnityEngine.UI;

public class SpatialEffectsController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Toggle effectsToggle;
    [SerializeField] private AudioSource bathroomAudio;
    [SerializeField] private AudioSource panAudio;
    [SerializeField] private AudioSource fryingAudio;

    private void Awake()
    {
        if (effectsToggle == null || bathroomAudio == null ||
            panAudio == null || fryingAudio == null)
        {
            Debug.LogError(
                "Asigna el toggle y las tres fuentes de efectos espaciales en el Inspector.",
                this);
            return;
        }

        effectsToggle.onValueChanged.RemoveListener(SetSpatialEffects);
        effectsToggle.onValueChanged.AddListener(SetSpatialEffects);
        SetSpatialEffects(effectsToggle.isOn);
    }

    private void OnDestroy()
    {
        if (effectsToggle != null) effectsToggle.onValueChanged.RemoveListener(SetSpatialEffects);
    }

    public void SetSpatialEffects(bool enabled)
    {
        SetMuted(bathroomAudio, !enabled);
        SetMuted(panAudio, !enabled);
        SetMuted(fryingAudio, !enabled);
    }

    private static void SetMuted(AudioSource source, bool muted)
    {
        if (source != null) source.mute = muted;
    }

}
