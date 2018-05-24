namespace ColusClient {
    public class KeyMap{
        public string KeyName { get; }
        public int KeyCode { get; }
        public float Weight { get; }
        public KeyMap(string keyName, int keyCode, float weight)
        {
            this.KeyName = keyName;
            this.KeyCode = keyCode;
            this.Weight = weight;
        }
        public KeyMap(string keyName, int keyCode) : this(keyName, keyCode, 1){}
    }
}
