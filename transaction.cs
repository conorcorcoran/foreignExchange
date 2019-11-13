
namespace fxTransaction
{
    public class Transation{
        private int id;
        private string sourceCurrency;
        private string destinationCurrency;
        private double sourceAmount;
        private double convertedAmount;
        public Transation(int id, string sourceCurrency, string destinationCurrency, double sourceAmount, double convertedAmount){
            id = this.id;
            sourceCurrency = this.sourceCurrency;
            destinationCurrency = this.destinationCurrency;
            sourceAmount = this.sourceAmount;
            convertedAmount = this.convertedAmount;
        }

        public int getId { 
            get{return id;}
            set{id = value; } 
        }

        public string getSourceCurrency { 
            get{return sourceCurrency;} 
            set{sourceCurrency = value;} 
        }

        public string getDestinationCurrency { 
            get{return destinationCurrency;}
            set{destinationCurrency = value;} 
        }

        public double getSourceAmount{
            get{return sourceAmount;}
            set{sourceAmount = value;}
        }

        public double getConvertedAmount { 
            get{return convertedAmount;} 
            set{convertedAmount = value;} 
        }



    }
}