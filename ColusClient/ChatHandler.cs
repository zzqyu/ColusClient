using System.Text;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace ColusClient
{
    public partial class MainActivity
    {
        /// <summary>
        /// Handles messages that come back from the ChatService.
        /// </summary>
        /// 
        
        class ChatHandler : Handler
        {
            MainActivity chatFrag;
            public ChatHandler(MainActivity frag)
            {
                chatFrag = frag;

            }
            public override void HandleMessage(Message msg)
            {
                switch (msg.What)
                {
                    case Constants.MESSAGE_STATE_CHANGE:
                        switch (msg.What)
                        {
                            case BluetoothChatService.STATE_CONNECTED:
                                chatFrag.SetStatus(chatFrag.GetString(Resource.String.title_connected_to, chatFrag.connectedDeviceName));
                                //chatFrag.conversationArrayAdapter.Clear();
                                break;
                            case BluetoothChatService.STATE_CONNECTING:
                                chatFrag.SetStatus(Resource.String.title_connecting);
                                break;
                            case BluetoothChatService.STATE_LISTEN:
                                chatFrag.SetStatus(Resource.String.not_connected);
                                break;
                            case BluetoothChatService.STATE_NONE:
                                chatFrag.SetStatus(Resource.String.not_connected);
                                break;
                        }
                        break;
                    case Constants.MESSAGE_WRITE:
                        var writeBuffer = (byte[])msg.Obj;
                        var writeMessage = Encoding.UTF8.GetString(writeBuffer);
                        //chatFrag.conversationArrayAdapter.Add($"Me:  {writeMessage}");
                        break;
                    case Constants.MESSAGE_READ:
                        var readBuffer = (byte[])msg.Obj;
                        var readMessage = Encoding.UTF8.GetString(readBuffer);
                        Toast.MakeText(chatFrag, $"{chatFrag.connectedDeviceName}: {readMessage}", ToastLength.Short).Show();
                        if (readMessage.Contains( "마우스"))
                            chatFrag.SetFlagDisplay(0);
                        else if (readMessage.Contains("키보드"))
                            chatFrag.SetFlagDisplay(1);
                        else if (readMessage.Contains("PPT"))
                            chatFrag.SetFlagDisplay(2);
                        else if (readMessage.Contains("PC기능"))
                            chatFrag.SetFlagDisplay(3);

                        //chatFrag.conversationArrayAdapter.Add($"{chatFrag.connectedDeviceName}: {readMessage}");
                        break;
                    case Constants.MESSAGE_DEVICE_NAME:
                        chatFrag.connectedDeviceName = msg.Data.GetString(Constants.DEVICE_NAME);
                        if (chatFrag != null)
                        {
                            Toast.MakeText(chatFrag, $"Connected to {chatFrag.connectedDeviceName}.", ToastLength.Short).Show();
                        }
                        break;
                    case Constants.MESSAGE_TOAST:
                        break;
                }
            }
        }
    }
}
