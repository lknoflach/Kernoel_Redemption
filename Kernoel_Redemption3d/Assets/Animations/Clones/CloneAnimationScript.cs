using UnityEngine;

namespace Animations.Clones
{
    public class CloneAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private MoveCharacterToPlayer _moveCloneToPlayer;
        private static readonly int _isWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _moveCloneToPlayer = transform.parent.GetComponent<MoveCharacterToPlayer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_moveCloneToPlayer) _animator.SetBool(_isWalking,_moveCloneToPlayer.IsMoving());
        }
    }
}
