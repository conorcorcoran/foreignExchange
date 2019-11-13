using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace fxTransaction
{
    class foreignExchange{
            private const string URL = "http://data.fixer.io/api/latest?access_key=a94666c6342e3bbf7fb4c218f6afb915";
        static void Main(string[] args)
        {
            Dictionary<int, fxTransaction.Transation>  transactions = new Dictionary<int, fxTransaction.Transation>();
            /* Gets the exchange rates from Fixer.io
               The API has different paid levels. The free version
               only allows you to get all current rates against the Euro.
               Paid version will allow the conversion to be done with the
               API with a specified base and destination currency.
            */
            dynamic exchangeData = JObject.Parse( HttpGet(URL));
            JObject rates = exchangeData.rates;


            //Get all the files in the directory that end with csv
            Console.WriteLine("Getting files");
            var files = Directory.GetFiles(@"C:\Users\ccorcoran\Desktop\fxTransactions", "*.csv");
            List<string> fileNames = new List<string>();
            for(int i = 0; i < files.Length; i++){
                fileNames.Add(files[i]);
            }
            Console.WriteLine("Got files");
            int fileIndex = -1;
            foreach(string file in files){
                fileIndex++;
        
            using(StreamReader reader = new StreamReader(file))
            {
                Console.WriteLine("Reading file " + fileNames[fileIndex]);
                
                List<string> id = new List<string>();
                List<string> sourceCurrency = new List<string>();
                List<string> destinationCurrency = new List<string>();
                List<string> sourceAmount = new List<string>();
              

                 while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    var items = values[0].Split(',');


                    id.Add(items[0]);
                    sourceCurrency.Add(items[1]);
                    destinationCurrency.Add(items[2]);
                    sourceAmount.Add(items[3]); 
                }

                for(int i = 1; i < id.Count; i++){
                    calculateExchange(sourceCurrency[i], destinationCurrency[i], sourceAmount[i], rates);
                }
                    Console.WriteLine("Done reading file " + fileNames[fileIndex]);
                }
            }
        }

        public static double calculateExchange(string srcCurr, string desCurr, string amount, JObject rates){
            Console.WriteLine("Source currency: " + srcCurr);
            Console.WriteLine("Destination currency: " + desCurr);
            Console.WriteLine("Amount to be converted: " + amount);
            double conversionPrice;
            /*
                Can only get rates with Euro as the base.
                Due to this some calculations have to be done, while converting.
            */
            if(srcCurr.Equals("EUR")){
                conversionPrice = rates[desCurr].ToObject<double>();
                Console.WriteLine("conversionPrice: " + conversionPrice);
            }else if(desCurr.Equals("EUR")){
                double srcCurrToEuro = rates[srcCurr].ToObject<double>();
                conversionPrice = 1 /srcCurrToEuro;
                Console.WriteLine("conversionPrice in Euro" + conversionPrice);
            }else{
                double srcCurrToEuro = rates[srcCurr].ToObject<double>();
                double conversionPriceInEuro = 1/srcCurrToEuro;
                conversionPrice = conversionPriceInEuro * rates[desCurr].ToObject<double>();
                Console.WriteLine("conversionPrice " + conversionPrice);
            }

            double exchangeAmount = double.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            double convertedCurrency = exchangeAmount*conversionPrice;
            Console.WriteLine("Converted amount: " + Math.Round(convertedCurrency, 2));
            return Math.Round(convertedCurrency, 2);
        }

        public static string HttpGet(string url){
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();

           try{
               using(Stream stream = response.GetResponseStream()){
                   StreamReader reader = new StreamReader(stream);

                   return reader.ReadToEnd();
               }
           } finally{
               response.Close();
           }
        }
    }
}
