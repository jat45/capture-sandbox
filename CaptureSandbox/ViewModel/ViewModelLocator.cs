using CaptureSandbox.Model;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CaptureSandbox.ViewModel
{
  public class ViewModelLocator
  {
    static ViewModelLocator()
    {
      ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
      SimpleIoc.Default.Register<IDatabase, LocalDatabase>();
      SimpleIoc.Default.Register<CaptureViewModel>();
      SimpleIoc.Default.Register<ChooseSleeveViewModel>();
      SimpleIoc.Default.Register<ApplicationViewModel>();
    }

    public ApplicationViewModel ApplicationVM
    {
      get { return ServiceLocator.Current.GetInstance<ApplicationViewModel>(); }
    }

    public ChooseSleeveViewModel ChooseSleeveVM
    {
      get { return ServiceLocator.Current.GetInstance<ChooseSleeveViewModel>(); }
    }

    public CaptureViewModel CaptureVM
    {
      get { return ServiceLocator.Current.GetInstance<CaptureViewModel>(); }
    }
  }
}