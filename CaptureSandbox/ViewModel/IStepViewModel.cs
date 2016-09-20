using System.Windows.Input;

namespace CaptureSandbox.ViewModel
{
  public interface IStepViewModel
  {
    ICommand Back { get; }
    ICommand Next { get; }
  }
}