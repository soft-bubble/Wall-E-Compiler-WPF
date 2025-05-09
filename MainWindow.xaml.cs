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
using Wall_E_Compiler.scripts.lexer;

namespace Wall_E_Compiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializePixelGrid(16, 16); 
        }

        private void InitializePixelGrid(int rows, int columns)
        {
            PixelGrid.Items.Clear();
            var uniformGrid = (UniformGrid)PixelGrid.ItemsPanel.LoadContent();
            uniformGrid.Rows = rows;
            uniformGrid.Columns = columns;

            for (int i = 0; i < rows * columns; i++)
            {
                PixelGrid.Items.Add(new object()); 
            }
        }

        private void Pixel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Child is Rectangle pixel)
            {
                pixel.Fill = (pixel.Fill == Brushes.White) ? Brushes.Red : Brushes.White; //no funcional
            }
        }
    }
}