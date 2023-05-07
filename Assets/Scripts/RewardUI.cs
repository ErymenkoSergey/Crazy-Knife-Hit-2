using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
/// <summary>
/// В этом скрипте настройка пуш-уведомлений, и времени для бонусов.
/// </summary>

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private ParticleSystem applePS;
    [SerializeField] private Text rewardText;

    void Start()
    {
        CreateNotificationChannel();
    }

    private void Update()
    {
        if (RewardController.Instance.CanRewardNow())
        {
            rewardText.text = "Get Reward!";
        }
        else
        {
            TimeSpan timeToReward = RewardController.Instance.TimeToReward;
            rewardText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
    }

    public void RewardPlayer()
    {
        if (RewardController.Instance.CanRewardNow())
        {
            int amount = RewardController.Instance.GetRandomReward();
            StartCoroutine(RewardCR());
            RewardController.Instance.ResetRewardTime();
            //SoundManager.Instance.PlayAppleReward();
            GameManager.Instance.TotalApple += amount;
        }
    }

    private IEnumerator RewardCR()
    {
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(0.3f); //1.6
        SoundManager.Instance.PlayAppleReward();

        Instantiate(applePS);
        yield return new WaitForSeconds(3f);
        rewardPanel.SetActive(false);
    }

    public void CreateNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Crazy Knife Hit 2!";
        notification.Text = "Твои бонусы готовы:) Зайди в игру, чтобы их собрать!)";
        notification.LargeIcon = "icon_0"; // id icon for settings notification!
        notification.FireTime = System.DateTime.Now.AddHours(1);// AddHours, AddMinuts... AddSeconds(10);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
