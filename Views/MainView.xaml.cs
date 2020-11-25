using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CoordinateTracker.Models;
using CoordinateTracker.ViewModels;

namespace CoordinateTracker.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Creates a new Main view window.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            ToggleAddingCoordinates(false);

            _mainViewModel = new MainViewModel();

            if (!SaveFile.Load()) return;

            foreach (var world in World.Worlds)
                AddWorldToWindow(world);
        }

        // * ---------- Event Listeners ---------- *
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            const string path = "SaveFile.txt";

            var saveFile = new SaveFile(path);

            saveFile.Save(World.Worlds);
        }

        private void AddWorldButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addWorldView = new AddWorldView();
            addWorldView.ShowDialog();

            var newWorld = World.Worlds[^1];

            AddWorldToWindow(newWorld);
        }

        private void WorldButton_OnClick(object sender, RoutedEventArgs e)
        {
            var worldName = ((Button) sender).Name;
            _mainViewModel.CurrentWorld = _mainViewModel.GetWorld(worldName);

            if (_mainViewModel.CurrentWorld == null)
            {
                DisplayError("The world could not be found.");
                return;
            }

            InitializeDataGrid();
        }

        private void CoordinateAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var name = CoordinateNameTextBox.Text;
            var rawCoordinates = CoordinatesTextBox.Text.Split(" ");
            var coordinates = _mainViewModel.ParseRawCoordinates(rawCoordinates);

            if (coordinates == null)
                return;

            Coordinate coordinate;

            AddCoordinateToWindow(coordinates.Length == 3
                ? coordinate = new Coordinate(coordinates[0], coordinates[1], coordinates[2], name)
                : coordinate = new Coordinate(coordinates[0], coordinates[1], name));

            _mainViewModel.CurrentWorld.Coordinates.Add(coordinate);
        }

        /// <summary>
        /// Adds the world to the window.
        /// </summary>
        /// <param name="world">The world to add.</param>
        private void AddWorldToWindow(World world)
        {
            var button = new Button { Content = world.Name, Height = 50 };

            button.Click += WorldButton_OnClick;
            button.Name = _mainViewModel.RemoveSpecialCharacter(world.Name, ' ');

            WorldsItemsControl.Items.Add(button);
            _mainViewModel.CurrentWorld = world;

            InitializeDataGrid();
        }

        /// <summary>
        /// Toggle if the user can add coordinates.
        /// </summary>
        /// <param name="enable">If it should be enabled or not.</param>
        private void ToggleAddingCoordinates(bool enable)
        {
            CoordinateAddButton.IsEnabled = enable;
            CoordinateNameTextBox.IsEnabled = enable;
            CoordinatesTextBox.IsEnabled = enable;
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="header">The header.</param>
        public static void DisplayError(string message, string header = "Error")
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Adds all coordinates to the window.
        /// </summary>
        /// <param name="world">The specific world.</param>
        private void LoadWorldCoordinates(World world)
        {
            foreach (var coordinate in world.Coordinates)
                AddCoordinateToWindow(coordinate);    
        }


        /// <summary>
        /// Adds a specific coordinate to the window.
        /// </summary>
        /// <param name="coordinate">The specific coordinate.</param>
        private void AddCoordinateToWindow(Coordinate coordinate)
        {
            CoordinateDataGrid.Items.Add(coordinate);
        }

        /// <summary>
        /// Remove all coordinates from window.
        /// </summary>
        private void RemoveCoordinatesFromWindow()
        {
            CoordinateDataGrid.Items.Clear();
            CoordinateDataGrid.Items.Refresh();
        }

        /// <summary>
        /// "Refresh" data grid.
        /// </summary>
        private void InitializeDataGrid()
        {
            RemoveCoordinatesFromWindow();
            LoadWorldCoordinates(_mainViewModel.CurrentWorld);
            ToggleAddingCoordinates(true);
        }
    }
}