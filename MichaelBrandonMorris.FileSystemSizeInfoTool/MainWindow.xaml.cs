using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Class MainWindow.
    /// </summary>
    /// <seealso cref="Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// TODO Edit XML Comment Template for MainWindow
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="MainWindow" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Handles the OnMouseLeftButtonDown event of the
        ///     UIElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="MouseButtonEventArgs" />
        ///     instance containing the event data.
        /// </param>
        /// TODO Edit XML Comment Template for UIElement_OnMouseLeftButtonDown
        private void UIElement_OnMouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
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