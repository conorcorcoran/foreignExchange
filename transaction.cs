
namespace fxTransaction
{
    public class Transaction{
        private int id;
        private string sourceCurrency;
        private string destinationCurrency;
        private double sourceAmount;
        private double convertedAmount;
        public Transaction(int id, string sourceCurrency, string destinationCurrency, double sourceAmount, double convertedAmount){
            this.id = id;
            this.sourceCurrency = sourceCurrency;
            this.destinationCurrency = destinationCurrency;
            this.sourceAmount = sourceAmount;
            this.convertedAmount = convertedAmount;
        }

        public int getId() {
            return id;
        }

        public string getSourceCurrency() { 
            return sourceCurrency; 
             
        }

        public string getDestinationCurrency() { 
          return destinationCurrency;
             
        }

        public double getSourceAmount(){
            return sourceAmount;
            
        }

        public double getConvertedAmount(){ 
           return convertedAmount;
        }

        public string printTransaction(){

            return "ID: " + getId() + 
            " Base Currency: " + getSourceCurrency() + 
            " Destination Currency: " + getDestinationCurrency() + 
            " Amount: " + getSourceAmount() + 
            " Coverted amount: " + getConvertedAmount() + 
            " Excahnge rate: " + (getConvertedAmount()/getSourceAmount());
        }



    }
}