using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    private bool usingShake = false;
    public GameObject player;

    [SerializeField]
    private float m_roughness;      // 거칠기 정도
    [SerializeField]
    private float m_magnitude;      // 움직임 범위

    void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
    public void Shaking()
    {
        usingShake = true;
        //usingShake = true;
        StartCoroutine(Shake(0.4f)); // 코루틴 실행
    }
    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-5f, 5f); // 5와 -5 사이의 랜덤 값 을 변수에 저장

        while (elapsed < duration) // elapsed가 duration보다 작을 동안 반복
        {
            elapsed += Time.deltaTime / halfDuration; // elapsed에 1프레임 동안 걸리는 시간을 더함 (+halfDuration으로 나누기)

            tick += Time.deltaTime * m_roughness; // tick에 1프레임 동안 걸리는 시간 곱하기 m_roughness 를 더함
            Vector3 position = transform.position; // 현재 위치를 포지션에 저장
            position.x += (Mathf.PerlinNoise(tick * 1.5f, 0) - 0.5f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration); // 부드럽게 이동
            transform.position = position; // 계산된 포지션으로 이동

            yield return null; // 중단
        }
        Debug.Log("쉐이킹 끝");
        usingShake = false;
    }
}