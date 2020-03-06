using UnityEngine;

namespace Animations.Zombie
{
    public class ZombieAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private MoveCharacterToPlayer _moveZombieToPlayer;
        private static readonly int _isWalking = Animator.StringToHash("Zwalk");

        private void Start()
        {
            _moveZombieToPlayer = transform.parent.GetComponent<MoveCharacterToPlayer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_moveZombieToPlayer) _animator.SetBool(_isWalking, _moveZombieToPlayer.IsMoving());
        }
    }
}
