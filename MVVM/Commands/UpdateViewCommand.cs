using UBB_SE_2024_Gaborment.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UBB_SE_2024_Gaborment.MVVM.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Feed")
            {
                viewModel.SelectedViewModel = new FeedViewModel();
            }
            else if (parameter.ToString() == "Friend")
            {
                viewModel.SelectedViewModel = new FriendViewModel();
            }
        }
    }
}
