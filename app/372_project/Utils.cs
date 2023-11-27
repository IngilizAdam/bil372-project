using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _372_project
{
    class Utils
    {
        public static TextBox createTextBox(string name, Grid grid, int row, int column, int rowSpan, int columnSpan)
        {
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.FontSize = 14;
            textBox.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBox.Width = 100;
            textBox.Margin = new System.Windows.Thickness(10, 0, 0, 0);

            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, column);
            Grid.SetRowSpan(textBox, rowSpan);
            Grid.SetColumnSpan(textBox, columnSpan);
            grid.Children.Add(textBox);

            return textBox;
        }

        public static TextBlock createTextBlock(string text, Grid grid, int row, int column, int rowSpan, int columnSpan)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.FontSize = 12;
            textBlock.TextWrapping = System.Windows.TextWrapping.WrapWithOverflow;
            textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBlock.Width = 120;
            textBlock.Margin = new System.Windows.Thickness(10, 0, 0, -20);

            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            Grid.SetRowSpan(textBlock, rowSpan);
            Grid.SetColumnSpan(textBlock, columnSpan);
            grid.Children.Add(textBlock);

            return textBlock;
        }
    }
}
