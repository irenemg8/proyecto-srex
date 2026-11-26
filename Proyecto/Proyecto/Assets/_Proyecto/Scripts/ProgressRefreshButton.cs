using UnityEngine;
using UnityEngine.UI;

public class ProgressRefreshButton : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Toggle refreshToggle;
    [SerializeField] private FruitProgressController progress;

    private void Awake()
    {
        if (refreshToggle == null || progress == null)
        {
            Debug.LogError(
                "Asigna el toggle de actualización y el FruitProgressController en el Inspector.",
                this);
            return;
        }

        refreshToggle.onValueChanged.RemoveListener(RefreshProgress);
        refreshToggle.onValueChanged.AddListener(RefreshProgress);
    }

    private void OnDestroy()
    {
        if (refreshToggle != null) refreshToggle.onValueChanged.RemoveListener(RefreshProgress);
    }

    private void RefreshProgress(bool _)
    {
        if (progress != null) progress.RefreshProgressUI();
        if (refreshToggle != null && refreshToggle.isOn) refreshToggle.SetIsOnWithoutNotify(false);
    }
}
