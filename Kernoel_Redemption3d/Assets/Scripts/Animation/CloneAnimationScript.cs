using UnityEngine;

namespace Animation
{
    public class CloneAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private CloneScript _cloneScript;
        private static readonly int _isWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _cloneScript = transform.parent.GetComponent<CloneScript>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(_isWalking, _cloneScript.isArrivedAtPlayer == false);
        }
    }
}