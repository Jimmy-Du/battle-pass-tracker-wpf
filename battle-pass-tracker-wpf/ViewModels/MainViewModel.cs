using battle_pass_tracker_wpf.Commands;
using System.Collections.Generic;

namespace battle_pass_tracker_wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand NavigateCommand { get; set; }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel 
        { 
            get { return currentViewModel; }
            set 
            { 
                currentViewModel = value; 
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private List<ViewModelBase> viewModels;
        public List<ViewModelBase> ViewModels
        {
            get
            {
                if (viewModels == null)
                {
                    viewModels = new List<ViewModelBase>();
                }

                return viewModels;
            }
        }



        public MainViewModel()
        {
            ViewModels.Add(new BattlePassViewModel());
            ViewModels.Add(new EventViewModel());
            ViewModels.Add(new GameSelectionViewModel());

            CurrentViewModel = ViewModels[0];
            NavigateCommand = new RelayCommand(ChangeViewModel, NavigateCanUse);
        }



        // Function:    ChangeViewModel()
        // Description: changes the view of the application to the parameter
        //              passed in
        // Parameters:  object newViewModel: an object containing the new
        //              view model to switch to
        // Return:      N/A
        public void ChangeViewModel(object newViewModel)
        {
            // if the new viewl model is equal to the first view model, the current view model
            // is switched to the battle pass view model
            if ((ViewModelBase)newViewModel == ViewModels[0])
            {
                CurrentViewModel = new BattlePassViewModel();
            }
            // if the new viewl model is equal to the second view model, the current view model
            // is switched to the event view model
            else if ((ViewModelBase)newViewModel == ViewModels[1])
            {
                CurrentViewModel = new EventViewModel();
            }
            // if the new viewl model is equal to the third view model, the current view model
            // is switched to the game selection view model
            else if ((ViewModelBase)newViewModel == ViewModels[2])
            {
                CurrentViewModel = new GameSelectionViewModel();
            }
        }



        // Function:    NavigateCanUse()
        // Description: determines if the Navigate Command can be executed
        // Parameters:  object newViewModel: an object containing the new
        //              view model to switch to
        // Return:      a boolean indiciting if the Navigate Command can be executed
        public static bool NavigateCanUse(object newViewModel)
        {
            return true;
        }
    }
}
