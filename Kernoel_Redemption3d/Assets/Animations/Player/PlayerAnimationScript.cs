using UnityEngine;

namespace Animations.Player
{
    public class PlayerAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private CharacterMovement _characterMovement;
        private static readonly int _playerIsMoving = Animator.StringToHash("player_is_moving");
       

        private void Start()
        {
            _characterMovement = transform.parent.GetComponent<CharacterMovement>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_characterMovement)
            {
                _animator.SetBool(_playerIsMoving, _characterMovement.playerIsMoving);    
            }
            else
            {
                _animator.SetBool(_playerIsMoving, true);
            }
            
            
           
        }
    }
}
