using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{
    [Header ("[따라다닐 타겟]")]
    public GameObject player;
    [Header ("[따라다님 리커버 강도 [높을수록 빠르게 따라다님]]")]
    public float recoverStrong = 7.5f;
    [Header("[전방 과 캐릭터 거리 [높을수록 거리 벌림]]")]
    public float distance = 5.5f;
    [Header ("[전방보며 활성화]")]
    public bool lookAhead = true;
    [Header("[전방을 내다보며 따라다닐 타겟]")]
    public GameObject reverseTarget;
    [Header ("[캠의 방향으로 조이스틱 조정하기 위해 사용]")]
    public Transform cam;

    //캐슁을 위해 
    private Vector3 camForward;
    private float h;
    private float v;
    private Vector3 pos, desiredVelocity;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        //메모리 절약용 렌더 캐슁
        skinnedMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        //해당 오브젝트가 비활성화 상태라면 움직이지 않음 [비용절약용 코드]
        if (!skinnedMeshRenderer.isVisible) return;

        if(!lookAhead) {//기본 따라다니기
            //실제 러프를 거는 부분
            transform.localPosition = Vector3.Lerp(transform.localPosition, player.transform.localPosition, Time.deltaTime * recoverStrong);
        } else {//전방을 내다 보며 따라다니기
            h = UltimateJoystick.GetHorizontalAxis("Move");
            v = UltimateJoystick.GetVerticalAxis("Move");

            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            desiredVelocity = v * camForward + h * cam.right;

            pos = player.transform.localPosition;
            pos.z += desiredVelocity.z * distance;
            pos.x += desiredVelocity.x * distance;

            //러프를 걸 타겟
            reverseTarget.transform.localPosition = pos;

            //실제 러프를 거는 부분
            transform.localPosition = Vector3.Lerp(transform.localPosition, reverseTarget.transform.localPosition, Time.deltaTime * recoverStrong);
            //Debug.Log("h = [" + h + "] v = [" + v + "]" + "  pos = " + pos);
            //Debug.Log("this = " + transform.localPosition + " reverseTarget = " + reverseTarget.transform.localPosition);
        }
    }
}
