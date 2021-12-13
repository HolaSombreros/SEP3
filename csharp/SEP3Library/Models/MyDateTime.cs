using System;

namespace SEP3Library.Models {
    public class MyDateTime {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public MyDateTime() {}
        
        public MyDateTime(DateTime dateTime) {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
            Second = DateTime.Now.Second;
        }
        
        public string DateToString(){
            return $"{Day}/{Month}/{Year}";
        }

        public override string ToString() {
            return DateToString() + $" {Hour}:{Minute}";
        }
    }

    

}