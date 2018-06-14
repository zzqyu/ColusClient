using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
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
            SendMessage("멀티미디어");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_item_four, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

            /*
               음소거0xAD, 볼륨감소0xAE, 볼륨증가0xAF
               0xB2
  뒤로이동0x25, 재생/일시정지0xFA/0xB3 or 0x20, 앞으로이동0x27
             */
            int[] idList = new int[] { Resource.Id.bnt0
                                      ,Resource.Id.bnt1
                                      ,Resource.Id.bnt2
                                      ,Resource.Id.bnt3
                                      ,Resource.Id.bnt4
                                      ,Resource.Id.bnt5};
            int[] keyCodeList = new int[] { 0xAD, 0xAE, 0xAF
                                         ,0x25, 0x20, 0x27};
            Button[] btList = new Button[6];
            for(int i = 0; i<6; i++)
            {
                String code = keyCodeList[i].ToString("X");
                btList[i] = view.FindViewById<Button>(idList[i]);
                btList[i].Click += ((s, e) => {
                    SendMessage(code);
                });
                if (i == 4)
                    btList[i].LongClick += (s, e) => SendMessage((0xB2).ToString("X"));
            }

        }
        
    }
}