using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MichaelBrandonMorris.FileSizeTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
            {
                return;
            }

            var messageBox = sender as Border;
            var message = messageBox?.Child as TextBlock;

            if (message == null)
            {
                return;
            }

            messageBox.Visibility = Visibility.Hidden;
            message.Text = string.Empty;
            Panel.SetZIndex(messageBox, -1);
        }
    }
}
