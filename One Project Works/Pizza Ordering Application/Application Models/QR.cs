namespace Pizza_Ordering_Application.Application_Models
{
    internal class QR
    {
        static QR()
        {
            NullCode = new QR();
            NullCode._code = 0;
            _hashSet = new HashSet<int>();
        }
        private int _code;

        private static HashSet<int> _hashSet = new HashSet<int>();
        private int GenerateCode()
        {
            while (true)
            {
                Random random = new Random();
                int k = random.Next((int)1e8, (int)1e9 - 1);
                int sz = _hashSet.Count;
                _hashSet.Add(k);
                if (sz != _hashSet.Count)
                {
                    return k;
                }
            }
        }
        public QR()
        {
            _code = GenerateCode();
        }

        public QR(QR qR)
        {
            _code = qR._code;
        }


        public readonly static QR NullCode;
        public static int ScanQR(QR qr)
        {
            return qr._code;
        }
    }
}