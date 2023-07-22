using System;
using System.Collections.Generic;
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

namespace Gallerey
{
    /// <summary>
    /// Логика взаимодействия для Folder.xaml
    /// </summary>
    public partial class Folder : UserControl
    {
        public int id;
        public Folder(int id, string name)
        {
            InitializeComponent();
            this.id = id;
            FolderName.Text = name;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void DeleteFolder(object sender, RoutedEventArgs e)
        {
            var MainWindowObject = (Application.Current.MainWindow as MainWindow);
            MainWindowObject.DeleteImages(id);
        }
        public void ChangeVisibleFolder(object sender, RoutedEventArgs e)
        {
            var MainWindowObject = (Application.Current.MainWindow as MainWindow);
            MainWindowObject.ChangeVisibleImages(id);
        }

        private void UserControl_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var MainWindowObject = (Application.Current.MainWindow as MainWindow);
            ButtonChanger.Header = !MainWindow.GetFolder(id).visible ? "Включить папку" : "Выключить папку";
        }
    }
}
