using System.Windows;
using CoordinateTracker.Models;

namespace CoordinateTracker.Views
{
    /// <summary>
    /// Interaction logic for AddWorldView.xaml
    /// </summary>
    public partial class AddWorldView
    {
        /// <summary>
        /// Creates a new window for adding a world.
        /// </summary>
        public AddWorldView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event listener for the add button that
        /// adds a new world with the given world name.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event argument.</param>
        private void AddWorldButton_OnClick(object sender, RoutedEventArgs e)
        {
            var name = WorldNameTextBox.Text;
            var world = new World(name);

            Close();
        }
    }
}
