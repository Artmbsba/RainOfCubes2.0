using TMPro;
using UnityEngine;

public class ViewSpawner : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _allCreated;
    [SerializeField] protected TextMeshProUGUI _activeCount;
    [SerializeField] protected TextMeshProUGUI _createdCount;

    public virtual void UpdateAllCreated(int count)
    {
        _allCreated.text = $"Number of logged objects for all time: {count}";
    }

    public virtual void UpdateActiveCount(int count)
    {
        _activeCount.text = $"Number of active objects on the stage: {count}";
    }

    public virtual void UpdateCreatedCount(int count)
    {
        _createdCount.text = $"Number of created objects: {count}";
    }
}
