using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int _price;

    public Rigidbody2D myRigidbody2D;
    public bool IsReleased { get; set; }

    public int PriceKnife => _price;

    //[SerializeField] private ParticleSystem wheelParticle; // �������� ��� ���������� ������
    public bool Hit { get; set; }
    private Animator camAnim; // �������� ������ - ������

    private void Start()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>(); // ���� �� ���� ������
    }
    public void FireKnife()
    {
        if (!IsReleased)
        {
            IsReleased = true;
            myRigidbody2D.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
            SoundManager.Instance.PlayKnifeFire(); // ���� ��������
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wheel") && !Hit && !GameManager.Instance.IsGameOver && IsReleased)
        {
            //wheelParticle.Play(); // �������� �������� ��� ������ ��� ���������
            other.gameObject.GetComponent<Wheel>().KnifeHit(this);
            SoundManager.Instance.Vibrate(); // ��������� ��������)
            SoundManager.Instance.PlayWheelHit();
            camAnim.SetTrigger("Shake");
        }
        else if (other.gameObject.CompareTag("Knife") && !Hit && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().IsReleased)
        {
            Hit = true;
            transform.SetParent(other.transform);

            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.isKinematic = true;

            SoundManager.Instance.PlayGameOver();
            SoundManager.Instance.Vibrate();
            camAnim.SetTrigger("Shake");

            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), 0.1f);
        }
    }

    private void GameOver()
    {
        UIManager.Instance.GameOver();
        SoundManager.Instance.Vibrate();
    }
}
