using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace ColusClient
{
    public class ItemTwoFragment : BChatFragment
    {
        private List<KeyMap> keyMapList;
        private int[] numItemOfRows;
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
            SetKeycode();
            LinearLayout keyboardBox = view.FindViewById<LinearLayout>(Resource.Id.keyboead_box);
            int idx = 0;
            foreach (int numItem in numItemOfRows)
            {
                int btHeightDp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 40, Activity.Resources.DisplayMetrics);
                LinearLayout rowLinearLayout = new LinearLayout(this.Activity);
                LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent, 1f);
                layoutParams.Gravity = GravityFlags.Center;
                rowLinearLayout.LayoutParameters = layoutParams;
                rowLinearLayout.SetPadding(1,1,1,1);
                rowLinearLayout.Orientation = Orientation.Horizontal;
                for (int i=0; i< numItem; i++)
                {
                    Button btKey = new Button(this.Activity);
                    btKey.LayoutParameters = new LinearLayout.LayoutParams(0, btHeightDp, keyMapList[idx].Weight+0f);
                    btKey.TextSize = TypedValue.ApplyDimension(ComplexUnitType.Sp, 4, Activity.Resources.DisplayMetrics);
                    btKey.Text = keyMapList[idx].KeyName;
                    btKey.SetTextColor(Color.LightGray);
                    btKey.SetBackgroundResource(Resource.Drawable.key_button);
                    btKey.Click += ((sender, e) => SendMessage(keyMapList[idx].KeyCode.ToString("X")));
                    rowLinearLayout.AddView(btKey);
                    idx++;
                }
                keyboardBox.AddView(rowLinearLayout);
            }

        }

        private void SetKeycode()
        {
            keyMapList = new List<KeyMap>();
            numItemOfRows = new int[] { 10, 10, 11, 11, 11, 10, 9, 5};
            //10개 ESC, INSERT , DEL, HOME, END, pageup pagedown, PRINTScr, F11, F12
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Esc",0x1B), new KeyMap("Ins", 0x2D), new KeyMap("Del", 0x2E),
                new KeyMap("Home", 0x24), new KeyMap("End", 0x23), new KeyMap("Pg Dn", 0x21), new KeyMap("Pg Up", 0x22),
                new KeyMap("PrtSc", 0x2A), new KeyMap("F11", 0x7A), new KeyMap("F12", 0x7B)});
            //10개 F1~F10
            for (int i = 0x70; i < 0x7A; i++)
                keyMapList.Add(new KeyMap("F"+(i- 0x70 + 1), i));
            //11개 1 ~ 9, 0, backSpace 
            keyMapList.AddRange(new KeyMap[] { new KeyMap("!\n1",0x31), new KeyMap("@\n2", 0x32), new KeyMap("#\n3", 0x33),
                new KeyMap("$\n4", 0x34), new KeyMap("%\n5", 0x35), new KeyMap("^\n6", 0x36), new KeyMap("&\n7", 0x37),
                new KeyMap("*\n8", 0x38), new KeyMap("(\n9", 0x39), new KeyMap(")\n0", 0x30), new KeyMap("←", 0x08)});

            //11개  `, -, =, [, ], \, ;, ', <, >, ?
            keyMapList.AddRange(new KeyMap[] { new KeyMap("~\n`",0xC0), new KeyMap("_\n-", 0xBD), new KeyMap("+\n=", 0xBB),
                new KeyMap("{\n[", 0xDB), new KeyMap("}\n]", 0xDD), new KeyMap("|\n\\", 0xDC), new KeyMap(":\n;", 0xBA),
                new KeyMap("\"\n'", 0xDE), new KeyMap("<\n,", 0xBC), new KeyMap(">\n.", 0xBE), new KeyMap("?\n/", 0xBF)});
            //11개 tab, q, w, e, r, t, y, u, i, o, p
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Tab",0x09), new KeyMap("Qㅃ\n ㅂ", 0x51), new KeyMap("Wㅉ\n ㅈ", 0x57),
                new KeyMap("Eㄸ\n ㄷ", 0x45), new KeyMap("Rㄲ\n ㄱ", 0x52), new KeyMap("Tㅆ\n ㅅ", 0x54), new KeyMap("Y\nㅛ", 0x59),
                new KeyMap("U\nㅕ", 0x55), new KeyMap("I\nㅑ", 0x49), new KeyMap("Oㅒ\n ㅐ", 0x4F), new KeyMap("Pㅖ\n ㅔ", 0x50)});
            //10개 CapsLock,a, s, d ,f, g, h, j, k , l
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Caps\nLock",0x14, 2), new KeyMap("A\nㅁ", 0x51), new KeyMap("S\nㄴ", 0x57),
                new KeyMap("D\nㅇ", 0x45), new KeyMap("F\nㄹ", 0x52), new KeyMap("G\nㅎ", 0x54), new KeyMap("H\nㅗ", 0x59),
                new KeyMap("J\nㅓ", 0x55), new KeyMap("K\nㅏ", 0x49), new KeyMap("L\nㅣ", 0x4F) });
            //9개 Shift, z, x, c, v, b, n, m, Enter
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Shift",0x10, 2), new KeyMap("Z\nㅋ", 0x5A), new KeyMap("X\nㅌ", 0x58),
                new KeyMap("C\nㅊ", 0x43), new KeyMap("V\nㅍ", 0x56), new KeyMap("B\nㅠ", 0x42), new KeyMap("N\nㅜ", 0x4E),
                new KeyMap("M\nㅡ", 0x4D), new KeyMap("Enter", 0x0D, 2)});
            //5개 Ctrl, Window, Alt, Space, Hangul, Hanja
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Ctrl",0x10), new KeyMap("Win", 0x5B), new KeyMap("Space", 0x20, 4),
                new KeyMap("한/영", 0x15), new KeyMap("한자", 0x17)});



        }
    }
}