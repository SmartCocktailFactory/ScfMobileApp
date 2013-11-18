using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScfApp.SimGui {
  /// <summary>
  /// Interaction logic for OrderView.xaml
  /// </summary>
  public partial class OrderView : UserControl {
    #region Members
    private Common.ViewModel.OrderViewModel _OrderModel = new Common.ViewModel.OrderViewModel();
    #endregion

    public OrderView() {
      InitializeComponent();
    }

    public void Reload() {
      this._UpdateOrder(this._OrderModel.CurrentOrder);
    }

    private void ListView_Loaded(object sender, RoutedEventArgs e) {
      this._OrderModel.OnViewModelChanged += _OrderModel_OnViewModelChanged;
    }

    void _OrderModel_OnViewModelChanged(object sender, Common.ViewModel.ViewModelChangedEventArgs e) {
      this.Dispatcher.Invoke(delegate {
        this._UpdateOrder(this._OrderModel.CurrentOrder);
      });
    }

    private void _UpdateOrder(Common.DTO.Order currentOrder) {
      this.lstOrderView.Items.Clear();

      ListViewItem item;
      IList<Common.DTO.Order> pendingOrders = this._OrderModel.Orders;

      foreach (Common.DTO.Order curOrder in pendingOrders) {
        item = new ListViewItem { Content = "drinkd: " + curOrder.DrinkId + ", id: " + curOrder.OrderId + ", status: " + curOrder.OrderStatus };
        if (currentOrder != null && currentOrder.OrderId == curOrder.OrderId) {
          item.Background = Brushes.LightBlue;
        }
        this.lstOrderView.Items.Add(item);
      }
    }
  }
}
