using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Gallerey
{
    /// <summary>
    /// Логика взаимодействия для ImageCard.xaml
    /// </summary>
    public partial class ImageCard : UserControl
    {
        public string path;
        public ImageCard(Image image, string path, string name)
        {
            InitializeComponent();
            ImageElement.Source = image.Source;
            this.path = path;
            ImageName.Text = name;
        }
        private void UserControl_ContextMenuOpening(object sender, RoutedEventArgs e)
        {

        }

        private void ImageElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(path);

        }
    }
}
