using System;
using System.Windows.Input;
using CaptureSandbox.Message;
using CaptureSandbox.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace CaptureSandbox.ViewModel
{
  public class ApplicationViewModel : ViewModelBase
  {
    private readonly IDatabase database;
    private IStepViewModel currentStepViewModel;

    public ApplicationViewModel(IDatabase database)
    {
      this.database = database;
      Reset();
      Messenger.Default.Register<StartNewGarment>(this, _ => Reset());
      Messenger.Default.Register<NextPage>(this,
        _ =>
        {
          if (CurrentStepViewModel is CaptureViewModel)
          {
            return;
          }
          CurrentStepViewModel = ServiceLocator.Current.GetInstance<CaptureViewModel>();
        });
      Messenger.Default.Register<PreviousPage>(this,
        _ =>
        {
          if (CurrentStepViewModel is ChooseSleeveViewModel)
          {
            return;
          }
          CurrentStepViewModel = ServiceLocator.Current.GetInstance<ChooseSleeveViewModel>();
        });
    }

    private void Reset()
    {
      database.CreateGarment(Guid.NewGuid());
      CurrentStepViewModel = ServiceLocator.Current.GetInstance<ChooseSleeveViewModel>();
    }

    public ICommand NewGarment
    {
      get { return new RelayCommand(() => Messenger.Default.Send(new StartNewGarment())); }
    }

    public IStepViewModel CurrentStepViewModel
    {
      get { return currentStepViewModel; }
      set
      {
        currentStepViewModel = value;
        RaisePropertyChanged();
      }
    }
  }
}