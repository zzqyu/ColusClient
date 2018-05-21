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
    public class BChatFragment : Fragment
    {
        private BluetoothChatService chatService = null;
        public BChatFragment()
        {

        }
        public BChatFragment(BluetoothChatService bcs)
        {
            chatService = bcs;
        }

        public bool IsOnBluetooth()
        {
            if (chatService == null || chatService.GetState() != BluetoothChatService.STATE_CONNECTED)
                return false;
            else return true;
        }


        public void SendMessage(string message)
        {
            if (!IsOnBluetooth())
            {
                Toast.MakeText(this.Activity, Resource.String.not_connected, ToastLength.Long).Show();
                return;
            }

            if (message.Length > 0)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                chatService.Write(bytes);
                //outStringBuffer.Clear();
            }
        }
    }
}