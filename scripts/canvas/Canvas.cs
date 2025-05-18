using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wall_E_Compiler
{
    public class Canvas
    {
        public Color[,] pixels { get; private set; }
        public int size { get; private set; }

        private UniformGrid uiGrid; 

        public Canvas(int size, UniformGrid uiGrid)
        {
            this.size = size;
            this.uiGrid = uiGrid;
            pixels = new Color[size, size];
            Initialize();
        }

        private void Initialize()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    pixels[x, y] = Colors.White;
                    UpdateUIPixel(x, y);
                }
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                pixels[x, y] = color;
                UpdateUIPixel(x, y);
            }
        }

        public Color GetPixel(int x, int y)
        {
            return pixels[x, y];
        }

        private void UpdateUIPixel(int x, int y)
        {
            int index = y * size + x;
            if (index < uiGrid.Children.Count)
            {
                var border = (Border)uiGrid.Children[index];
                border.Background = new SolidColorBrush(pixels[x, y]);
            }
        }

        public void Clear()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    pixels[x, y] = Colors.White;
                    UpdateUIPixel(x, y);
                }
            }
        }

        public void Resize(int newSize, UniformGrid newUIGrid)
        {
            Color[,] oldPixels = (Color[,])pixels.Clone();
            int oldSize = size;

            size = newSize;
            uiGrid = newUIGrid;
            pixels = new Color[newSize, newSize];

            Initialize();

            int copySize = Math.Min(oldSize, newSize);
            for (int y = 0; y < copySize; y++)
            {
                for (int x = 0; x < copySize; x++)
                {
                    pixels[x, y] = oldPixels[x, y];
                }
            }

            RefreshAllPixels();
        }

        private void RefreshAllPixels()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    UpdateUIPixel(x, y);
                }
            }
        }
    }
}
