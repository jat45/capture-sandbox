using CaptureSandbox.Model;
using GalaSoft.MvvmLight;

namespace CaptureSandbox.ViewModel
{
  public class PhotoViewModel : ViewModelBase
  {
    private readonly PhotoModel source;

    public PhotoViewModel(PhotoModel source)
    {
      this.source = source;
    }

    public string Name
    {
      get { return source.Name; }
    }

    public string ImagePath
    {
      get { return source.ImagePath; }
      set
      {
        source.ImagePath = value;
        RaisePropertyChanged();
      }
    }

    public string Path
    {
      get { return source.Path; }
    }
  }
}