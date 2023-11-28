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

namespace _372_project
{
    /// <summary>
    /// Interaction logic for WindowEkle.xaml
    /// </summary>
    public partial class WindowEkle : Window
    {
        private ComboboxKeyValuePair category1;
        private ComboboxKeyValuePair category2;
        private int categoryLevel;
        private DataSet dataSet;
        private Window window;

        private List<TextBox> textBoxes = new List<TextBox>();
        private List<TextBlock> textBlocks = new List<TextBlock>();
        private Grid filterTextBoxesGrid = new Grid();

        public WindowEkle(ComboboxKeyValuePair category1, ComboboxKeyValuePair category2, int categoryLevel, DataSet dataSet, Window window)
        {
            InitializeComponent();

            this.category1 = category1;
            this.category2 = category2;
            this.categoryLevel = categoryLevel;
            this.dataSet = dataSet;
            this.window = window;

            setupTextBoxes();
        }

        private void setupTextBoxes()
        {
            //Grid.SetRow(filterTextBoxesGrid, 0);
            //Grid.SetColumn(filterTextBoxesGrid, 2);
            //Grid.SetColumnSpan(filterTextBoxesGrid, 7);
            //grid.Children.Add(filterTextBoxesGrid);

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
            //Grid.SetRowSpan(filterTextBoxesGrid, rowCount+2);

            Button ekleButton = new Button();
            ekleButton.Content = "Ekle";
            ekleButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            Grid.SetRow(ekleButton, 13);
            Grid.SetColumn(ekleButton, 4);
            Grid.SetColumnSpan(ekleButton, 3);
            grid.Children.Add(ekleButton);

            ekleButton.Click += new RoutedEventHandler(Ekle_Button_Click);
        }

        private void Ekle_Button_Click(object sender, RoutedEventArgs e)
        {
            if(categoryLevel == 1)
            {
                if (category1.Value.Equals("STUDENT"))
                {
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();
                    
                        for(int i = 0; i < textBoxes.Count; i++)
                        {
                            if (!textBoxes[i].Name.Equals("grad_grad_date"))
                                subListTextBox.Add(textBoxes[i]);
                            else
                            {
                                Regex regex = new Regex(@"^\d{4}[-\/](0?[1-9]|1[012])[-\/](0?[1-9]|[12][0-9]|3[01])$");
                                if (!regex.IsMatch(textBoxes[i].Text.Trim()))
                                {
                                    MessageBox.Show("Hata: Ekleme işlemi başarısız oldu.");
                                    return;
                                }
                                textBoxes[i].Text = textBoxes[i].Text.Trim().Replace("-", "/");
                            }
                        }
                        
                        if(!insertValue(category1.Value, subListTextBox))
                            return;
                    }

                    if (category2.Value.Equals("ACTIVE"))
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            if (textBoxes[i].Name.Equals("stud_id"))
                                subListTextBox.Add(textBoxes[i]);
                        }

                        if (insertValue(category2.Value, subListTextBox))
                        {
                            MessageBox.Show("Ekleme işlemi başarılı oldu.");
                        }
                        
                        return;
                    }
                    else if (category2.Value.Equals("GRADUATE"))
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            if (textBoxes[i].Name.Equals("stud_id") || textBoxes[i].Name.Equals("grad_grad_date"))
                                subListTextBox.Add(textBoxes[i]);
                        }

                        if (insertValue(category2.Value, subListTextBox))
                        {
                            MessageBox.Show("Ekleme işlemi başarılı oldu.");
                        }

                        return;
                    }
                }
                else if (category1.Value.Equals("EMPLOYEE"))
                {
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            subListTextBox.Add(textBoxes[i]);
                        }

                        if (!insertValue(category1.Value, subListTextBox))
                            return;
                    }

                    if (category2.Value.Equals("INSTRUCTOR"))
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            if (textBoxes[i].Name.Equals("empl_id"))
                                subListTextBox.Add(textBoxes[i]);
                        }

                        if (insertValue(category2.Value, subListTextBox))
                        {
                            MessageBox.Show("Ekleme işlemi başarılı oldu.");
                        }

                        return;
                    }
                    else if (category2.Value.Equals("STAFF"))
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            if (textBoxes[i].Name.Equals("empl_id"))
                                subListTextBox.Add(textBoxes[i]);
                        }

                        if (insertValue(category2.Value, subListTextBox))
                        {
                            MessageBox.Show("Ekleme işlemi başarılı oldu.");
                        }

                        return;
                    }
                    else if (category2.Value.Equals("ADMIN_STAFF"))
                    {
                        List<TextBox> subListTextBox = new List<TextBox>();

                        for (int i = 0; i < textBoxes.Count; i++)
                        {
                            if (textBoxes[i].Name.Equals("empl_id"))
                                subListTextBox.Add(textBoxes[i]);
                        }

                        if (insertValue(category2.Value, subListTextBox))
                        {
                            MessageBox.Show("Ekleme işlemi başarılı oldu.");
                        }

                        return;
                    }
                }
            }
            else
            {
                if (insertValue(category1.Value, textBoxes))
                {
                    MessageBox.Show("Ekleme işlemi başarılı oldu.");
                }

                return;
            }
        }

        private bool insertValue(string tableName, List<TextBox> textBoxList)
        {
            string command = "INSERT INTO " + tableName + " (";
            string values = " VALUES (";
            for (int i = 0; i < textBoxList.Count; i++)
            {
                TextBox textBox = textBoxList[i];
                string attributeName = textBox.Name;
                string attributeValue = textBox.Text.Trim();

                if (attributeValue.Equals(""))
                    continue;

                foreach (DataColumn attribute in dataSet.Tables[0].Columns)
                {
                    string columnName = attribute.ColumnName;
                    Constants.NAME_TO_ATTR_DICT.TryGetValue(columnName, out columnName);
                    if (columnName.Equals(attributeName))
                    {
                        if (attribute.DataType == typeof(string) || attribute.DataType == typeof(DateTime))
                        {
                            attributeValue = "'" + attributeValue + "'";
                        }

                        break;
                    }
                }

                command += attributeName + ", ";
                values += attributeValue + ", ";
            }

            command = command.Substring(0, command.Length - 2);
            values = values.Substring(0, values.Length - 2);
            command += ")";
            values += ")";
            command += values;

            try
            {
                int row = DatabaseManager.insertCommand(command);
                if (row == 0) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata: Ekleme işlemi başarısız oldu.");

                return false;
            }

            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.IsEnabled = true;
        }
    }
}
