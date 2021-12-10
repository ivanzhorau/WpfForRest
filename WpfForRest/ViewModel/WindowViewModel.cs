using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfForRest.Model;

namespace WpfForRest.ViewModel
{
    class WindowViewModel : INotifyPropertyChanged
    {//адразу я хацеў зрабіць так каб у кодзе ўсё было прыгожа, але цяпер трэба зрабіць так каб працавала
        private ILot selectedLot;
        private ObservableCollection<ILot> lots;
        private IConnector connect;
        public ObservableCollection<ILot> Lots
        {
            get { return lots; }
            set
            {
                lots = value;
                OnPropertyChanged("Lots");
            }
        }


        public ILot SelectedLot
        {
            get { return selectedLot; }
            set
            {
                selectedLot = value;
                OnPropertyChanged("SelectedLot");
            }
        }


        private RelayCommand addLot;
        public RelayCommand AddLot
        {
            get
            {
                return addLot ??
                  (addLot = new RelayCommand(obj =>
                  {
                      Lot lot = new Lot();
                      connect.doRequest(lot, "POST");
                      Thread.Sleep(1000);
                      Lots = new ObservableCollection<ILot>(connect.getList());
                      SelectedLot = lot;
                  }));
            }
        }
        private RelayCommand delLot;
        public RelayCommand DelLot
        {
            get
            {
                return delLot ??
                  (delLot = new RelayCommand(obj =>
                  {
                      connect.doRequest(SelectedLot, "DELETE");
                      Thread.Sleep(1000);
                      Lots = new ObservableCollection<ILot>(connect.getList());
                      SelectedLot = Lots[0];
                  }));
            }
        }
        private RelayCommand changeLot;
        public RelayCommand ChangeLot
        {
            get
            {
                return changeLot ??
                  (changeLot = new RelayCommand(obj =>
                  {
                      connect.doRequest(SelectedLot, "PUT");
                      Thread.Sleep(1000);
                      connect.getList();
                  }));
            }
        }



        public WindowViewModel()
        {
            connect = new ApiConnector();
            IList<ILot> lotArr = connect.getList();
            Lots = new ObservableCollection<ILot>(lotArr);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
