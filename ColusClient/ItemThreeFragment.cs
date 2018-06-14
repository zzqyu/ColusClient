using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private List<uint> mulKeyList = new List<uint>();
        private List<KeyMap> keyMapList;
        private int[] numItemOfRows;

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
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Button prev = view.FindViewById<Button>(Resource.Id.prevBtn); // <-
            Button next = view.FindViewById<Button>(Resource.Id.nextBtn); // ->
            Button f5 = view.FindViewById<Button>(Resource.Id.f5Btn); // f5
            Button shiftF5 = view.FindViewById<Button>(Resource.Id.shiftF5Btn); // shift + f5
            Button esc = view.FindViewById<Button>(Resource.Id.escBtn); // esc
            Button home = view.FindViewById<Button>(Resource.Id.homeBtn); // home
            Button end = view.FindViewById<Button>(Resource.Id.endBtn); // end

            SetKeycode();

            prev.Click += Prev_Click;
            next.Click += Next_Click;
            f5.Click += F5_Click;
            shiftF5.Click += ShiftF5_Click;
            esc.Click += Esc_Click;
            home.Click += Home_Click;
            end.Click += End_Click;
        }

        private void End_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[9].KeyCode;

            string sendKeyCode = keyCode.ToString("X");

            SendMessage(sendKeyCode);
        }

        private void Home_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[8].KeyCode;
            string sendKeyCode = keyCode.ToString("X");
            SendMessage(sendKeyCode);
        }

        private void Esc_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[5].KeyCode;

            string sendKeyCode = keyCode.ToString("X");

            SendMessage(sendKeyCode);
        }

        private void ShiftF5_Click(object sender, EventArgs e)
        {
            int keyCodeShift = keyMapList[65].KeyCode;
            int keyCodeF5 = keyMapList[19].KeyCode;
            int keyCodeMulty = keyMapList[0].KeyCode;

            string sendKeyCodeShift = keyCodeShift.ToString("X");
            string sendKeyCodeF5 = keyCodeF5.ToString("X");
            string sendKeyCodeMulty = keyCodeMulty.ToString("X");

            SendMessage(sendKeyCodeMulty);
            Thread.Sleep(100);
            SendMessage(sendKeyCodeShift);
            Thread.Sleep(100);
            SendMessage(sendKeyCodeF5);
            Thread.Sleep(100);
            SendMessage(sendKeyCodeMulty);
        }


        private void F5_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[19].KeyCode;

            string sendKeyCode = keyCode.ToString("X");

            SendMessage(sendKeyCode);
        }

        private void Next_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[4].KeyCode;

            string sendKeyCode = keyCode.ToString("X");

            SendMessage(sendKeyCode);
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            int keyCode = keyMapList[1].KeyCode;

            string sendKeyCode = keyCode.ToString("X");

            SendMessage(sendKeyCode);
            
        }

        private void SetKeycode()
        {
            keyMapList = new List<KeyMap>();
            numItemOfRows = new int[] { 5, 10, 10, 10, 10, 10, 10, 10, 8 };
            //4개 좌상하우
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Multi\nKey", 0xff), /*0*/
                new KeyMap("←", 0x25)/*1*/, new KeyMap("↑", 0x26)/*2*/, new KeyMap("↓", 0x28)/*3*/, new KeyMap("→", 0x27)/*4*/});
            //10개 ESC, INSERT , DEL, HOME, END, pageup pagedown, PRINTScr, F11, F12
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Esc",0x1B)/*5*/, new KeyMap("Ins", 0x2D)/*6*/, new KeyMap("Del", 0x2E)/*7*/,
                new KeyMap("Home", 0x24)/*8*/, new KeyMap("End", 0x23)/*9*/, new KeyMap("Pg Up", 0x21)/*10*/, new KeyMap("Pg Dn", 0x22)/*11*/,
                new KeyMap("PrtSc", 0x2C)/*12*/, new KeyMap("F11", 0x7A)/*13*/, new KeyMap("F12", 0x7B)/*14*/});
            //10개 F1~F10
            for (int i = 0x70; i < 0x7A; i++)
                keyMapList.Add(new KeyMap("F" + (i - 0x70 + 1), i)/*15~24*/);
            //10개 1 ~ 9, 0
            keyMapList.AddRange(new KeyMap[] { new KeyMap("!\n1",0x31)/*25*/, new KeyMap("@\n2", 0x32)/*26*/, new KeyMap("#\n3", 0x33)/*27*/,
                new KeyMap("$\n4", 0x34)/*28*/, new KeyMap("%\n5", 0x35)/*29*/, new KeyMap("^\n6", 0x36)/*30*/, new KeyMap("&\n7", 0x37)/*31*/,
                new KeyMap("*\n8", 0x38)/*32*/, new KeyMap("(\n9", 0x39)/*33*/, new KeyMap(")\n0", 0x30)/*34*/});

            //10개  `, -, =, [, ], \, ;, ', <, >, 
            keyMapList.AddRange(new KeyMap[] { new KeyMap("~\n`",0xC0)/*35*/, new KeyMap("_\n-", 0xBD)/*36*/, new KeyMap("+\n=", 0xBB)/*37*/,
                new KeyMap("{\n[", 0xDB)/*38*/, new KeyMap("}\n]", 0xDD)/*39*/, new KeyMap("|\n\\", 0xDC)/*40*/, new KeyMap(":\n;", 0xBA)/*41*/,
                new KeyMap("\"\n'", 0xDE)/*42*/, new KeyMap("<\n,", 0xBC)/*43*/, new KeyMap(">\n.", 0xBE)/*44*/});
            //10개  q, w, e, r, t, y, u, i, o, p
            keyMapList.AddRange(new KeyMap[] {new KeyMap("Qㅃ\n ㅂ", 0x51)/*45*/, new KeyMap("Wㅉ\n ㅈ", 0x57)/*46*/,
                new KeyMap("Eㄸ\n ㄷ", 0x45)/*47*/, new KeyMap("Rㄲ\n ㄱ", 0x52)/*48*/, new KeyMap("Tㅆ\n ㅅ", 0x54)/*49*/, new KeyMap("Y\nㅛ", 0x59)/*50*/,
                new KeyMap("U\nㅕ", 0x55)/*51*/, new KeyMap("I\nㅑ", 0x49)/*52*/, new KeyMap("Oㅒ\n ㅐ", 0x4F)/*53*/, new KeyMap("Pㅖ\n ㅔ", 0x50)/*54*/});
            //10개 CapsLock,a, s, d ,f, g, h, j, k , l
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Caps\nLock",0x14)/*55*/, new KeyMap("A\nㅁ", 0x41)/*56*/, new KeyMap("S\nㄴ", 0x53)/*57*/,
                new KeyMap("D\nㅇ", 0x44)/*58*/, new KeyMap("F\nㄹ", 0x46)/*59*/, new KeyMap("G\nㅎ", 0x47)/*60*/, new KeyMap("H\nㅗ", 0x48)/*61*/,
                new KeyMap("J\nㅓ", 0x4A)/*62*/, new KeyMap("K\nㅏ", 0x4B)/*63*/, new KeyMap("L\nㅣ", 0x4C)/*64*/ });
            //10개 Shift, z, x, c, v, b, n, m, ?,  backSpace 
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Shift",0x10)/*65*/, new KeyMap("Z\nㅋ", 0x5A)/*66*/, new KeyMap("X\nㅌ", 0x58)/*67*/,
                new KeyMap("C\nㅊ", 0x43)/*68*/, new KeyMap("V\nㅍ", 0x56)/*69*/, new KeyMap("B\nㅠ", 0x42)/*70*/, new KeyMap("N\nㅜ", 0x4E)/*71*/,
                new KeyMap("M\nㅡ", 0x4D)/*72*/ , new KeyMap("?\n/", 0xBF)/*73*/ , new KeyMap("←", 0x08)/*74*/});
            //8개 tab, Ctrl, Window, Alt, Space, Hangul, Hanja,  Enter
            keyMapList.AddRange(new KeyMap[] { new KeyMap("Tab",0x09)/*75*/,  new KeyMap("Ctrl",0x11)/*76*/, new KeyMap("Win", 0x5B)/*77*/,
                new KeyMap("Alt",0x12)/*78*/, new KeyMap("Space", 0x20, 2.5f)/*79*/,
                new KeyMap("한/영", 0x15)/*80*/, new KeyMap("한자", 0x19)/*81*/,  new KeyMap("Enter", 0x0D, 1.5f)/*82*/});
        }
    }
}