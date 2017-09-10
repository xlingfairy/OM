using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OM.App
{
    /// <summary>
    /// Pagination.xaml 的交互逻辑
    /// </summary>
    public partial class Pagination : UserControl
    {

        public static readonly DependencyProperty TotalProperty
            = DependencyProperty.Register("Total", typeof(long), typeof(Pagination), new FrameworkPropertyMetadata(0L, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, Changed));

        public static readonly DependencyProperty PageProperty
            = DependencyProperty.Register("Page", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, Changed));

        public static readonly DependencyProperty PageSizeProperty
            = DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, Changed));

        public static readonly DependencyProperty LabelCountProperty
            = DependencyProperty.Register("LabelCount", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, Changed));


        public static readonly DependencyProperty PageChangedCommandProperty
            = DependencyProperty.Register("PageChangedCommand", typeof(ICommand), typeof(Pagination));


        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (Pagination)d;
            p.Reset();
        }


        public long Total
        {
            get
            {
                return (long)this.GetValue(TotalProperty);
            }
            set
            {
                this.SetValue(TotalProperty, value < 0 ? 0 : value);
            }
        }

        public int PageSize
        {
            get
            {
                return (int)this.GetValue(PageSizeProperty);
            }
            set
            {
                this.SetValue(PageSizeProperty, value < 1 ? 10 : value);
            }
        }

        public int Page
        {
            get
            {
                return (int)this.GetValue(PageProperty);
            }
            set
            {
                this.SetValue(PageProperty, value < 0 ? 0 : value);
            }
        }

        public int LabelCount
        {
            get
            {
                return (int)this.GetValue(LabelCountProperty);
            }
            set
            {
                this.SetValue(LabelCountProperty, value < 0 ? 10 : value);
            }
        }


        public ICommand PageChangedCommand
        {
            get
            {
                return (ICommand)this.GetValue(PageChangedCommandProperty);
            }
            set
            {
                this.SetValue(PageChangedCommandProperty, value);
            }
        }


        private void Reset()
        {
            var totalPage = (int)Math.Ceiling((float)this.Total / this.PageSize);
            totalPage = totalPage < 1 ? 1 : totalPage;
            if (this.Page >= totalPage)
                this.Page = 0;

            var pages = this.GetPageItems(totalPage);

            this.pages.ItemsSource = pages;
            this.totalPageTxt.Text = totalPage.ToString();
            this.currPageTxt.Text = (this.Page + 1).ToString();
            this.totalTxt.Text = this.Total.ToString();
        }

        public Pagination()
        {
            InitializeComponent();
        }


        private List<PageItem> GetPageItems(int totalPage)
        {
            int begin = this.Page - this.LabelCount / 2;
            if (begin < 1)
                begin = 1;
            int end = begin + this.LabelCount;
            if (end > totalPage)
            {
                end = totalPage + 1;
                begin = end - this.LabelCount;
                if (begin < 1)
                    begin = 1;
            }
            if (end - begin > 0)
            {

                var items = Enumerable.Range(begin, end - begin)
                    .Select(i => new PageItem()
                    {
                        Page = i - 1,
                        Label = i.ToString(),
                        NotCurrent = i - 1 != this.Page
                    }).ToList();

                if (this.Page > 0)
                {
                    items.Insert(0, new PageItem()
                    {
                        Label = "<",
                        Page = this.Page - 1,
                        NotCurrent = true
                    });
                    items.Insert(0, new PageItem()
                    {
                        Label = "|<",
                        Page = 0,
                        NotCurrent = this.Page != 0
                    });
                }

                if (this.Page < totalPage - 1)
                {

                    items.Add(new PageItem()
                    {
                        Label = ">",
                        Page = this.Page + 1,
                        NotCurrent = true
                    });

                    items.Add(new PageItem()
                    {
                        Label = ">|",
                        Page = totalPage - 1,
                        NotCurrent = totalPage - 1 != this.Page
                    });
                }
                return items;
            }
            else
                return null;

        }

        public class PageItem
        {
            public int Page { get; set; }

            public string Label { get; set; }

            public bool NotCurrent { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var page = Convert.ToInt32(btn.Tag);
            this.PageChangedCommand?.Execute(page);
            this.Page = page;
        }
    }
}
