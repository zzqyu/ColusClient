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

namespace ColusClient
{
    public class ItemTwoFragment : BChatFragment
    {
        public ItemTwoFragment()
        {

        }
        public ItemTwoFragment(BluetoothChatService bcs) : base(bcs)
        {

        }

        public static ItemTwoFragment NewInstance(BluetoothChatService bcs)
        {
            ItemTwoFragment fragment = new ItemTwoFragment(bcs);
            return fragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SendMessage("키보드");
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_item_two, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            EditText et = view.FindViewById<EditText>(Resource.Id.invisible_et);
            //et.TextChanged

        }
    }
}