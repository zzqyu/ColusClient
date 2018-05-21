﻿using System;
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
    public class ItemFourFragment : BChatFragment
    {
        public ItemFourFragment()
        {

        }
        public ItemFourFragment(BluetoothChatService bcs) : base(bcs)
        {

        }

        public static ItemFourFragment NewInstance(BluetoothChatService bcs)
        {
            ItemFourFragment fragment = new ItemFourFragment(bcs);
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SendMessage("PC기능");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_item_four, container, false);
        }
    }
}