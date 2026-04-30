using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Diffusion.Toolkit.Services;

namespace Diffusion.Toolkit
{
    public partial class RemoveUnavailableFoldersWindow : BorderlessWindow
    {
        public RemoveUnavailableFoldersModel Model { get; }

        public RemoveUnavailableFoldersWindow()
        {
            InitializeComponent();

            Model = new RemoveUnavailableFoldersModel
            {
                ShowUnavailableRootFolders = false
            };

            LoadImagePaths(false);

            Model.PropertyChanged += ModelOnPropertyChanged;

            DataContext = Model;
        }

        private void LoadImagePaths(bool showUnavailable)
        {
            var paths = ServiceLocator.FolderService.RootFolders.Select(p => new ImageFileItem()
            {
                Path = p.Path,
                Recursive = p.Recursive,
                IsUnavailable = !Directory.Exists(p.Path)
            })
            .Where(p => showUnavailable || !p.IsUnavailable);

            Model.ImagePaths = new ObservableCollection<ImageFileItem>(paths);

            foreach (var item in Model.ImagePaths)
            {
                item.PropertyChanged += ItemOnPropertyChanged;
            }
        }

        private void ModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.ShowUnavailableRootFolders))
            {
                LoadImagePaths(Model.ShowUnavailableRootFolders);
            }

            Model.IsStartEnabled = Model.ImagePaths.Any(p => p.IsSelected);
        }

        private void ItemOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Model.IsStartEnabled = Model.ImagePaths.Any(p => p.IsSelected);
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
