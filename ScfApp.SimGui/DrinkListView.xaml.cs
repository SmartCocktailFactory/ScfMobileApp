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
  /// Interaction logic for DrinkListView.xaml
  /// </summary>
  public partial class DrinkListView : UserControl {
    #region Members
    private Common.ViewModel.DrinkViewModel _DrinkModel = new Common.ViewModel.DrinkViewModel();
    private Common.ViewModel.OrderViewModel _OrderModel = new Common.ViewModel.OrderViewModel();
    #endregion

    public DrinkListView() {
      InitializeComponent();
      this._DrinkModel.OnViewModelChanged += _Model_OnViewModelChanged;
    }

    #region Public methods
    public void Reload() {
      this._UpdateDrinkNames(this._DrinkModel.DrinkNames);
    }
    #endregion

    #region Model event handler
    void _Model_OnViewModelChanged(object sender, Common.ViewModel.ViewModelChangedEventArgs e) {
      this.Dispatcher.Invoke(delegate {
        this._UpdateDrinkNames(this._DrinkModel.DrinkNames);
      });
    }
    #endregion

    #region Private methods
    private void _UpdateDrinkNames(IList<string> drinkNames) {
      this.lstView.Items.Clear();
      foreach (string name in drinkNames) {
        this.lstView.Items.Add(new ListViewItem { Content = name });
      }
    }
    #endregion

    private void btnOrder_Click(object sender, RoutedEventArgs e) {
      int selectedIndex = this.lstView.SelectedIndex;
      if (selectedIndex < 0) {
        return;
      }
      this._OrderModel.OrderDrink(this._DrinkModel.Drinks[selectedIndex].DrinkId);
    }
  }
}
