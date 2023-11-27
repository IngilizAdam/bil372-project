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
        private Grid filterTextBoxesGrid = new Grid();
        private List<TextBox> textBoxes = new List<TextBox>();
        private List<TextBlock> textBlocks = new List<TextBlock>();

        private DataSet dataSet = new DataSet();

        public int categoryLevel = 0;
        List<ComboBox> categoryComboBoxes = new List<ComboBox>();

        public PageSorgula()
        {
            InitializeComponent();

            category1.ItemsSource = Constants.CATEGORY1_LIST;

            categoryComboBoxes.Add(category1);
            categoryComboBoxes.Add(category2);

            category2.Visibility = Visibility.Hidden;
            category2TextBlock.Visibility = Visibility.Hidden;

            ekleButton.Visibility = Visibility.Hidden;
        }

        private void Geri_Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("GeriButtonClick");
            Application.Current.MainWindow.Content = new PageMain();
        }

        private void category1_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore if changed from code
            if (e.AddedItems.Count == 0)
                return;

            categoryLevel = 0;
            ComboboxKeyValuePair selection = (ComboboxKeyValuePair) e.AddedItems[0];
            Debug.WriteLine(selection.Value);
            
            setupTableWithFilters(selection);
            
            initCategory2ComboBox(selection);
        }

        private void category2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ignore if changed from code
            if (e.AddedItems.Count == 0)
                return;

            categoryLevel = 1;
            ComboboxKeyValuePair selection = (ComboboxKeyValuePair) e.AddedItems[0];
            Debug.WriteLine(selection.Value);

            setupTableWithFilters(selection);

            if(category2.SelectedIndex != 0)
            {
                ekleButton.Visibility = Visibility.Visible;
                ekleButton.Content = selection.Key + " Ekle";
            }
            else
            {
                ekleButton.Visibility = Visibility.Hidden;
            }
        }

        private void initCategory2ComboBox(ComboboxKeyValuePair parent)
        {
            category2.Visibility = Visibility.Hidden;
            category2TextBlock.Visibility = Visibility.Hidden;
            List<ComboboxKeyValuePair> category2List = Constants.CATEGORY1_TO_2_DICT[parent.Value];
            category2.ItemsSource = category2List;
            if(category2List.Count > 0)
            {
                ekleButton.Visibility = Visibility.Hidden;
                category2.SelectedIndex = 0;
                category2.Visibility = Visibility.Visible;
                category2TextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ekleButton.Visibility = Visibility.Visible;
                ekleButton.Content = parent.Key + " Ekle";
            }
        }

        private void filter_textbox_key_down(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                string command = "";

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

                            condition = condition[0] + condition.Substring(1).Trim();

                            string attributeName = "";
                            foreach (DataColumn attribute in dataSet.Tables[0].Columns)
                            {
                                attributeName = attribute.ColumnName;
                                Constants.NAME_TO_ATTR_DICT.TryGetValue(attributeName, out attributeName);
                                if (attributeName.Equals(textBoxes[i].Name))
                                {
                                    if(attribute.DataType == typeof(string) || attribute.DataType == typeof(DateTime))
                                    {
                                        condition = condition[0] + "'" + condition.Substring(1) + "'";
                                    }
                                    
                                    break;
                                }
                            }

                            command += (" " + attributeName + condition);
                        }
                    }
                }

                if (conditionCount > 0)
                {
                    command = "SELECT * FROM " + ((ComboboxKeyValuePair)(categoryComboBoxes[categoryLevel].SelectedItem)).Value + " WHERE" + command;
                }
                else
                {
                    command = "SELECT * FROM " + ((ComboboxKeyValuePair)(categoryComboBoxes[categoryLevel].SelectedItem)).Value;
                }

                Debug.WriteLine(command);
                try
                {
                    dataSet = DatabaseManager.selectCommand(command);
                    setDataGridSource(dataSet);
                }
                catch (Exception)
                {
                    MessageBox.Show("Yanlış bir veri tipi girildi.", "Hata");
                }
            }
        }

        private void setupTableWithFilters(ComboboxKeyValuePair selection)
        {
            if (categoryLevel == 1 && categoryComboBoxes[categoryLevel].SelectedIndex != 0)
            {
                ComboboxKeyValuePair parent = ((ComboboxKeyValuePair)(categoryComboBoxes[categoryLevel - 1].SelectedItem));
                string attrName = Constants.JOIN_ATTR_DICT[parent.Value];
                dataSet = DatabaseManager.selectCommand("SELECT * FROM " + parent.Value + " JOIN " + selection.Value + " USING(" + attrName + ")");
            }
            else
            {
                dataSet = DatabaseManager.selectCommand("SELECT * FROM " + selection.Value);
            }

            setDataGridSource(dataSet);

            for (int i = 0; i < textBoxes.Count; i++)
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

        
        private void Ekle_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowEkle popup = new WindowEkle((ComboboxKeyValuePair)category1.SelectedItem, (ComboboxKeyValuePair)category2.SelectedItem, categoryLevel, dataSet);
            popup.Show();
        }
    }
}
