using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _372_project
{
    /// <summary>
    /// Interaction logic for PageSorgula.xaml
    /// </summary>
    public partial class PageSorgula : Page
    {
        List<ComboboxKeyValuePair> category1List = new List<ComboboxKeyValuePair>();

        Grid filterTextBoxesGrid = new Grid();
        List<TextBox> textBoxes = new List<TextBox>();
        List<TextBlock> textBlocks = new List<TextBlock>();

        public PageSorgula()
        {
            InitializeComponent();
            category1List.Add(new ComboboxKeyValuePair("Öğrenci", "STUDENT"));
            category1List.Add(new ComboboxKeyValuePair("Veli", "PARENT"));
            category1List.Add(new ComboboxKeyValuePair("Çalışan", "EMPLOYEE"));
            category1List.Add(new ComboboxKeyValuePair("Ders", "COURSE"));
            category1List.Add(new ComboboxKeyValuePair("Harcama", "EXPENSE"));
            category1List.Add(new ComboboxKeyValuePair("Stok", "STOCK"));

            category1.ItemsSource = category1List;
        }

        private void Geri_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GeriButtonClick");
            Application.Current.MainWindow.Content = new PageMain();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboboxKeyValuePair selection = (ComboboxKeyValuePair) e.AddedItems[0];
            Debug.WriteLine(selection.Value);
            
            DataSet dataSet = DatabaseManager.selectCommand("SELECT * FROM " + selection.Value);
            dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;

            for(int i = 0; i < textBoxes.Count; i++)
            {
                filterTextBoxesGrid.Children.Remove(textBoxes[i]);
                filterTextBoxesGrid.Children.Remove(textBlocks[i]);
            }
            textBoxes.Clear();

            grid.Children.Remove(filterTextBoxesGrid);

            filterTextBoxesGrid = new Grid();

            Grid.SetRow(filterTextBoxesGrid, 5);
            Grid.SetColumn(filterTextBoxesGrid, 0);
            Grid.SetColumnSpan(filterTextBoxesGrid, 16);
            Grid.SetRowSpan(filterTextBoxesGrid, 2);
            grid.Children.Add(filterTextBoxesGrid);

            filterTextBoxesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            filterTextBoxesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                filterTextBoxesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(130, GridUnitType.Pixel) });
                DataColumn attribute = dataSet.Tables[0].Columns[i];
                string attributeName = attribute.ColumnName;
                Constants.NAME_TO_ATTR_DICT.TryGetValue(attributeName, out attributeName);
                TextBox textBox = Utils.createTextBox(attributeName, filterTextBoxesGrid, 1, i, 1, 2);
                TextBlock textBlock = Utils.createTextBlock(attribute.ColumnName, filterTextBoxesGrid, 0, i, 1, 2);
                textBoxes.Add(textBox);
                textBlocks.Add(textBlock);

                textBox.KeyDown += new KeyEventHandler(filter_textbox_key_down);
            }
        }

        private void filter_textbox_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                string command = "";

                if (category1.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir kategori seçiniz.");
                    return;
                }

                int conditionCount = 0;
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    string text = textBoxes[i].Text.Trim();

                    if (text.Length > 0)
                    {
                        if (conditionCount > 0)
                            command += " AND";
                        conditionCount++;

                        text = text.ToUpper();

                        Regex regex = new Regex("(AND)|(OR)");

                        string[] conditions = regex.Split(text);
                        for(int j = 0; j < conditions.Length; j++)
                        {
                            string condition = conditions[j].Trim();

                            if(condition.Equals("AND") || condition.Equals("OR"))
                            {
                                command += (" " + condition);
                                continue;
                            }

                            if (!condition[0].Equals('<') && !condition[0].Equals('>') && !condition[0].Equals('='))
                                condition = "=" + condition;

                            command += (" " + textBoxes[i].Name + condition);
                        }
                    }
                }

                if (conditionCount > 0)
                {
                    command = "SELECT * FROM " + ((ComboboxKeyValuePair)category1.SelectedItem).Value + " WHERE" + command;
                }
                else
                {
                    command = "SELECT * FROM " + ((ComboboxKeyValuePair)category1.SelectedItem).Value;
                }

                Debug.WriteLine(command);
                DataSet dataSet = DatabaseManager.selectCommand(command);
                dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
        }
    }
}
