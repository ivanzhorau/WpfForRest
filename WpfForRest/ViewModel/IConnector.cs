using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfForRest.Model;

namespace WpfForRest.ViewModel
{
    interface IConnector
    {
        void setConnString(string conn);
        void doRequest(ILot lot, string method);
        IList<ILot> getList();
    }
}
