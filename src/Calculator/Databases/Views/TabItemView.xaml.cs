using Calculator.Databases.Models;
using Calculator.Exceptions;
using Calculator.Extensions;
using Calculator.Workspaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.Databases.Views
{
    /// <summary>
    /// Logique d'interaction pour TabItemView.xaml
    /// </summary>
    public partial class TabItemView : UserControl
    {
        public TabItemView()
        {
            InitializeComponent();
        }
        private DatabaseModel Model => DataContext as DatabaseModel;

        private void ItemFilter_Checked(object sender, RoutedEventArgs e)
        {
            var button = e.Source as RadioButton;
            if (button != null)
            {
                var filter = button.Content.ToString();
                if (filter == "None")
                {
                    Model.Items = Model.Database.Items.ToObservableCollection();
                }
                else
                {
                    Model.Items = Model.Database.Items.Where(x => x.ItemType == filter).ToObservableCollection();
                }
            }
        }
        private void ListViewItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.ListViewItems.SelectedItem as Item;
            this.Model.SelectedItem = item;
        }
        private void ItemType_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ItemType_LostFocus(sender, null);
            }
        }
        private void ItemType_LostFocus(object sender, RoutedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var text = combobox.Text;
            if (!Model.ItemTypes.Contains(text))
            {
                Model.ItemTypes.Add(text);
                combobox.SelectedItem = text;
            }
        }
        private void ItemForm_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ItemForm_LostFocus(sender, null);
            }
        }
        private void ItemForm_LostFocus(object sender, RoutedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var text = combobox.Text;
            if (!Model.ItemForms.Contains(text))
            {
                Model.ItemForms.Add(text);
                combobox.SelectedItem = text;
            }
        }
        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = Model.SelectedItem;
                Model.SelectedItem = item.Clone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EraserItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.SelectedItem = new Item();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = Model.SelectedItem;
                Model.SaveItem(item);
                Model.SelectedItem = item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = Model.SelectedItem;
                Model.DeleteItem(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectItemIconPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // To use System.Windows.Forms add <UseWindowsForms>true</UseWindowsForms> in .csproj file
                using (var dialog = new System.Windows.Forms.OpenFileDialog())
                {
                    dialog.Filter = "Image PNG: (*.png)|*.png|Image JPEG: (*.jpg)|*.jpg;*.jpeg|AllFiles:(*.*)|*.*";
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    string path = dialog.FileName;
                    if (!String.IsNullOrEmpty(path))
                    {
                        try
                        {
                            var imageFile = WorkspacesModel.Intance.Current.SaveImageIntoWorkspace(path, false);
                            Model.SelectedItem.IconPath = imageFile;
                        }
                        catch (ImageException ex)
                        {
                            // Configure message box
                            string message = "Hello, MessageBox!";
                            string caption = $"{ex.Message}\nDo you want to overwrite the image?";
                            MessageBoxButton buttons = MessageBoxButton.YesNo;
                            // Show message box
                            MessageBoxResult resultImage = MessageBox.Show(message, caption, buttons);
                            if (resultImage == MessageBoxResult.Yes)
                            {
                                var imageFile = WorkspacesModel.Intance.Current.SaveImageIntoWorkspace(path, true);
                                Model.SelectedItem.IconPath = imageFile;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
