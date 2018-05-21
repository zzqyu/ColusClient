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
    public class ItemThreeFragment : BChatFragment
    {
        public ItemThreeFragment()
        {

        }
        public ItemThreeFragment(BluetoothChatService bcs) : base(bcs)
        {

        }

        public static ItemThreeFragment NewInstance(BluetoothChatService bcs)
        {
            ItemThreeFragment fragment = new ItemThreeFragment(bcs);
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SendMessage("PPT");
        }

        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_item_three, container, false);
        }
    }
}