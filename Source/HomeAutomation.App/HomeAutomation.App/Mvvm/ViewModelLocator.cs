using System;
using HomeAutomation.App.DependencyInjection;
using Xamarin.Forms;

namespace HomeAutomation.App.Mvvm
{
  public class ViewModelLocator
  {
    public static readonly BindableProperty ViewModelTypeProperty = BindableProperty.CreateAttached("ViewModelType", typeof(Type), typeof(ViewModelLocator), null, propertyChanged: ViewModelTypeChanged);

    private static void ViewModelTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
      bindable.BindingContext = StaticServiceLocator.Current.Get((Type)newValue);
    }

    public static void SetViewModelType(BindableObject element, Type value)
    {
      element.SetValue(ViewModelTypeProperty, value);
    }

    public static Type GetViewModelType(BindableObject element)
    {
      return (Type)element.GetValue(ViewModelTypeProperty);
    }
  }
}