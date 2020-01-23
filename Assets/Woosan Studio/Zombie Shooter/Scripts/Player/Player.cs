using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Player : MonoBehaviour
    {
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;
        //인풋 액션
        IInputActions _inputActions;
        //리로드 액션이 있을때 연결되는 부분
        IReloadEventSocket _reloadEventSocket;

        //ICameraShaker를 가져오기 위한 용도
        public GameObject cameras;
        //카메라 쉐이커
        ICameraShaker _cameraShaker;

        //캐슁용
        IWeapon _weapon;
        

        private void Awake()
        {
            _cameraShaker = cameras.GetComponent<ICameraShaker>();
            _inputActions = GetComponent<IInputActions>();
            _reloadEventSocket = GetComponent<IReloadEventSocket>();
        }

        IEnumerator Start()
        {
            _weaponFactory = FindObjectOfType<WeaponFactory>();

            yield return new WaitForSeconds(0.2f);

            //키인풋으로 사격 컨트롤
            _weapon = _weaponFactory.MakeWeapon(_inputActions,_cameraShaker, _reloadEventSocket, this.transform, 0);

            yield return new WaitForSeconds(0.1f);
        }

        public void PlayAnimation(int number)
        {

        }

        public void AttackStart()
        {
            _weapon.Attack();
        }

        public void AttackStop()
        {
            _weapon.Stop();
        }
    }
}
