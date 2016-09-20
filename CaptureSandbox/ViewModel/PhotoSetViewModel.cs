using System.Collections.Generic;
using System.Linq;
using CaptureSandbox.Model;
using GalaSoft.MvvmLight;

namespace CaptureSandbox.ViewModel
{
  public sealed class PhotoSetViewModel : ViewModelBase
  {
    private PhotoViewModel selectedPhotoVm;
    private string name;
    private IEnumerable<PhotoViewModel> photoVMs;

    public PhotoSetViewModel(string name, IEnumerable<PhotoModel> photos)
    {
      this.name = name;
      photoVMs = photos.Select(e => new PhotoViewModel(e)).ToArray();
      foreach (var photoVM in photoVMs)
      {
        photoVM.PropertyChanged += (sender, args) =>
        {
          var vm = (PhotoViewModel) sender;
          if (args.PropertyName == "ImagePath")
          {
            SelectedPhotoVM = vm;
          }
        };
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        name = value;
        RaisePropertyChanged();
      }
    }

    public IEnumerable<PhotoViewModel> PhotoVMs
    {
      get { return photoVMs; }
      set
      {
        photoVMs = value;
        RaisePropertyChanged();
      }
    }

    public PhotoViewModel SelectedPhotoVM
    {
      get { return selectedPhotoVm; }
      set
      {
        selectedPhotoVm = value;
        RaisePropertyChanged();
      }
    }
  }
}