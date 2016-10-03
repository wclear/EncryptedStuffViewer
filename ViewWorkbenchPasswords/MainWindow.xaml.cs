using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace EncryptedStuffViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hides all sections and reveals the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            OutputGrid.Visibility = Visibility.Hidden;
            StringGrid.Visibility = Visibility.Hidden;
            MenuGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Ask the user for a file and decrypt it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptFileOptionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (openFileDialog.ShowDialog() == true)
            {
                string result = File.Exists(openFileDialog.FileName) ?
                    DPAPILite.DecryptFile(openFileDialog.FileName) : 
                    string.Format("File not found: {0}", openFileDialog.FileName);
                ShowResult(result);
            }

            // If no file is selected, the menu will still be visible at this point.
        }

        /// <summary>
        /// Shows the section for decrypting strings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptStringOptionButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            StringGrid.Visibility = Visibility.Visible;
            InputStringTextBox.Focus();
            AddMenuButton(StringGrid);
        }

        /// <summary>
        /// Adds the menu button to the given section.
        /// </summary>
        /// <param name="targetSection"></param>
        private void AddMenuButton(Grid targetSection)
        {
            if (!targetSection.Children.Contains(MenuButton))
            {
                Grid menuButtonParent = (Grid)MenuButton.Parent;
                menuButtonParent.Children.Remove(MenuButton);
                targetSection.Children.Add(MenuButton);
            }
        }

        /// <summary>
        /// Shows the output grid with the result of the decryption process.
        /// </summary>
        /// <param name="result"></param>
        private void ShowResult(string result)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            StringGrid.Visibility = Visibility.Hidden;
            Output.Text = result;
            OutputGrid.Visibility = Visibility.Visible;
            Output.Focus();
            Output.SelectAll();
            AddMenuButton(OutputGrid);
        }

        /// <summary>
        /// Decrypts the string and shows the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptStringButton_Click(object sender, RoutedEventArgs e)
        {
            ShowResult(DPAPILite.Decrypt(InputStringTextBox.Text));
        }
    }
}
