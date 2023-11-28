using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace _372_project
{
    /// <summary>
    /// Interaction logic for WindowEkle.xaml
    /// </summary>
    public partial class WindowEkle : Window
    {
        private Window window;

        int mode = 0;

        private DataSet dataSet1;
        private DataSet dataSet2;
        private DataSet dataSetJoined;

        private string tableName1;
        private string tableName2;

        private List<TextBox> textBoxes = new List<TextBox>();
        private List<TextBlock> textBlocks = new List<TextBlock>();
        private Grid filterTextBoxesGrid = new Grid();

        public WindowEkle(Window window)
        {
            InitializeComponent();

            this.window = window;

            setupControls();
        }

        private void setupControls()
        {
            combobox_veri_tipi.ItemsSource = Constants.NAME_TO_TABLE;
        }

        private void combobox_veri_tipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            mode = 0;
            ComboboxKeyValuePair selected = (ComboboxKeyValuePair)combobox_veri_tipi.SelectedItem;

            tableName1 = selected.Value;
            tableName2 = selected.Value2;
            string joinCommand = selected.Command;

            dataSet1 = DatabaseManager.selectCommand("SELECT * FROM " + tableName1 + " LIMIT 0");
            if((tableName2.Length > 0) && (joinCommand.Length > 0))
            {
                dataSet2 = DatabaseManager.selectCommand("SELECT * FROM " + tableName2 + " LIMIT 0");
                dataSetJoined = DatabaseManager.selectCommand("SELECT * FROM " + tableName1 + " " + joinCommand + " LIMIT 0");
                mode = 1;
            }

            setupTextBoxes();
        }

        private void setupTextBoxes()
        {
            DataSet dataSet = (mode == 0) ? dataSet1 : dataSetJoined;

            textBoxes.Clear();
            textBlocks.Clear();
            filterTextBoxesGrid.Children.Clear();
            filterTextBoxesGrid = new Grid();

            filter_scrollviewer.Content = filterTextBoxesGrid;

            filterTextBoxesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            filterTextBoxesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(45, GridUnitType.Pixel) });
            filterTextBoxesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int rowCount = 0;

            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                if(i % 2 == 0)
                {
                    filterTextBoxesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Pixel) });
                    filterTextBoxesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Pixel) });
                    rowCount += 2;
                }

                DataColumn attribute = dataSet.Tables[0].Columns[i];
                string attributeName = attribute.ColumnName;
                Constants.NAME_TO_ATTR_DICT.TryGetValue(attributeName, out attributeName);
                TextBox textBox = Utils.createTextBox(attributeName, filterTextBoxesGrid, (i/2)+1, (i%2)*2, 1, 1);
                TextBlock textBlock = Utils.createTextBlock(attribute.ColumnName, filterTextBoxesGrid, i/2, (i%2)*2, 1, 1);
                textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                textBox.Margin = new Thickness(0, 0, 0, 0);
                textBlock.Margin = new Thickness(10, 0, 0, -60);
                textBoxes.Add(textBox);
                textBlocks.Add(textBlock);
            }
        }

        private void button_ekle_click(object sender, RoutedEventArgs e)
        {
            string command = "START TRANSACTION;";

            {
                string attributes = tableName1 + "(";
                string values = "VALUES(";

                for(int i = 0; i < textBoxes.Count; i++)
                {
                    string attributeValue = textBoxes[i].Text.Trim();
                    if (attributeValue.Length == 0)
                        continue;

                    string attributeName = textBoxes[i].Name;

                    foreach (DataColumn attribute in dataSet1.Tables[0].Columns)
                    {
                        string columnName = attribute.ColumnName;
                        Constants.NAME_TO_ATTR_DICT.TryGetValue(columnName, out columnName);
                        if (columnName.Equals(attributeName))
                        {
                            if (attribute.DataType == typeof(string) || attribute.DataType == typeof(DateTime))
                            {
                                attributeValue = "'" + attributeValue + "'";
                            }
                
                            attributes += attributeName + ", ";
                            values += attributeValue + ", ";

                            break;
                        }
                    }

                }

                command += " INSERT INTO " + attributes.Substring(0, attributes.Length - 2) + ") " + values.Substring(0, values.Length - 2) + ");";
            }

            if(mode == 1)
            {
                string attributes = tableName2 + "(";
                string values = "VALUES(";

                for (int i = 0; i < textBoxes.Count; i++)
                {
                    string attributeValue = textBoxes[i].Text.Trim();
                    if (attributeValue.Length == 0)
                        continue;

                    string attributeName = textBoxes[i].Name;

                    foreach (DataColumn attribute in dataSet2.Tables[0].Columns)
                    {
                        string columnName = attribute.ColumnName;
                        Constants.NAME_TO_ATTR_DICT.TryGetValue(columnName, out columnName);
                        if (columnName.Equals(attributeName))
                        {
                            if (attribute.DataType == typeof(string) || attribute.DataType == typeof(DateTime))
                            {
                                attributeValue = "'" + attributeValue + "'";
                            }

                            attributes += attributeName + ", ";
                            values += attributeValue + ", ";

                            break;
                        }
                    }
                }

                command += " INSERT INTO " + attributes.Substring(0, attributes.Length - 2) + ") " + values.Substring(0, values.Length - 2) + ");";
            }

            try
            {
                int rowCount = DatabaseManager.insertCommand(command);
                if (rowCount == 0) throw new Exception();
            } catch(Exception)
            {
                DatabaseManager.rollback();
                MessageBox.Show("Hata: Ekleme işlemi başarısız oldu.");
                return;
            }

            DatabaseManager.commit();
            MessageBox.Show("Veri başarıyla eklendi");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.IsEnabled = true;
        }
    }
}
