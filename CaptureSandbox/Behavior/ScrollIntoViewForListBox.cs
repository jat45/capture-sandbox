using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CaptureSandbox.Behavior
{
  public class ScrollIntoViewForListBox : Behavior<ListBox>
  {
    protected override void OnAttached()
    {
      base.OnAttached();
      AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
    }

    private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var listBox = (sender as ListBox);
      if (listBox == null)
      {
        return;
      }
      if (listBox.SelectedItem == null)
      {
        return;
      }
      listBox.Dispatcher.BeginInvoke(DoAction(listBox));
    }

    private Action DoAction(ListBox listBox)
    {
      return () =>
      {
        listBox.UpdateLayout();
        if (listBox.SelectedItem != null)
        {
          listBox.ScrollIntoView(listBox.SelectedItem);
        }
      };
    }

    protected override void OnDetaching()
    {
      base.OnDetaching();
      AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
    }
  }
}