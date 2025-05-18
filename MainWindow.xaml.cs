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
    public partial class MainWindow : Window
    {
        private Canvas drawingCanvas;
        private int currentGridSize = 16;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCanvas(currentGridSize);
            GridSizeComboBox.SelectionChanged += GridSizeComboBox_SelectionChanged;
        }

        private void InitializeCanvas(int size)
        {
            PixelGrid.Children.Clear();
            PixelGrid.Rows = size;
            PixelGrid.Columns = size;

            // Crear elementos UI
            for (int i = 0; i < size * size; i++)
            {
                PixelGrid.Children.Add(new Border()
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White
                });
            }

            // Inicializar nuestro canvas lógico
            drawingCanvas = new Canvas(size, PixelGrid);
        }

        private void CompileButton_Click(object sender, RoutedEventArgs e)
        {
            string code = CodeTextBox.Text;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            drawingCanvas.Clear();
        }

        private void GridSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridSizeComboBox.SelectedItem is ComboBoxItem selectedItem &&
                int.TryParse(selectedItem.Tag.ToString(), out int newSize))
            {
                InitializeCanvas(newSize);
            }
        }
    }
}
