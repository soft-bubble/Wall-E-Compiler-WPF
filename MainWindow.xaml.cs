using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wall_E_Compiler;

namespace Wall_E_Compiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentGridSize = 16;

        public MainWindow()
        {
            InitializeComponent();
            InitializePixelGrid(currentGridSize);
            GridSizeComboBox.SelectionChanged += GridSizeComboBox_SelectionChanged;
        }

        private void GridSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridSizeComboBox.SelectedItem is ComboBoxItem selectedItem &&
                int.TryParse(selectedItem.Tag.ToString(), out int newSize))
            {
                currentGridSize = newSize;
                InitializePixelGrid(currentGridSize);
            }
        }

        private void InitializePixelGrid(int size)
        {
            PixelGrid.Children.Clear();
            PixelGrid.Rows = size;
            PixelGrid.Columns = size;

            for (int i = 0; i < size * size; i++)
            {
                var border = new Border()
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White
                };
                PixelGrid.Children.Add(border);
            }
        }

        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            string codigo = CodeTextBox.Text;
            ChangePixelColor(2, 3, Brushes.Red);
            MessageBox.Show($"Código compilado: {codigo}");
        }

        private void ChangePixelColor(int row, int col, Brush color)
        {
            int index = row * currentGridSize + col;
            if (index >= 0 && index < PixelGrid.Children.Count)
            {
                ((Border)PixelGrid.Children[index]).Background = color;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CodeTextBox.Clear();
            InitializePixelGrid(currentGridSize);
        }
    }
}
