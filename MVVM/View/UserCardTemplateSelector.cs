using System;
using System.Windows;
using System.Windows.Controls;
using UBB_SE_2024_Gaborment.MVVM.ViewModel;

namespace UBB_SE_2024_Gaborment.MVVM.View
{
    public class UserCardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalUserCardTemplate { get; set; }
        public DataTemplate BlockedUserCardTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var viewModel = (FriendViewModel)((FrameworkElement)container).DataContext;
            if (viewModel.CurrentState == "Blocked")
            {
                return BlockedUserCardTemplate;
            }
            else
            {
                return NormalUserCardTemplate;
            }
        }
    }
}