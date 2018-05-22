using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace ColusClient
{
    public class ItemOneFragment : BChatFragment, IOnTouchListener
    {
        public ItemOneFragment()
        {

        }
        public ItemOneFragment(BluetoothChatService bcs) : base(bcs)
        {
           
        }
        
        public static ItemOneFragment NewInstance(BluetoothChatService bcs)
        {
            ItemOneFragment fragment = new ItemOneFragment(bcs);
            return fragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SendMessage("마우스");
            
        }

        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_item_one, container, false);
        }

        

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            View tv = view.FindViewById<View>(Resource.Id.touchview);
            tv.Touch += tv_TouchAction;
            tv.SetOnTouchListener(this);
            
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            throw new NotImplementedException();
        }
        private void tv_TouchAction(object sender, View.TouchEventArgs e)
        {
            if (IsOnBluetooth())
                SendMessage(e.Event.GetX() + ", " + e.Event.GetY());
        }
        
    }
}