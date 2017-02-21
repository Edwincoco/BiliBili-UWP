﻿using BiliBili3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BiliBili3.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BanTagPage : Page
    {
        public BanTagPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void btn_Filter_Click(object sender, RoutedEventArgs e)
        {

            if (sp.IsPaneOpen)
            {
                sp.IsPaneOpen = false;
            }
            else
            {
                sp.IsPaneOpen = true;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (SettingHelper.Get_RefreshButton() && SettingHelper.IsPc())
            {
                b_btn_Refresh.Visibility = Visibility.Visible;
            }
            else
            {
                b_btn_Refresh.Visibility = Visibility.Collapsed;
            }
            if (e.NavigationMode == NavigationMode.New)
            {
               
                await Task.Delay(200);
                PageNum = 1;
                LoadLocal();
                LoadTag();
            }
        }

        private void LoadLocal()
        {
            List<FilterModel> LX = new List<FilterModel>() {
                new FilterModel() {name="全部",data="0"},
                new FilterModel() {name="TV版",data="1"},
                new FilterModel() {name="OVA·OAD版",data="2" },
                new FilterModel() {name="剧场版",data="3" },
                new FilterModel() {name="其他",data="4" },
            };
            view_LX.ItemsSource = LX;
            view_LX.SelectedIndex = 0;

            List<FilterModel> ZT = new List<FilterModel>() {
                new FilterModel() {name="全部",data="0"},
                new FilterModel() {name="完结",data="2"},
                new FilterModel() {name="连载",data="1" }
            };
            view_ZT.ItemsSource = ZT;
            view_ZT.SelectedIndex = 0;
            List<FilterModel> DQ = new List<FilterModel>() {
                new FilterModel() {name="全部",data="0"},
                 new FilterModel() {name="国产",data="1"},
                new FilterModel() {name="日本",data="2" },
                new FilterModel() {name="美国",data="3" },
                new FilterModel() {name="其它",data="4" }
            };
            view_DQ.ItemsSource = DQ;
            view_DQ.SelectedIndex = 0;
            List<FilterModel> Quarter = new List<FilterModel>() {
                new FilterModel() {name="全部",data="0"},
                 new FilterModel() {name="1月",data="1"},
                new FilterModel() {name="4月",data="2" },
                new FilterModel() {name="7月",data="3" },
                new FilterModel() {name="10月",data="4" }
            };
            view_Quarter.ItemsSource = Quarter;
            view_Quarter.SelectedIndex = 0;
        }

        private async void LoadTag()
        {
            try
            {
                pr_Load.Visibility = Visibility.Visible;
                string url = string.Format("https://bangumi.bilibili.com/api/bangumi_index_cond?access_key={0}&appkey={1}&build=411005&mobi_app=android&platform=wp&ts={2}000&type=0", ApiHelper.access_key,ApiHelper._appKey, ApiHelper.GetTimeSpan);
                url += "&sign=" + ApiHelper.GetSign(url);
                string results = await WebClientClass.GetResults(new Uri(url));
                BantagModel m = JsonConvert.DeserializeObject<BantagModel>(results);
                if (m.code == 0)
                {
                   
                    List<FilterModel> Years = new List<FilterModel>();
                    m.result.years.OrderByDescending(x => x).ToList().ForEach(x => Years.Add(new FilterModel() { name=x,data=x}));
                    Years.Insert(0,new FilterModel() { name = "全部", data = "0" });
                    view_Year.ItemsSource = Years;
                    view_Year.SelectedIndex = 0;

                    List<FilterModel> Tags = new List<FilterModel>();
                    m.result.category.ForEach(x=>Tags.Add(new FilterModel() { name=x.tag_name,data=x.tag_id}));
                    Tags.Insert(0,new FilterModel() { name = "全部", data = "" } );
                    view_FG.ItemsSource = Tags;
                    view_FG.SelectedIndex = 0;

                    GetInfo();
                }
                else
                {
                    messShow.Show(m.message, 3000);
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147012867 || ex.HResult == -2147012889)
                {
                    messShow.Show("无法连接服务器，请检查你的网络连接", 3000);
                }
                else
                {

                    messShow.Show("读取风格索引失败了", 3000);
                }
            }
            finally
            {
                pr_Load.Visibility = Visibility.Collapsed;
            }
        }

        private async void GetInfo()
        {
            try
            {
                _Loading=true;
                pr_Load.Visibility = Visibility.Visible;
                if (PageNum==1)
                {
                    gv1.Items.Clear();
                }
                //http://bangumi.bilibili.com/web_api/season/index?page=1&page_size=20&version=0&is_finish=0&start_year=0&quarter=1&tag_id=&index_type=1&index_sort=0
                //string url = "http://bangumi.bilibili.com/web_api/season/index?&area="+(view_DQ.SelectedItem as FilterModel).data+"&index_sort="+index_sort+"&index_type="+index_type+"=&is_finish="+ (view_ZT.SelectedItem as FilterModel).data + "&page="+PageNum+"&page_size=30&quarter="+ (view_Quarter.SelectedItem as FilterModel).data + "&start_year="+ (view_Year.SelectedItem as FilterModel).data + "&tag_id="+ (view_FG.SelectedItem as FilterModel).data + "&version="+ (view_LX.SelectedItem as FilterModel).data;
                string url = "http://bangumi.bilibili.com/web_api/season/index?page="+PageNum+"&page_size=30&version="+ (view_LX.SelectedItem as FilterModel).data + "&is_finish="+ (view_ZT.SelectedItem as FilterModel).data + "&start_year=" + (view_Year.SelectedItem as FilterModel).data + "&quarter="+ (view_Quarter.SelectedItem as FilterModel).data + "&tag_id="+ (view_FG.SelectedItem as FilterModel).data + "&index_type="+index_type+"&index_sort="+index_sort+ ((view_DQ.SelectedIndex!=0)?"&area=" + (view_DQ.SelectedItem as FilterModel).data:"");
                string results = await WebClientClass.GetResults(new Uri(url));
                AllBanModel m = JsonConvert.DeserializeObject<AllBanModel>(results);
                if (m.code == 0)
                {
                    if (m.result.list.Count!=0)
                    {
                        m.result.list.ForEach(x => gv1.Items.Add(x));
                        PageNum++;
                    }
                    else
                    {
                        messShow.Show("加载完了...", 3000);
                    }
                }
                else
                {
                    messShow.Show(m.message, 3000);
                }


            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147012867 || ex.HResult == -2147012889)
                {
                    messShow.Show("无法连接服务器，请检查你的网络连接", 3000);
                }
                else
                {

                    messShow.Show("读取番剧失败了", 3000);
                }
            }
            finally
            {
                _Loading = false;
                pr_Load.Visibility = Visibility.Collapsed;
            }
        }

        bool _Loading = false;
        int PageNum = 1;
        int index_sort = 0;
        int index_type = 1;
        private void tw_zf_0_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = true;
            tw_zf_1.IsChecked = false;
            tw_gx_0.IsChecked = false;
            tw_gx_1.IsChecked = false;
            tw_rq_0.IsChecked = false;
            tw_rq_1.IsChecked = false;
            PageNum = 1;
            index_type = 1;
            index_sort = 0;
            GetInfo();

        }

        private void tw_zf_1_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = false;
            tw_zf_1.IsChecked = true;
            tw_gx_0.IsChecked = false;
            tw_gx_1.IsChecked = false;
            tw_rq_0.IsChecked = false;
            tw_rq_1.IsChecked = false;
            PageNum = 1;
            index_type = 1;
            index_sort = 1;
            GetInfo();
        }

        private void tw_gx_0_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = false;
            tw_zf_1.IsChecked = false;
            tw_gx_0.IsChecked = true;
            tw_gx_1.IsChecked = false;
            tw_rq_0.IsChecked = false;
            tw_rq_1.IsChecked = false;
            PageNum = 1;
            index_type = 0;
            index_sort = 0;
            GetInfo();
        }

        private void tw_gx_1_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = false;
            tw_zf_1.IsChecked = false;
            tw_gx_0.IsChecked = false;
            tw_gx_1.IsChecked = true;
            tw_rq_0.IsChecked = false;
            tw_rq_1.IsChecked = false;
            PageNum = 1;
            index_type = 0;
            index_sort = 1;
            GetInfo();
        }

        private void tw_rq_0_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = false;
            tw_zf_1.IsChecked = false;
            tw_gx_0.IsChecked = false;
            tw_gx_1.IsChecked = false;
            tw_rq_0.IsChecked = true;
            tw_rq_1.IsChecked = false;
            PageNum = 1;
            index_type = 2;
            index_sort = 0;
            GetInfo();
        }

        private void tw_rq_1_Click(object sender, RoutedEventArgs e)
        {
            tw_zf_0.IsChecked = false;
            tw_zf_1.IsChecked = false;
            tw_gx_0.IsChecked = false;
            tw_gx_1.IsChecked = false;
            tw_rq_0.IsChecked = false;
            tw_rq_1.IsChecked = true;
            PageNum = 1;
            index_type = 2;
            index_sort = 1;
            GetInfo();

        }

        private void btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            view_DQ.SelectedIndex = 0;
            view_FG.SelectedIndex = 0;
            view_LX.SelectedIndex = 0;
            view_Quarter.SelectedIndex = 0;
            view_Year.SelectedIndex = 0;
            view_ZT.SelectedIndex = 0;
            PageNum = 1;
            GetInfo();
            sp.IsPaneOpen = false;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {

            PageNum = 1;
            GetInfo();
            sp.IsPaneOpen = false;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            int w = Convert.ToInt32(this.ActualWidth / 120);

            if (this.ActualWidth <= 500)
            {

                ViewBox2_num.Width = ActualWidth / 3 - 15;
            }
            else
            {

                ViewBox2_num.Width = ActualWidth / w - 13;
            }



        }

        private void sv_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (sv.VerticalOffset == sv.ScrollableHeight)
            {
                if (!_Loading)
                {
                    GetInfo();
                }
            }
        }

        private void btn_LoadMore_Click(object sender, RoutedEventArgs e)
        {
            if (!_Loading)
            {
                GetInfo();
            }
        }

        private void gv1_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(BanInfoPage), (e.ClickedItem as AllBanModel).season_id);
        }

        private void b_btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            PageNum = 1;
            //LoadLocal();
            LoadTag();
        }
    }
}
