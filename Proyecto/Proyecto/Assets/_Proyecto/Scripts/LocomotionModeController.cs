using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction.Locomotion;

public class LocomotionModeController : MonoBehaviour
{
    private const int SlideMode = 0;
    private const int TeleportMode = 1;
    private const int BothMode = 2;
    private const int NoneMode = 3;

    [Header("Referencias")]
    [SerializeField] private TMP_Dropdown modeDropdown;
    [SerializeField] private MovingSetting movingSetting;

    private bool _listReleased;

    private void Awake()
    {
        if (modeDropdown == null || movingSetting == null)
        {
            Debug.LogError(
                "Asigna el desplegable de locomoción y el MovingSetting del rig en el Inspector.",
                this);
            return;
        }

        if (modeDropdown.captionText != null)
        {
            modeDropdown.captionText.enableWordWrapping = false;
            modeDropdown.captionText.overflowMode = TextOverflowModes.Overflow;
        }
        if (modeDropdown.itemText != null)
        {
            modeDropdown.itemText.enableWordWrapping = false;
            modeDropdown.itemText.overflowMode = TextOverflowModes.Overflow;
        }

        modeDropdown.onValueChanged.RemoveListener(SetMode);
        modeDropdown.onValueChanged.AddListener(SetMode);
        SetMode(modeDropdown.value);
    }

    private void OnDestroy()
    {
        if (modeDropdown != null) modeDropdown.onValueChanged.RemoveListener(SetMode);
    }

    private void LateUpdate()
    {
        if (modeDropdown == null) return;

        var list = modeDropdown.transform.Find("Dropdown List") as RectTransform;
        if (list == null)
        {
            _listReleased = false;
            return;
        }
        if (_listReleased) return;

        var canvas = modeDropdown.GetComponentInParent<Canvas>();
        if (canvas == null) return;

        // La plantilla nace dentro de UIBackplate, que tiene un Mask.
        // Ahí la lista se recorta y solo queda el asa del scroll. Se saca al canvas raíz.
        var root = canvas.rootCanvas.transform;
        list.SetParent(root, true);
        list.SetAsLastSibling();

        var layout = list.GetComponent<LayoutElement>();
        if (layout == null) layout = list.gameObject.AddComponent<LayoutElement>();
        layout.ignoreLayout = true;

        var graphics = list.GetComponentsInChildren<MaskableGraphic>(true);
        for (int i = 0; i < graphics.Length; i++)
            graphics[i].maskable = false;

        _listReleased = true;
    }

    public void SetMode(int mode)
    {
        if (mode < SlideMode || mode > NoneMode) return;
        if (movingSetting == null) return;

        bool enableSlide = mode == SlideMode || mode == BothMode;
        bool enableTeleport = mode == TeleportMode || mode == BothMode;
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var settingType = movingSetting.GetType();
        var slideField = settingType.GetField("_slideGameObjects", flags);
        var teleportField = settingType.GetField("_teleportGameObjects", flags);
        var slideObjects = slideField == null ? null : slideField.GetValue(movingSetting) as IList<GameObject>;
        var teleportObjects = teleportField == null ? null : teleportField.GetValue(movingSetting) as IList<GameObject>;

        if (slideObjects != null)
        {
            foreach (var go in slideObjects)
                if (go != null) go.SetActive(enableSlide || (teleportObjects != null && teleportObjects.Contains(go) && enableTeleport));
        }
        if (teleportObjects != null)
        {
            foreach (var go in teleportObjects)
                if (go != null) go.SetActive(enableTeleport || (slideObjects != null && slideObjects.Contains(go) && enableSlide));
        }

        if ((mode == SlideMode || mode == TeleportMode) && movingSetting.ControllerMovement != null)
        {
            movingSetting.ControllerMovement.Value = mode == SlideMode
                ? MovingSetting.MovementStyle.Slide
                : MovingSetting.MovementStyle.Teleport;
        }
    }
}
