using System;
using System.Windows;
using System.Windows.Input;

namespace LWarehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event EventHandler EventSizeChanged;
        public static double mainWidth;
        public static double mainHeight;

        public MainWindow()
        {
            InitializeComponent();
            mainWidth = Width;
            mainHeight = Height;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_ExitProgram_Click(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainWidth = Width;
            mainHeight = Height;
            EventSizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
