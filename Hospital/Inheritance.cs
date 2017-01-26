using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Inheritance
    {
        protected string a = "Pasien dilayani oleh dokter ";
        public string DataA()
        {
            string a = this.a;
            return a;
        }
    }
    class Dok1 : Inheritance
    {
        public string DataB()
        {
            string b = "Penyakit Umum";
            return b;
        }
    }
    class Dok2 : Inheritance
    {
        public string DataC()
        {
            string c = "Penyakit THT";
            return c;
        }
    }
    class Dok3 : Inheritance
    {
        public string DataD()
        {
            string d = "Penyakit Dalam";
            return d;
        }
    }
}
