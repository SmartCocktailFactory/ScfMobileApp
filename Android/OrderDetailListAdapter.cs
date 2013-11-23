using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ScfMobileApp.Android {
  class OrderDetailListAdapter : BaseAdapter<Common.ViewModel.OrderDetails> {
    List<Common.ViewModel.OrderDetails> items;
    Activity context;
    public OrderDetailListAdapter(Activity context, List<Common.ViewModel.OrderDetails> items)
      : base() {
      this.context = context;
      this.items = items;
    }
    public override long GetItemId(int position) {
      return position;
    }
    public override Common.ViewModel.OrderDetails this[int position] {
      get { return items[position]; }
    }
    public override int Count {
      get { return items.Count; }
    }
    public override View GetView(int position, View convertView, ViewGroup parent) {
      var item = items[position];
      View view = convertView;
      if (view == null) {
        // no view to re-use, create new
        view = context.LayoutInflater.Inflate(Resource.Layout.OrderViewDataItem, null);
      }

      view.FindViewById<TextView>(Resource.Id.txtDrinkName).Text = item.DrinkName;
      view.FindViewById<TextView>(Resource.Id.txtOrderId).Text = item.OrderId;
      view.FindViewById<TextView>(Resource.Id.txtOrderState).Text = item.OrderStatus;
      view.FindViewById<TextView>(Resource.Id.txtSecondsToFinish).Text = item.SecondsToFinish;

      if (item.OrderStateId == Common.DTO.StateId.InProgress) {
        view.FindViewById<LinearLayout>(Resource.Id.lstViewOrderDetails).SetBackgroundColor(Color.LimeGreen);
      } else if (item.OrderStateId == Common.DTO.StateId.Completed) {
        view.FindViewById<LinearLayout>(Resource.Id.lstViewOrderDetails).SetBackgroundColor(Color.LightSlateGray);
      }
      return view;
    }
  }
}