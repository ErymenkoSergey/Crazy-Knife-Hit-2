////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;
////using Unity.Notifications.Android;

//public class NotificationManager : MonoBehaviour
//{
//    //void Start()
//    //{
//    //    CreateNotificationChannel();
//    //}

//    public void CreateNotificationChannel()
//    {
//        var channel = new AndroidNotificationChannel()
//        {
//            Id = "channel_id",
//            Name = "Default Channel",
//            Importance = Importance.High,
//            Description = "Generic notifications",
//        };

//        AndroidNotificationCenter.RegisterNotificationChannel(channel);
//    }

//    public void SendNotification()
//    {
//        var notification = new AndroidNotification();
//        notification.Title = "Crazy Knife Hit 2!";
//        notification.Text = "Твои бонусы готовы:) Зайди в игру, что бы их собрать!)";
//        notification.LargeIcon = "icon_0";
//        notification.FireTime = System.DateTime.Now.AddSeconds(10);// AddHourse, AddMinuts...

//        AndroidNotificationCenter.SendNotification(notification, "channel_id");
//    }
//}
