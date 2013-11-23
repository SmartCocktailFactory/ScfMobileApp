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

namespace ScfMobileApp.Android {
  public class DrinkListAdapter : BaseAdapter<Common.DTO.Drink> {
      List<Common.DTO.Drink> items;
      Activity context;
      public DrinkListAdapter(Activity context, List<Common.DTO.Drink> items)
        : base() {
        this.context = context;
        this.items = items;
      }
      public override long GetItemId(int position) {
        return position;
      }
      public override Common.DTO.Drink this[int position] {
        get { return items[position]; }
      }
      public override int Count {
        get { return items.Count; }
      }
      public override View GetView(int position, View convertView, ViewGroup parent) {
        var item = items[position];
        View view = convertView;
        if (view == null) // no view to re-use, create new
          view = context.LayoutInflater.Inflate(Resource.Layout.ListViewDataItems, null);
        view.FindViewById<TextView>(Resource.Id.textItem).Text = item.Name;
        return view;
      }
    }
}