using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MindfulnessMeditationApp
{
    public partial class MainWindow : Window
    {
        private bool _createdFile;
        private string _filePath;

        public MainWindow()
        {
            InitializeComponent();

            KeyDown += MainWindow_KeyDown;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

            textBox.KeyDown += TextBox_KeyDown;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StoreNote();
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            StoreNote();
        }

        private void StoreNote()
        {
            string text = textBox.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (!_createdFile)
                {
                    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Thoughts";
                    Directory.CreateDirectory(folder);
                    _filePath = folder + "\\" + "Thoughts_" + DateTime.Now.ToString("yyyy.MM.dd HH-mm-ss") + ".txt";
                    _createdFile = true;
                }

                using (var fileStream = File.AppendText(_filePath))
                {
                    fileStream.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " " + text);
                }
            }

            textBox.Text = string.Empty;
        }

    }
}
