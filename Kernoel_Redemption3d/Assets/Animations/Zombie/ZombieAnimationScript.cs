using UnityEngine;

namespace Animations.Zombie
{
    public class ZombieAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private ZombieScript _zombieScript;
        private static readonly int _isWalking = Animator.StringToHash("Zwalk");

        private void Start()
        {
            _zombieScript = transform.parent.GetComponent<ZombieScript>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(_isWalking, _zombieScript.isMoving != false);
        }
    }
}
