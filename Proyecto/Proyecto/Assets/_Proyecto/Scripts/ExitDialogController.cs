using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitDialogController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private FruitProgressController progress;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject dialogRoot;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button cancelButton;

    [Header("Ajustes")]
    [SerializeField] private float secondsToReopen = 10f;

    private Coroutine _reopenCoroutine;

    private void Awake()
    {
        if (progress == null || statusText == null || dialogRoot == null ||
            exitButton == null || cancelButton == null)
        {
            Debug.LogError(
                "Asigna el progreso, el texto de estado, el panel y los dos botones en el Inspector.",
                this);
        }

        if (exitButton != null && exitButton.onClick.GetPersistentEventCount() == 0)
        {
            exitButton.onClick.RemoveListener(ExitApplication);
            exitButton.onClick.AddListener(ExitApplication);
        }

        if (cancelButton != null && cancelButton.onClick.GetPersistentEventCount() == 0)
        {
            cancelButton.onClick.RemoveListener(CancelDialog);
            cancelButton.onClick.AddListener(CancelDialog);
        }
    }

    private void OnEnable()
    {
        if (progress != null)
        {
            progress.ProgressChanged -= UpdateStatus;
            progress.ProgressChanged += UpdateStatus;
            UpdateStatus(progress.IsComplete);
        }
    }

    private void OnDisable()
    {
        if (progress != null) progress.ProgressChanged -= UpdateStatus;
    }

    private void UpdateStatus(bool complete)
    {
        if (statusText != null)
            statusText.text = complete ? "¡Ya has terminado!" : "Todavía no has terminado";
    }

    private void CancelDialog()
    {
        if (dialogRoot != null) dialogRoot.SetActive(false);

        // Vuelve a mostrar el mensaje pasados X segundos para que
        // el usuario siempre pueda salir del juego.
        if (_reopenCoroutine != null) StopCoroutine(_reopenCoroutine);
        _reopenCoroutine = StartCoroutine(ReopenAfterDelay());
    }

    private IEnumerator ReopenAfterDelay()
    {
        yield return new WaitForSeconds(secondsToReopen);
        if (dialogRoot != null) dialogRoot.SetActive(true);
        _reopenCoroutine = null;
    }

    private void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
