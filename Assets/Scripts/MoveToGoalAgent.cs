using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private ResetPositions reset;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Player player;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;

    private float punishTime = 3.0f;
    private Vector3 lastPosition;
    private float stationaryTime;
    public override void OnEpisodeBegin()
    {
        lastPosition = transform.localPosition;
        stationaryTime = 0f;
        Debug.Log("beginepisodereset");
        reset.Reset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);   ///Player ve hedefin pozisyonlarını gözlüyoruz.
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        movement.setZ(actions.DiscreteActions[0]);  ///agent'ın int değerinde aksiyon almasını sağlayan değerler. DiscreteActions[0] 0,1,2 diğer ikisi 0,1 arasında değer alıyor.
        movement.setX(actions.DiscreteActions[1]);  ///0 ve 1 değerini alanların biri hızlanma, biri de sağ tuş için kullanılıyor. 0,1,2 değerini alan zıplama için.
        movement.setRight(actions.DiscreteActions[2]);  ///Çünkü zıplama tuşuna basıldıkça karakter daha yükseğe zıplıyor ve bu, aksiyonun 0'dan büyük olduğu değerlerde gerçekleşiyor.
                                                        ///Yani 1 ve 2 değerinin gelme olasılığı 0'dan fazla olduğu için, tuşa 2 frame basılı tutulmuş gibi oluyor.

        float speed = rb.velocity.magnitude;

        float speedReward = speed * 0.1f; ///Hızlı gitmesine de biraz ödül vermek gerekiyor çünkü Mario hızlı giderken daha yükseğe zıplıyor.
        AddReward(speedReward);

        if (transform.localPosition.x == lastPosition.x && transform.localPosition.x != 2.5f)
        {
            stationaryTime += Time.fixedDeltaTime;   /////Yatay eksende bekleme süresini arttır.

            // Eğer aynı yerde çok uzun süre beklediyse:
            if (stationaryTime >= punishTime)
            {
                // Agent'a ceza puanı ver, bekleme süresini sıfırla ve karakterlerin pozisyonunu ve hızını sıfırla.
                AddReward(-1.0f);
                stationaryTime = 0f;
                Debug.Log("stationaryReset");
                reset.Reset();
            }
        }
        else
        {
            //Eğer karakter hareket ettiyse beklediği süreyi sıfırlıyoruz.
            stationaryTime = 0f;
        }

        float deltaX = transform.localPosition.x - lastPosition.x;   ///Sağa gittiği mesafe kadar agent'ı ödüllendiriyoruz.

        if (deltaX > 0)
        {
            AddReward(deltaX * 1000);
        }

        // Karakterin son pozisyonunu update ediyoruz.
        lastPosition = transform.localPosition;

        if (transform.localPosition.x < 2.5f) ///Bir bug'ı önlemek için.
        {
            AddReward(-1f);
            EndEpisode();
            Debug.Log("localpositionreset");
            reset.Reset();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal") ////Hedefe ulaşıldığında agent'a çok yüksek bir puan veriyoruz.
        {
            AddReward(10000f);
            Debug.Log("goalreset");
            EndEpisode();
            reset.Reset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")    ///Düşmana temas ettiğinde cezalandırıp bölümü ve nesli yeniden başlatıyoruz.
        {
            //Debug.Log
            Debug.Log("enemyreset");
            AddReward(-1f);
            EndEpisode();
            reset.Reset();
        }
    }
}
