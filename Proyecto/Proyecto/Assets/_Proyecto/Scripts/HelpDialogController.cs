using UnityEngine;
using UnityEngine.UI;

public class HelpDialogController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Toggle helpToggle;
    [SerializeField] private GameObject helpRoot;

    private void Awake()
    {
        if (helpToggle == null || helpRoot == null)
        {
            Debug.LogError(
                "Asigna el toggle de ayuda y el panel de ayuda en el Inspector.",
                this);
            return;
        }

        helpToggle.onValueChanged.RemoveListener(SetHelpVisible);
        helpToggle.onValueChanged.AddListener(SetHelpVisible);
        SetHelpVisible(helpToggle.isOn);
    }

    private void OnDestroy()
    {
        if (helpToggle != null) helpToggle.onValueChanged.RemoveListener(SetHelpVisible);
    }

    public void SetHelpVisible(bool visible)
    {
        if (helpRoot != null) helpRoot.SetActive(visible);
    }
}
