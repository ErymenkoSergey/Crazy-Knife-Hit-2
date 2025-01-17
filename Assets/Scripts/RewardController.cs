using System.Collections;
using UnityEngine;
using System;

public class RewardController : MonoBehaviour
{
    public static RewardController Instance;

    [SerializeField] private int hoursTorReward;
    [SerializeField] private int minutesTorReward;
    [SerializeField] private int secondsTorReward = 10;

    public int minReward = 50;
    public int maxReward = 90; // ���������� ����� �� � ��.. �������� ���!

    private const string NEXT_REWARD = "RewardTime";

    public DateTime NextRewardTime => GetNextRewardTime();
    public TimeSpan TimeToReward => NextRewardTime.Subtract(DateTime.Now);

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool CanRewardNow()
    {
        return NextRewardTime <= DateTime.Now;
    }

    public int GetRandomReward()
    {
        return UnityEngine.Random.Range(minReward, maxReward);
    }

    public void ResetRewardTime()
    {
        DateTime nextReward = DateTime.Now.Add(new TimeSpan(hoursTorReward, minutesTorReward, secondsTorReward));
        SaveNextRewardTime(nextReward);
    }

    private void SaveNextRewardTime(DateTime time) // ����������
    {
        PlayerPrefs.SetString(NEXT_REWARD, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private DateTime GetNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(NEXT_REWARD, string.Empty);
        if (!string.IsNullOrEmpty(nextReward))
        {
            return DateTime.FromBinary(Convert.ToInt64(nextReward));
        }
        else
        {
            return DateTime.Now;
        }
    }
}
