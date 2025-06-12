using TMPro;
using UnityEngine;

public class DronesCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private DronesCount _dronesCount;

    private void Awake()
    {
        _dronesCount.ChangeCount += OnChangeCount;
    }

    public void OnChangeCount(float count)
    {
        _text.text = count.ToString();
    }

    private void OnDestroy()
    {
        _dronesCount.ChangeCount -= OnChangeCount;
    }
}
