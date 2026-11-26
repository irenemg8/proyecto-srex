using UnityEngine;
using UnityEngine.UI;

public class AmbientVolumeController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource ambientAudio;

    private void Awake()
    {
        if (volumeSlider == null || ambientAudio == null)
        {
            Debug.LogError(
                "Asigna el deslizador y la fuente de audio ambiental en el Inspector.",
                this);
            return;
        }

        volumeSlider.onValueChanged.RemoveListener(SetAmbientVolume);
        volumeSlider.onValueChanged.AddListener(SetAmbientVolume);
        SetAmbientVolume(volumeSlider.value);
    }

    private void OnDestroy()
    {
        if (volumeSlider != null) volumeSlider.onValueChanged.RemoveListener(SetAmbientVolume);
    }

    public void SetAmbientVolume(float value)
    {
        if (ambientAudio != null) ambientAudio.volume = Mathf.Clamp01(value);
    }
}
