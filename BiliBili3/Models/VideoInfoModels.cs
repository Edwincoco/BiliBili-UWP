﻿using BiliBili3.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace BiliBili3.Models
{
    class VideoInfoModels
    {
        public int code { get; set; }
        public string message { get; set; }
        public VideoInfoModels data { get; set; }
        public string aid { get; set; }
        public string attribute { get; set; }
        public int copyright { get; set; }
        public long? ctime { get; set; }
        public string desc { get; set; }
        public long? duration { get; set; }//长度，秒
        public string pic { get; set; }
        public long? pubdate { get; set; }
        public int state { get; set; }
        public string title { get; set; }
        public int tid { get; set; }
        public string tname { get; set; }

        public ownerModel owner { get; set; }
        public owner_extModel owner_ext { get; set; }
        public List<pagesModel> pages { get; set; }
        public List<relatesModel> relates { get; set; }
        public req_userModel req_user { get; set; }
        public rightsModel rights { get; set; }
        public seasonModel season { get; set; }
        public List<tagModel> tag { get; set; }
        public statModel stat { get; set; }

        public MovieModel movie { get; set; }

        public elecModel elec { get; set; }


        public string Created_at
        {
            get
            {
                DateTime dtStart = new DateTime(1970, 1, 1);
                long lTime = long.Parse(pubdate + "0000000");
                //long lTime = long.Parse(textBox1.Text);
                TimeSpan toNow = new TimeSpan(lTime);

                DateTime dt = dtStart.Add(toNow);
                if (dt.Date == DateTime.Now.Date)
                {
                    TimeSpan ts = DateTime.Now - dt;
                    return ts.Hours + "小时前";
                }
                else
                {
                    return dt.ToString();
                }
            }
        }
    }
    public class ActorModel
    {
        public string actor { get; set; }
        public int actor_id { get; set; }
    }
    public class TagModel
    {
        public object result { get; set; }
        public string cover { get; set; }
        public int index { get; set; }
        public int tag_id { get; set; }
        public string tag_name { get; set; }
    }
    public class MovieModel
    {
        public int movie_status { get; set; }//1为收费，2为免费
        public MovieModel pay_user { get; set; }
        public int status { get; set; }//0为未付费
        public MovieModel payment { get; set; }
        public string pay_begin_time { get; set; }
        public decimal price { get; set; }
        public string product_id { get; set; }
        public MovieModel season { get; set; }

        public List<ActorModel> actor { get; set; }
        public string actors
        {
            get
            {
                if (actor.Count != 0)
                {
                    return actor[0].actor;
                }
                else
                {
                    return "";
                }
            }
        }




        public List<TagModel> tags { get; set; }
        public string tag
        {
            get
            {
                string str = "";
                foreach (var item in tags)
                {
                    str += item.tag_name + "、";
                }
                if (str.Length != 0)
                {
                    str = str.Remove(str.Length - 1);
                }
                return str;
            }
        }

        public string area { get; set; }
        public string cover { get; set; }
        public string pub_time { get; set; }
        public string time
        {
            get
            {
                DateTime dt = Convert.ToDateTime(pub_time);
                return dt.ToString("yyyy-MM-dd");
            }
        }

        public string title { get; set; }
        public string total_duration { get; set; }
        public MovieModel activity { get; set; }
        public int activity_id { get; set; }
        public string link { get; set; }
        public string script_src { get; set; }
    }
    public class relatesModel
    {
        public string aid { get; set; }
        public relatesModel owner { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string pic { get; set; }
        public relatesModel stat { get; set; }
        public string danmaku { get; set; }
        public string view { get; set; }
    }

    public class ownerModel
    {
        public string face { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
    }

    public class elecModel
    {
        public int count { get; set; }
        public int elec_num { get; set; }
        public int total { get; set; }
        public List<elecModel> list { get; set; }
        public string avatar { get; set; }
        public string pay_mid { get; set; }
        public string rank { get; set; }
        public string uname { get; set; }
    }



    public class owner_extModel
    {

    }
    public class FavboxModel
    {
        public object data { get; set; }

        public int code { get; set; }

        public string fid { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public int max_count { get; set; }//总数
        public int cur_count { get; set; }//现存

        public string Count
        {
            get
            {
                return cur_count + "/" + max_count;
            }
        }
    }

    public class pagesModel
    {
        public string cid { get; set; }
        public string from { get; set; }
        public string has_alias { get; set; }
        public string link { get; set; }
        public string page { get; set; }
        public string part { get; set; }
       
        public string View
        {
            get {
                if (part.Length==0)
                {
                    return page;
                }
                else
                {
                    return page+" "+part;
                }
               
            } }
        public string rich_vid { get; set; }
        public string vid { get; set; }
        public string weblink { get; set; }
        public Visibility IsDowned
        {
            get
            {
                if (DownloadHelper.downMids.ContainsKey(cid))
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
                if (SqlHelper.GetPostIsViewPost(cid))
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
    public class statModel
    {
        public string coin { get; set; }
        public string danmaku { get; set; }
        public string favorite { get; set; }
       // public int his_rank { get; set; }
       // public int now_rank { get; set; }
        public string reply { get; set; }
        public string share { get; set; }
        public string view { get; set; }
        public string View
        {
            get
            {
                if (Convert.ToInt64(view) > 10000)
                {
                    double d = (double)Convert.ToDouble(view) / 10000;
                    return d.ToString("0.0") + "万";
                }
                else
                {
                    return view;
                }
            }
        }
        public string Coin
        {
            get
            {
                if (Convert.ToInt32(coin) > 10000)
                {
                    double d = (double)Convert.ToDouble(coin) / 10000;
                    return d.ToString("0.0") + "万";
                }
                else
                {
                    return coin;
                }
            }
        }
        public string Danmaku
        {
            get
            {
                if (Convert.ToInt32(danmaku) > 10000)
                {
                    double d = (double)Convert.ToDouble(danmaku) / 10000;
                    return d.ToString("0.0") + "万";
                }
                else
                {
                    return danmaku;
                }
            }
        }
        public string Favorite
        {
            get
            {
                if (Convert.ToInt32(favorite) > 10000)
                {
                    double d = (double)Convert.ToDouble(favorite) / 10000;
                    return d.ToString("0.0") + "万";
                }
                else
                {
                    return favorite;
                }
            }
        }
    }
   
    public class req_userModel
    {
       public int attention { get; set; }
        public int favorite { get; set; }
    }
    public class rightsModel
    {
       public int bp { get; set; }
        public int download { get; set; }
        public int elec { get; set; }
        public int hd5 { get; set; }
        public int movie { get; set; }
        public int pay { get; set; }
    }
    public class seasonModel
    {
        public string season_id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public int is_finish { get; set; }
        public int episode_status { get; set; }
        public string newest_ep_index { get; set; }
        public int weekday { get; set; }
        public int total_count { get; set; }
        public string BanText
        {
            get
            {
                if (is_finish == 1)
                {
                    return string.Format("已完结,共{0}话", total_count);
                }
                else
                {
                    string we = string.Empty;
                    switch (weekday)
                    {
                        case 1:
                            we = "一";
                            break;
                        case 2:
                            we = "二";
                            break;
                        case 3:
                            we = "三";
                            break;
                        case 4:
                            we = "四";
                            break;
                        case 5:
                            we = "五";
                            break;
                        case 6:
                            we = "六";
                            break;
                        case 7:
                            we = "日";
                            break;
                        default:
                            break;
                    }
                    return string.Format("连载中,每周{0}更新", we);
                }
            }
        }
    }
    public class tagModel
    {
        public string cover { get; set; }
        public string hates { get; set; }
        public string likes { get; set; }
        public string tag_id { get; set; }
        public string tag_name { get; set; }
    }

}
