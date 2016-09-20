using System.Windows.Input;
using CaptureSandbox.Message;
using CaptureSandbox.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CaptureSandbox.ViewModel
{
  public class ChooseSleeveViewModel : ViewModelBase, IStepViewModel
  {
    private readonly IDatabase database;
    private bool? isSleeved;

    public ChooseSleeveViewModel(IDatabase database)
    {
      this.database = database;
      Messenger.Default.Register<StartNewGarment>(this,
        _ => { IsSleeved = null; });
    }

    public ICommand AddSleeves
    {
      get
      {
        return new RelayCommand(
          () =>
          {
            database.AddSleeves();
            IsSleeved = true;
            NextPage();
          },
          () => IsSleeved != true);
      }
    }

    public ICommand RemoveSleeves
    {
      get
      {
        return new RelayCommand(
          () =>
          {
            database.RemoveSleeves();
            IsSleeved = false;
            NextPage();
          },
          () => IsSleeved != false);
      }
    }


    public bool? IsSleeved
    {
      get { return isSleeved; }
      private set
      {
        isSleeved = value;
        RaisePropertyChanged();
        if (value.HasValue)
        {
          Messenger.Default.Send(new SleevesChanged());
        }
      }
    }

    public ICommand Back
    {
      get { return new RelayCommand(() => { }, () => false); }
    }

    public ICommand Next
    {
      get { return new RelayCommand(NextPage, () => IsSleeved != null); }
    }

    private void NextPage()
    {
      Messenger.Default.Send(new NextPage());
    }
  }
}