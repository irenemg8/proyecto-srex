using System.Collections.Generic;
using UnityEngine;

// NOTA: este componente debe quedarse en el objeto SartenSensor (o el objeto que
// tenga el trigger collider), porque OnTriggerEnter solo se dispara en los
// componentes que cuelgan del GameObject con el collider. El resto de la logica
// no depende de la posicion en la jerarquia.
public class SartenController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private FruitProgressController progress;
    [SerializeField] private AudioSource fryingAudio;

    private readonly HashSet<int> acceptedFruit = new HashSet<int>();

    private void Awake()
    {
        if (progress == null || fryingAudio == null)
        {
            Debug.LogError(
                "Asigna FruitProgressController y el AudioSource de fritura en el Inspector.",
                this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || progress == null) return;

        var body = other.attachedRigidbody;
        var fruit = body != null ? body.gameObject : other.gameObject;
        int id = fruit.GetInstanceID();
        if (acceptedFruit.Contains(id)) return;

        FruitKind kind;
        if (fruit.CompareTag("Platano")) kind = FruitKind.Banana;
        else if (fruit.CompareTag("Naranja")) kind = FruitKind.Orange;
        else if (fruit.CompareTag("Pina")) kind = FruitKind.Pineapple;
        else return;

        if (!progress.TryRegisterFruit(kind)) return;

        acceptedFruit.Add(id);
        if (fryingAudio != null && fryingAudio.clip != null)
            fryingAudio.PlayOneShot(fryingAudio.clip);

        Destroy(fruit);
    }
}
