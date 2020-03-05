using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    public bool isBarrierActive = true;
    private GameObject _barrier;
    
    private void Start()
    {
        _barrier = transform.Find("Barrier").gameObject;
        
        if (_barrier) _barrier.SetActive(isBarrierActive);
    }
}