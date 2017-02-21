﻿using BiliBili3.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace BiliBili3.Models
{
    //番剧信息
    public class BangumiInfoModel : INotifyPropertyChanged
    {
        public int code { get; set; }
        public string message { get; set; }
        public BangumiInfoModel result { get; set; }

        public string alias { get; set; }//别名
        public string area { get; set; }//地区
        public string bangumi_title { get; set; }//番剧名。与season_title不同
        public string season_title { get; set; }//专题名
        public string title { get; set; }
        public string evaluate { get; set; }//介绍
        public int favorites { get; set; }//订阅
        public int coins { get; set; }//硬币
        public int play_count { get; set; }
        public int danmaku_count { get; set; }
        public int is_finish { get; set; }//是否完结
        public string newest_ep_index { get; set; }//最新话
        public string staff { get; set; }

        public string cover { get; set; }
        public DateTime? pub_time
        {
            get;
            set;
        }

        public BangumiInfoModel user_season { get; set; }
        public int attention { get; set; }//是否关注

        public int Num { get; set; }
        public List<tagsModel> tags { get; set; }
       
       

        public List<actorModel> actor { get; set; }
        public string role { get; set; }
       public List<episodesModel> episodes { get; set; }
       


        public int total_count { get; set; }
        public string status
        {
            get
            {
                if (is_finish == 1)
                {
                    return string.Format("已完结，共{0}话", total_count);
                }
                else
                {
                    return string.Format("连载中，更新至{0}话", newest_ep_index) ?? "";
                }
            }
        }

        public string upTime
        {
            get
            {
                if (pub_time.HasValue)
                {

                    return pub_time.Value.Date.ToString("d");
                }
                else
                {
                    return "";
                }
            }

        }

        public string PlayCount
        {
            get
            {
                if (play_count > 10000)
                {
                    return ((double)play_count / 10000).ToString("0.0" + "万");
                }
                else
                {
                    return play_count.ToString();
                }
            }
        }

        public string favoritesCount
        {
            get
            {
                if (favorites > 10000)
                {
                    return ((double)favorites / 10000).ToString("0.0" + "万");
                }
                else
                {
                    return favorites.ToString();
                }
            }
        }

        public string cv
        {
            get
            {
                string a = "";
                if (actor != null&&actor.Count!=0)
                {
                    actor.ForEach(x => a += x.role + " : " + x.actor + "\r\n");
                }
                return a;
            }
        }
        public object rank { get; set; }
        public object list { get; set; }
        public List<BangumiInfoModel> seasons { get; set; }
        public string bangumi_id { get; set; }
        public string season_id { get; set; }
        //public string title { get; set; }
        public int total_bp_count { get; set; }
        public int week_bp_count { get; set; }
     

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
    public class actorModel
    {
        public string actor { get; set; }
        public string role { get; set; }
    }
    public class tagsModel
    {
        public string index { get; set; }
        public int tag_id { get; set; }
        public string tag_name { get; set; }
    }
    public class episodesModel
    {
        public string av_id { get; set; }
        public int danmaku { get; set; }
        public string index_title { get; set; }
        public bool inLocal { get; set; }
        public string index { get; set; }
        public int orderindex { get; set; }
        public int episode_status { get; set; }
        public string episode_id { get; set; }
        public string badge { get; set; }
        public Visibility IsBadge
        {
            get
            {
                if (badge!=null&&badge.Length!=0)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }
        public Visibility IsDowned
        {
            get
            {
                if (DownloadHelper.downMids.ContainsKey(danmaku.ToString()))
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }
        public SolidColorBrush f
        {
            get
            {
                if (SqlHelper.GetPostIsViewPost(danmaku.ToString()))
                {
                    return new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }
            }
        }
    }

    public class CBRankModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public object result { get; set; }
        public object list { get; set; }
        public string face { get; set; }
        public string hidden { get; set; }//0 false,1 true
        public string rank { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
    }
    public class TokenModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public object result { get; set; }
        public string token { get; set; }
    }
    public class OrderModel
    {
        public int code { get; set; }
        public string data { get; set; }
        public object result { get; set; }
        public string cashier_url { get; set; }
        public string order_id { get; set; }
        public string pay_pay_order_no { get; set; }
        public string qrcode { get; set; }
    }
}
