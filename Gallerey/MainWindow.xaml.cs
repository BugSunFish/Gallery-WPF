using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using MessageBox = System.Windows.MessageBox;

namespace Gallerey
{
    public class FolderPatch
    {
        public int id;
        public string path;
        public bool visible;
        public FolderPatch(int id, string path, bool visible)
        {
            this.id = id;
            this.path = path;
            this.visible = visible;
        }
        public void SetVisible(bool visible)
        {
            this.visible = visible;
        }
    }
    public partial class MainWindow : Window
    {
        static public List<FolderPatch> folders = new List<FolderPatch>();
        public MainWindow()
        {
            InitializeComponent();
        }
        public void AddFolder(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog() { IsFolderPicker = true };
            dlg.Title = "Выберите папку с изображениями";
            dlg.IsFolderPicker = true;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dlg.FileName;
                string folderName = Path.GetFileName(folder);
                int id = 0;
                foreach(Folder item in ImageFolders.Items) 
                {
                   if(item.id >= id)
                    {
                        item.id = id;
                        id++;
                    }
                }
                Folder Folder = new Folder(id, folderName);
                ImageFolders.Items.Add(Folder);
                folders.Add(new FolderPatch(id, folder, true));
                UpdateImages(folders);
            }
        }
        public void ChangeVisibleImages(int id)
        {
            FolderPatch temp = GetFolder(id);
            temp.SetVisible(!temp.visible);
            UpdateImages(folders);
        }
        public void DeleteImages(int id)
        {
            ImageFolders.Items.RemoveAt(id);
            folders.Remove(GetFolder(id));
            UpdateImages(folders);
        }
        static public FolderPatch GetFolder(int id)
        {
            foreach (var item in folders)
            {
                if (id == item.id)
                {
                    return item;
                }
            }
            return null;
        }
        private void UpdateImages(List<FolderPatch> folders)
        {
            imageWrapPanel.Children.Clear();
            foreach (var item in folders)
            {
                if(item.visible)
                    LoadImagesFromFolder(item.path);
            }
        }
        private void LoadImagesFromFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg"); // Измените расширение файла, если ваши изображения имеют другое расширение

                foreach (string imagePath in imageFiles)
                {
                    BitmapImage bitmapImage = new BitmapImage(new System.Uri(imagePath));
                    Image image = new Image();
                    image.Source = bitmapImage;
                    image.Width = 200;
                    image.Height = 200;
                    ImageCard imageCard = new ImageCard(image, imagePath, Path.GetFileName(imagePath));
                    imageWrapPanel.Children.Add(imageCard);
                }
            }
            else
            {
                MessageBox.Show("Папка не существует");
            }
        }
    }
}
