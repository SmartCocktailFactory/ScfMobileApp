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
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      Common.Model.ModelFactory.Instance().Dispose();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
      this.cmpSignIn.OnConnecting += cmpSignIn_OnConnecting;
      this.cmpSignIn.OnConnected += cmpSignIn_OnConnected;
    }

    void cmpSignIn_OnConnecting(object sender, EventArgs e) {
      this.tabDrinkList.Visibility = System.Windows.Visibility.Hidden;
    }

    void cmpSignIn_OnConnected(object sender, EventArgs e) {
      this.Dispatcher.Invoke(delegate {
        this.tabDrinkList.Visibility = System.Windows.Visibility.Visible;
      });
    }

    private void tabDrinkList_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
      if (this.IsVisible == true) {
        this.cmpDrinkListView.Reload();
      }
    }
  }
}
