using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using CaptureSandbox.Message;
using CaptureSandbox.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CaptureSandbox.ViewModel
{
  public class CaptureViewModel : ViewModelBase, IStepViewModel
  {
    private readonly IDatabase database;
    private PhotoSetViewModel selectedPhotoSetVM;
    private PhotoSetViewModel[] photoSetVMs;
    private bool captureInProgress;

    public CaptureViewModel(IDatabase database)
    {
      this.database = database;
      Reset();
      Messenger.Default.Register<StartNewGarment>(this,
        _ =>
        {
          if (SelectedPhotoSetVM != null)
          {
            SelectedPhotoSetVM.SelectedPhotoVM = null;
            SelectedPhotoSetVM = null;
          }
          Reset();
        });
      Messenger.Default.Register<SleevesChanged>(this, _ => Reset());
    }


    private void Reset()
    {
      var photos = database.GetPhotos();
      PhotoSetVMs = photos.Select(e => new PhotoSetViewModel(e.Name, e.Photos)).ToArray();
    }

    public PhotoSetViewModel[] PhotoSetVMs
    {
      get { return photoSetVMs; }
      private set
      {
        photoSetVMs = value;
        RaisePropertyChanged();
      }
    }

    public PhotoSetViewModel SelectedPhotoSetVM
    {
      get { return selectedPhotoSetVM; }
      set
      {
        selectedPhotoSetVM = value;
        RaisePropertyChanged();
      }
    }

    public bool CaptureInProgress
    {
      get { return captureInProgress; }
      private set
      {
        captureInProgress = value;
        RaisePropertyChanged();
      }
    }

    public ICommand Capture
    {
      get { return new RelayCommand(CaptureAction, CanExecuteCapture); }
    }

    private void CaptureAction()
    {
      var worker = new BackgroundWorker();
      worker.DoWork += (sender, args) =>
      {
        foreach (var photoVM in SelectedPhotoSetVM.PhotoVMs)
        {
          Thread.Sleep(500);
          photoVM.ImagePath = photoVM.Path;
        }
      };
      worker.RunWorkerCompleted += (sender, args) =>
      {
        CaptureInProgress = false;
        CommandManager.InvalidateRequerySuggested();
      };
      CaptureInProgress = true;
      worker.RunWorkerAsync();
    }

    private bool CanExecuteCapture()
    {
      return !CaptureInProgress && SelectedPhotoSetVM != null;
    }

    private void NextAction()
    {
      database.Publish();
      Messenger.Default.Send(new StartNewGarment());
    }

    public ICommand Back
    {
      get { return new RelayCommand(() => Messenger.Default.Send(new PreviousPage())); }
    }

    public ICommand Next
    {
      get { return new RelayCommand(NextAction, CanExecuteNext); }
    }

    private bool CanExecuteNext()
    {
      return !CaptureInProgress && PhotoSetVMs.SelectMany(e => e.PhotoVMs).All(e => e.ImagePath != null);
    }
  }
}