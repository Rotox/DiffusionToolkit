using System.Collections.ObjectModel;
using Diffusion.Toolkit.Models;

namespace Diffusion.Toolkit;

public class RemoveUnavailableFoldersModel : BaseNotify
{
    public bool ShowUnavailableRootFolders
    {
        get;
        set => SetField(ref field, value);
    }

    public ObservableCollection<ImageFileItem> ImagePaths
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsStartEnabled
    {
        get;
        set => SetField(ref field, value);
    }
}
