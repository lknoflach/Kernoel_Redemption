using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    public GameObject player;
    private CharacterMovement _characterMovement;
    public float totalSpeed = 12f;
    private CloningScript _cloningScript;

    [Header("Unity Stuff")] public Image speedBar;

    private void Start()
    {
        _characterMovement = player.GetComponent<CharacterMovement>();
        _cloningScript = player.GetComponent<CloningScript>();
        if (speedBar) speedBar.fillAmount = ((float) _characterMovement.speed) / ((float) totalSpeed);
    }

    public void UpgradeSpeed()
    {
        if (_characterMovement.speed < totalSpeed && _cloningScript.lowGradeSeedOil >= 3)
        {
            _cloningScript.lowGradeSeedOil = _cloningScript.lowGradeSeedOil - 3;
            _characterMovement.speed = _characterMovement.speed + 1f;
            if (speedBar) speedBar.fillAmount = ((float) _characterMovement.speed) / ((float) totalSpeed);
        }
    }
}