using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    private bool usingShake = false;
    public GameObject player;

    [SerializeField]
    private float m_roughness;      // ��ĥ�� ����
    [SerializeField]
    private float m_magnitude;      // ������ ����

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
        StartCoroutine(Shake(0.4f)); // �ڷ�ƾ ����
    }
    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-5f, 5f); // 5�� -5 ������ ���� �� �� ������ ����

        while (elapsed < duration) // elapsed�� duration���� ���� ���� �ݺ�
        {
            elapsed += Time.deltaTime / halfDuration; // elapsed�� 1������ ���� �ɸ��� �ð��� ���� (+halfDuration���� ������)

            tick += Time.deltaTime * m_roughness; // tick�� 1������ ���� �ɸ��� �ð� ���ϱ� m_roughness �� ����
            Vector3 position = transform.position; // ���� ��ġ�� �����ǿ� ����
            position.x += (Mathf.PerlinNoise(tick * 1.5f, 0) - 0.5f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration); // �ε巴�� �̵�
            transform.position = position; // ���� ���������� �̵�

            yield return null; // �ߴ�
        }
        Debug.Log("����ŷ ��");
        usingShake = false;
    }
}