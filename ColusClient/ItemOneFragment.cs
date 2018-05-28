using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ColusClient
{

    public class ItemOneFragment : BChatFragment, View.IOnTouchListener, View.IOnLongClickListener
    {
        public GestureDetector detector;
        public GestureDetector detector2;

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
            tv.SetOnTouchListener(this);
            detector = new GestureDetector(new GestureTouch(this));
            tv.SetOnLongClickListener(this);


            View sv = view.FindViewById<View>(Resource.Id.scrview);
            sv.SetOnTouchListener(this);
            detector2 = new GestureDetector(new GestureTouch2(this));
            sv.SetOnLongClickListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (v.Id == Resource.Id.touchview)
                return detector.OnTouchEvent(e);
            else if (v.Id == Resource.Id.scrview)
                return detector2.OnTouchEvent(e);
            return false;
        }



        public bool OnLongClick(View v)
        {
            return false;
        }
    }
    public class GestureTouch : GestureDetector.SimpleOnGestureListener
    {
        ItemOneFragment frag;

        public GestureTouch(ItemOneFragment frag)
        {
            this.frag = frag;
        }

        public override bool OnDoubleTap(MotionEvent e) //더블 클릭
        {
            int stateBit = 4;
            if (frag.IsOnBluetooth())
                frag.SendMessage(stateBit.ToString());
            return false;
        }

        public override bool OnDoubleTapEvent(MotionEvent e)
        {
            return false;
        }


        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return false;
        }

        public override void OnLongPress(MotionEvent e)
        {
            int stateBit = 3;
            if (frag.IsOnBluetooth())
                frag.SendMessage(stateBit.ToString());
        }

        public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) //MOVE
        {
            int stateBit = 2;

            if (frag.IsOnBluetooth())
            {
                frag.SendMessage(stateBit + "," + (int)distanceX + "," + (int)distanceY + ",");
                Thread.Sleep(20);
            }
            return false;
        }

        public override void OnShowPress(MotionEvent e)
        {
        }

        public override bool OnSingleTapConfirmed(MotionEvent e)
        {
            int dpWidth;
            DisplayMetrics dm = frag.Context.ApplicationContext.Resources.DisplayMetrics;
            dpWidth = dm.WidthPixels;

            int halfDisplay = (dpWidth / 2);

            if ((int)e.GetX() < halfDisplay) //좌클릭
            {
                int stateBit = 0;
                if (frag.IsOnBluetooth())
                    frag.SendMessage(stateBit.ToString());
                return false;
            }
            else //우클릭
            {
                int stateBit = 1;
                if (frag.IsOnBluetooth())
                    frag.SendMessage(stateBit.ToString());
                return false;
            }

        }

        public override bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
    }

    public class GestureTouch2 : GestureDetector.SimpleOnGestureListener
    {
        ItemOneFragment frag;

        public GestureTouch2(ItemOneFragment frag)
        {
            this.frag = frag;
        }

        public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) //MOVE
        {
            int stateBitDown = 10;
            int stateBitUp = 20;
            int tempY = (int)e1.GetY();

            if (tempY < (int)e2.GetY())
            {
                //scroll down
                frag.SendMessage(stateBitDown + ",");
            }
            else if (tempY > (int)e2.GetY())
            {
                //scroll up
                frag.SendMessage(stateBitUp + ",");
            }

            return false;
        }
    }
}