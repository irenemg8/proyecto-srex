using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum FruitKind
{
    Banana,
    Orange,
    Pineapple
}

public class FruitProgressController : MonoBehaviour
{
    private const int BananaGoal = 2;
    private const int OrangeGoal = 2;
    private const int PineappleGoal = 1;
    private const int EmptyCount = 0;

    [Header("Paneles de ingredientes")]
    [SerializeField] private GameObject bananaPanel;
    [SerializeField] private GameObject orangePanel;
    [SerializeField] private GameObject pineapplePanel;

    [Header("Pista de ingredientes faltantes")]
    [SerializeField] private GameObject hintPanel;

    private int bananas;
    private int oranges;
    private int pineapples;

    private TMP_Text bananaText;
    private TMP_Text orangeText;
    private TMP_Text pineappleText;
    private GameObject bananaMissing;
    private GameObject bananaComplete;
    private GameObject orangeMissing;
    private GameObject orangeComplete;
    private GameObject pineappleMissing;
    private GameObject pineappleComplete;

    public event Action<bool> ProgressChanged;

    public bool IsComplete
    {
        get
        {
            return bananas >= BananaGoal &&
                   oranges >= OrangeGoal &&
                   pineapples >= PineappleGoal;
        }
    }

    private void Awake()
    {
        CacheCounterUIReferences();
        if (hintPanel == null) hintPanel = FindHintPanel();
        RefreshProgressUI();
    }

    private void CacheCounterUIReferences()
    {
        if (bananaPanel == null || orangePanel == null || pineapplePanel == null)
        {
            Debug.LogError(
                "Asigna los paneles de plátano, naranja y piña en el Inspector.",
                this);
        }

        bananaText = FindChildText(bananaPanel, "Text_Cantidad");
        orangeText = FindChildText(orangePanel, "Text_Cantidad");
        pineappleText = FindChildText(pineapplePanel, "Text_Cantidad");

        bananaMissing = FindChild(bananaPanel, "Icono_Falta");
        bananaComplete = FindChild(bananaPanel, "Icono_Completo");
        orangeMissing = FindChild(orangePanel, "Icono_Falta");
        orangeComplete = FindChild(orangePanel, "Icono_Completo");
        pineappleMissing = FindChild(pineapplePanel, "Icono_Falta");
        pineappleComplete = FindChild(pineapplePanel, "Icono_Completo");
    }

    private static GameObject FindChild(GameObject parent, string childName)
    {
        if (parent == null) return null;
        var child = parent.transform.Find(childName);
        return child == null ? null : child.gameObject;
    }

    private static TMP_Text FindChildText(GameObject parent, string childName)
    {
        var child = FindChild(parent, childName);
        return child == null ? null : child.GetComponent<TMP_Text>();
    }

    public bool TryRegisterFruit(FruitKind kind)
    {
        switch (kind)
        {
            case FruitKind.Banana:
                if (bananas >= BananaGoal) return false;
                bananas++;
                break;
            case FruitKind.Orange:
                if (oranges >= OrangeGoal) return false;
                oranges++;
                break;
            case FruitKind.Pineapple:
                if (pineapples >= PineappleGoal) return false;
                pineapples++;
                break;
            default:
                return false;
        }

        RefreshProgressUI();
        return true;
    }

    public void RefreshProgressUI()
    {
        SetCounter(bananaText, bananaMissing, bananaComplete, bananas, BananaGoal);
        SetCounter(orangeText, orangeMissing, orangeComplete, oranges, OrangeGoal);
        SetCounter(pineappleText, pineappleMissing, pineappleComplete, pineapples, PineappleGoal);

        // Ocultar la pista "Añade 1 alimento más" cuando ya se completaron todos los ingredientes.
        if (hintPanel != null) hintPanel.SetActive(!IsComplete);

        var callback = ProgressChanged;
        if (callback != null) callback(IsComplete);
    }

    /// <summary>
    /// Busca automáticamente el panel de pista "Añade 1 alimento más" si no se asignó en el Inspector.
    /// Sube desde el texto hasta encontrar el objeto del diálogo (fondo Image + botón Toggle "Recargar marcador").
    /// </summary>
    private GameObject FindHintPanel()
    {
        foreach (var text in Resources.FindObjectsOfTypeAll<TMP_Text>())
        {
            if (!text.gameObject.scene.IsValid()) continue;
            if (!text.text.Contains("alimento más")) continue;

            var t = text.transform;
            while (t != null && t.parent != null)
            {
                t = t.parent;
                // El diálogo tiene un fondo (Image) y el botón "Recargar marcador" es un Toggle hijo.
                if (t.GetComponent<Image>() != null && t.GetComponentInChildren<Toggle>() != null)
                    return t.gameObject;
            }
        }
        return null;
    }

    private static void SetCounter(TMP_Text text, GameObject missing, GameObject complete, int count, int goal)
    {
        if (text != null) text.text = count == EmptyCount ? "X" : count + "/" + goal;
        bool isComplete = count >= goal;
        if (missing != null) missing.SetActive(!isComplete);
        if (complete != null) complete.SetActive(isComplete);
    }
}
