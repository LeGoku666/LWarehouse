using LWarehouse.MVVM.CustomControlls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace LWarehouse.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();

            MainWindow.EventSizeChanged += EventReach;
            WindowSizeChanged();
        }

        private void EventReach(object sender, EventArgs e)
        {
            WindowSizeChanged();
        }

        private void WindowSizeChanged()
        {
            Container_ListBox.Height = MainWindow.mainHeight - 220;

            double width = MainWindow.mainWidth - 220;
            Search_Border.Width = width;
            Search_RectangleGeometry.Rect = new Rect() { Width = width, Height = 60 };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random r = new();
            var time = 0.0f;
            for (int i = 0; i < 20; i++)
            {
                //Button but = new Button() { Content = "Button", Height = r.Next(15, 150), Width = r.Next(15, 150) };
                //Container_ListBox.Items.Add(but);

                SearchResult res = new();
                res.textBox_Header.Text = $"{i}";

                res.myBorder.Width = 0;
                res.myBorder.Height = 0;

                
                time += .05f;
                res.storyboardOnLoadedHeight.BeginTime = TimeSpan.FromSeconds(time);
                res.storyboardOnLoadedWidth.BeginTime = TimeSpan.FromSeconds(time);

                Container_ListBox.Items.Add(res);
            }
        }
    }
}
