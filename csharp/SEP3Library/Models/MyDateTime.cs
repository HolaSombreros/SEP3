namespace SEP3Library.Models {
    public class MyDateTime {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
       
        public string DateToString(){
            return $"{Day}/{Month}{Year}";
        }
    }

    

}