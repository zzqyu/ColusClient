using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System.Text;

namespace ColusClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public partial class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        public BottomNavigationView navigation;


        const string TAG = "MainActivity";

        const int REQUEST_CONNECT_DEVICE_SECURE = 1;
        const int REQUEST_CONNECT_DEVICE_INSECURE = 2;
        const int REQUEST_ENABLE_BT = 3;
        

        string connectedDeviceName = "";
        BluetoothAdapter bluetoothAdapter = null;
        BluetoothChatService chatService = null;

        bool requestingPermissionsSecure, requestingPermissionsInsecure;

        DiscoverableModeReceiver receiver;
        ChatHandler handler;
        WriteListener writeListener;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            string[] title = new string[] {"마우스", "키보드", "PPT모드", "PC기능" };
            int tabIndex = -1;
            if (item.ItemId == Resource.Id.navigation_mouse) tabIndex = (0);
            else if (item.ItemId == Resource.Id.navigation_keyboard) tabIndex = (1);
            else if (item.ItemId == Resource.Id.navigation_ppt) tabIndex = (2);
            else if (item.ItemId == Resource.Id.navigation_pcfunc) tabIndex = (3);
            if (tabIndex != -1)
            {
                Title = title[tabIndex];
                SetFlagDisplay(tabIndex);
                return true;
            }
            return false;
        }
        public void SetFlagDisplay(int tabIndex)
        {
            Fragment fragment = null;
            switch (tabIndex)
            {
                case 0:
                    fragment = ItemOneFragment.NewInstance(chatService); break;
                case 1:
                    fragment = ItemTwoFragment.NewInstance(chatService); break;
                case 2:
                    fragment = ItemThreeFragment.NewInstance(chatService); break;
                case 3:
                    fragment = ItemFourFragment.NewInstance(chatService); break;

            }
            navigation.Menu.GetItem(tabIndex).SetChecked(true);
            
            if (fragment != null)
            {
                FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title = "마우스";
            SetContentView(Resource.Layout.activity_main);
            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            if (savedInstanceState == null)
            {
                Fragment fragment = ItemOneFragment.NewInstance(chatService);
                FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .Commit();

            }
            
            //SetHasOptionsMenu(true);
            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;


            receiver = new DiscoverableModeReceiver();
            receiver.BluetoothDiscoveryModeChanged += (sender, e) =>
            {
                this.InvalidateOptionsMenu();
            };

            if (bluetoothAdapter == null)
            {
                Toast.MakeText(this, "Bluetooth is not available.", ToastLength.Long).Show();
                this.FinishAndRemoveTask();
            }

            writeListener = new WriteListener(this);
            handler = new ChatHandler(this);
        }

        
        
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainmenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        
        protected override void OnStart()
        {
            base.OnStart();
            if (!bluetoothAdapter.IsEnabled)
            {
                var enableIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableIntent, REQUEST_ENABLE_BT);
            }
            else if (chatService == null)
            {
                SetupChat();
            }

            // Register for when the scan mode changes
            var filter = new IntentFilter(BluetoothAdapter.ActionScanModeChanged);
            RegisterReceiver(receiver, filter);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (chatService != null)
            {
                if (chatService.GetState() == BluetoothChatService.STATE_NONE)
                {
                    chatService.Start();
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            var allGranted = grantResults.AllPermissionsGranted();
            if (requestCode == PermissionUtils.RC_LOCATION_PERMISSIONS)
            {
                if (requestingPermissionsSecure)
                {
                    PairWithBlueToothDevice(true);
                }
                if (requestingPermissionsInsecure)
                {
                    PairWithBlueToothDevice(false);
                }

                requestingPermissionsSecure = false;
                requestingPermissionsInsecure = false;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case REQUEST_CONNECT_DEVICE_SECURE:
                    if (Result.Ok == resultCode)
                    {
                        ConnectDevice(data, true);
                    }
                    break;
                case REQUEST_CONNECT_DEVICE_INSECURE:
                    if (Result.Ok == resultCode)
                    {
                        ConnectDevice(data, true);
                    }
                    break;
                case REQUEST_ENABLE_BT:
                    if (Result.Ok == resultCode)
                    {
                        Toast.MakeText(this, "Bluetooth ON", ToastLength.Short).Show();
                        //this.FinishAndRemoveTask();
                        Finish();
                        StartActivity(new Intent(this, this.GetType()));
                    }
                    break;
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.UnregisterReceiver(receiver);
            if (chatService != null)
            {
                chatService.Stop();
            }
        }

        void SetupChat()
        {
            chatService = new BluetoothChatService(handler);
        }
        void SendMessage(string message)
        {
            if (chatService.GetState() != BluetoothChatService.STATE_CONNECTED)
            {
                Toast.MakeText(this, Resource.String.not_connected, ToastLength.Long).Show();
                return;
            }

            if (message.Length > 0)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                chatService.Write(bytes);
                //outStringBuffer.Clear();
            }
        }
        bool HasActionBar()
        {
            if (this == null)
            {
                return false;
            }
            if (this.ActionBar == null)
            {
                return false;
            }
            return true;
        }

        void SetStatus(int resId)
        {
            if (HasActionBar())
            {
                this.ActionBar.SetSubtitle(resId);
            }
        }

        void SetStatus(string subTitle)
        {
            if (HasActionBar())
            {
                this.ActionBar.Subtitle = subTitle;
            }
        }

        void ConnectDevice(Intent data, bool secure)
        {
            var address = data.Extras.GetString(DeviceListActivity.EXTRA_DEVICE_ADDRESS);
            var device = bluetoothAdapter.GetRemoteDevice(address);
            //connect code
            var connectCode = data.Extras.GetString(DeviceListActivity.EXTRA_CONNECT_CODE);
            chatService.Connect(device, secure, connectCode);
        }
        class WriteListener : Java.Lang.Object, TextView.IOnEditorActionListener
        {
            MainActivity host;
            public WriteListener(MainActivity frag)
            {
                host = frag;
            }
            public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
            {
                if (actionId == ImeAction.ImeNull && e.Action == KeyEventActions.Up)
                {
                    host.SendMessage(v.Text);
                }
                return true;
            }
        }
        
        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            bool result = true;
            if (item.ItemId == Resource.Id.secure_connect_scan)
                PairWithBlueToothDevice(true);
            else result = false;
            return result;
        }
        void PairWithBlueToothDevice(bool secure)
        {
            requestingPermissionsSecure = false;
            requestingPermissionsInsecure = false;

            // Bluetooth is automatically granted by Android. Location, OTOH,
            // is considered a "dangerous permission" and as such has to 
            // be explicitly granted by the user.
            if (!this.HasLocationPermissions())
            {
                requestingPermissionsSecure = secure;
                requestingPermissionsInsecure = !secure;
                PermissionUtils.RequestPermissionsForApp(this);
                return;
            }

            var intent = new Intent(this, typeof(DeviceListActivity));
            if (secure)
            {
                StartActivityForResult(intent, REQUEST_CONNECT_DEVICE_SECURE);
            }
            else
            {
                StartActivityForResult(intent, REQUEST_CONNECT_DEVICE_INSECURE);
            }
        }
        

    }
}

