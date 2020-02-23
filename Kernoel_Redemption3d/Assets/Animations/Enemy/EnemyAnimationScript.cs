using UnityEngine;

namespace Animations.Enemy
{
    public class EnemyAnimationScript : MonoBehaviour
    {
        private Animator _animator;
        private EnemyScript _enemyScript;
        private static readonly int _isShooting = Animator.StringToHash("isShooting");

        private void Start()
        {
            _enemyScript = transform.parent.GetComponent<EnemyScript>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(_isShooting, _enemyScript.isShooting == true);
        }
    }
}
