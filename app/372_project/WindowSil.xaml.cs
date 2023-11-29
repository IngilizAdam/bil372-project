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
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _372_project
{
    /// <summary>
    /// Interaction logic for WindowSil.xaml
    /// </summary>
    public partial class WindowSil : Window
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

        public WindowSil(Window window)
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
            if ((tableName2.Length > 0) && (joinCommand.Length > 0))
            {
                dataSet2 = DatabaseManager.selectCommand("SELECT * FROM " + tableName2 + " LIMIT 0");
                dataSetJoined = DatabaseManager.selectCommand("SELECT * FROM " + tableName1 + " " + joinCommand + " LIMIT 0");
                mode = 1;
            }

            setupTextBoxes();

            refresh();
        }

        private void setupTextBoxes()
        {
            DataSet dataSet = (mode == 0) ? dataSet1 : dataSetJoined;

            textBoxes.Clear();
            textBlocks.Clear();
            filterTextBoxesGrid.Children.Clear();
            filterTextBoxesGrid = new Grid();

            filter_scrollviewer.Content = filterTextBoxesGrid;

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
                textBlock.Margin = new Thickness(10, 0, 0, 0);
                textBox.Margin = new Thickness(10, 0, 0, 5);

                textBoxes.Add(textBox);
                textBlocks.Add(textBlock);

                textBox.KeyDown += new KeyEventHandler(filter_textbox_key_down);
            }
        }
        private void filter_textbox_key_down(object sender, KeyEventArgs e)
        {
            
            if (e.Key.Equals(Key.Enter))
            {
                refresh();
            }
        }

        private void refresh()
        {
            if(combobox_veri_tipi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir veri tipi seçiniz.", "Hata");
                return;
            }

            ComboboxKeyValuePair selected = (ComboboxKeyValuePair)combobox_veri_tipi.SelectedItem;

            DataSet dataSet = (mode == 0) ? dataSet1 : dataSetJoined;

            string command = "SELECT * FROM " + tableName1;
            if (mode == 1)
            {
                command += " " + selected.Command;
            }

            int conditionCount = 0;
            string conditionCommand = " WHERE";

            for (int i = 0; i < textBoxes.Count; i++)
            {
                string text = textBoxes[i].Text.Trim();

                if (text.Length > 0)
                {
                    if (conditionCount > 0)
                        conditionCommand += " AND";
                    conditionCount++;

                    text = text.ToUpper();

                    Regex regex = new Regex("(AND)|(OR)");

                    string[] conditions = regex.Split(text);
                    for (int j = 0; j < conditions.Length; j++)
                    {
                        string condition = conditions[j].Trim();

                        if (condition.Equals("AND") || condition.Equals("OR"))
                        {
                            conditionCommand += (" " + condition);
                            continue;
                        }

                        if (!condition[0].Equals('<') && !condition[0].Equals('>') && !condition[0].Equals('='))
                            condition = "=" + condition;

                        condition = condition[0] + condition.Substring(1).Trim();

                        string attributeName = "";
                        foreach (DataColumn attribute in dataSet.Tables[0].Columns)
                        {
                            attributeName = attribute.ColumnName;
                            Constants.NAME_TO_ATTR_DICT.TryGetValue(attributeName, out attributeName);
                            if (attributeName.Equals(textBoxes[i].Name))
                            {
                                if (attribute.DataType == typeof(string) || attribute.DataType == typeof(DateTime))
                                {
                                    condition = condition[0] + "'" + condition.Substring(1) + "'";
                                }

                                break;
                            }
                        }

                        conditionCommand += (" " + attributeName + condition);
                    }
                }
            }

            if(conditionCount > 0)
            {
                command += conditionCommand;
            }

            dataSet = DatabaseManager.selectCommand(command);
            setDataGridSource(dataSet);
        }

        private void setDataGridSource(DataSet dataSet)
        {
            dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;

            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                DataColumn attribute = dataSet.Tables[0].Columns[i];
                if (attribute.DataType == typeof(DateTime))
                {
                    DataGridTextColumn column = (DataGridTextColumn)dataGrid.Columns[i];
                    column.Binding.StringFormat = "yyyy/MM/dd";
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.IsEnabled = true;
        }

        private void button_sil_Click(object sender, RoutedEventArgs e)
        {
            IList selectedRows = dataGrid.SelectedItems;

            if(selectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek satırları seçiniz.", "Hata");
                return;
            }

            string transaction = "START TRANSACTION;";

            for (int i = 0; i < selectedRows.Count; i++)
            {
                DataRowView rowView = (DataRowView) selectedRows[i];
                DataRow row = rowView.Row;
                Debug.WriteLine(row[0]);

                DataSet dataSet = dataSet1;
                string tableName = tableName1;

                string command = " DELETE FROM " + tableName + " WHERE";
                int conditionCount = 0;

                for(int j = 0; j < dataSet.Tables[0].Columns.Count; j++)
                {
                    DataColumn attribute = dataSet.Tables[0].Columns[j];
                    string attributeName = attribute.ColumnName;
                    Constants.NAME_TO_ATTR_DICT.TryGetValue(attributeName, out attributeName);

                    if(attribute.DataType == typeof(Single))
                        continue;

                    if(conditionCount > 0)
                        command += " AND";
                    conditionCount++;

                    command += " " + attributeName + "=";

                    if (attribute.DataType == typeof(string))
                    {
                        command += "'" + row[attribute.ColumnName] + "'";
                    }
                    else if(attribute.DataType == typeof(DateTime))
                    {
                        command += "'" + ((DateTime)row[attribute.ColumnName]).ToString("yyyy-MM-dd") + "'";
                    }
                    else
                    {
                        command += row[attribute.ColumnName];
                    }
                }

                transaction += command + ";";
            }

            int rowCount;
            try
            {
                rowCount = DatabaseManager.insertCommand(transaction);
                if (rowCount == 0) throw new Exception();
            }
            catch (Exception)
            {
                DatabaseManager.rollback();
                MessageBox.Show("Hata: Silme işlemi başarısız oldu.");
                return;
            }

            DatabaseManager.commit();
            MessageBox.Show(rowCount + " satır silindi");
            refresh();
        }
    }
}
