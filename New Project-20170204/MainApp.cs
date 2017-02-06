using System;
using System.Collections.Generic;

 
namespace DoFactory.GangOfFour.Command.CarPark
{
  /// <summary>
  /// MainApp startup class for CarPark 
  /// Command Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    public static void Main(string[] args)
    {
      // get Entry date of Patron
      Console.WriteLine("Enter Entry date (yyyy-mm-dd):");
      string userInputEntrydate = Console.ReadLine();
      string sday = getTheDayofWeek(userInputEntrydate); //start day
      Console.WriteLine("Start Day : {0} \n", sday); 
      
      // get entry time of Patron 
      Console.WriteLine("Enter Entry time(HH:MM exmaple 3.30 pm should be put in as 15:30) :");
      string userInputEntryTime = Console.ReadLine();
      TimeSpan stime = TimeSpan.Parse(userInputEntryTime);
      
       //get Exit date of Patron
      Console.WriteLine("Enter Exit date (yyyy-mm-dd) :");
      string userInputExitdate = Console.ReadLine();
      string eday = getTheDayofWeek(userInputExitdate); //start day
      Console.WriteLine("Exit Day : {0} \n", eday); 
      
      // get exit time of Patron 
      Console.WriteLine("Enter Exit time(HH:MM exmaple 3.30 pm should be put in as 15:30): ");
      var userInputExitTime = Console.ReadLine();
      TimeSpan etime = TimeSpan.Parse(userInputExitTime);
      
     
     EntryType et = new EntryType();
     string entryType =  et.getEntryType(sday,eday,stime,etime);
     Console.WriteLine("Flag : {0} \n", entryType);
     
     double totVal = 0.0;
     
     totVal = et.ComputeParkingRate(entryType, userInputEntrydate, userInputExitdate, sday, eday, stime, etime  );
     
     Console.WriteLine("\n Total : {0}", totVal);
     
     
    }
    
    
    public static string getTheDayofWeek(string adate){
        DateTime dateValue;
      DateTime.TryParse(adate, out dateValue );
      return dateValue.ToString("ddd");
    }
    
    
    
    
  }
  
  public class EntryType {
      
      
      public double ComputeParkingRate(string entryType, string EntryDate, string ExitDate, string sday, string eday, TimeSpan stime, TimeSpan etime)
      {
          if(entryType== "WeekEndRate")
           //here it is assumed as per requirement that if a Patron keep the car on sat and sun , the total rate is $10
           {
               return 10.00;
           }
           if(entryType== "NightPark")
           {
               return 6.50;
           }
           if(entryType== "EarlyBird")
           {
               return 13.00;
           }
            if(entryType== "FlatRate")
           {
               //now we need to check for number of days 
               DateTime d1, d2;
                DateTime.TryParse(EntryDate, out d1 );
                DateTime.TryParse(ExitDate, out d2 );
                //int n=1;
                //n= (d2-d1).TotalDays;
               // if((d2-d1).TotalDays>=1)
                
               return 13.00*(d2-d1).TotalDays;
           }
            if(entryType== "StandardRate")
            {// here we have to check the rate per hour calculation
            
             if((etime-stime)<=TimeSpan.Parse("01:00"))
              {
                  return 5.00;
              }
              if((etime-stime)>TimeSpan.Parse("01:00") && (etime-stime)<=TimeSpan.Parse("02:00"))
              {
                  return 10.00;
              }
               if((etime-stime)>TimeSpan.Parse("02:00") && (etime-stime)<=TimeSpan.Parse("03:00"))
              {
                  return 15.00;
              }
               if((etime-stime)>TimeSpan.Parse("03:00"))
              {
                  return 20.00;
              }  
                return 0.0;//default;
            }
           
           
          
          
          return 0.0;
      }
      
      
      
      public string getEntryType(string sday, string eday, TimeSpan stime, TimeSpan etime){
          
          string sflag = "y";
          
          //Check if day is a week day
          switch (sday)
            {
                case "Sat": sflag= "n"; break;
                case "Sun": sflag= "n"; break;
                case "Mon": sflag= "y"; break;
                case "Tue": sflag= "y"; break;
                case "Wed": sflag= "y"; break;
                case "Thu": sflag= "y"; break;
                case "Fri": sflag= "y"; break;
            }
            
            if(sflag=="n")// if day is not a week day
            {
                string eflag = "y";
                
                switch(eday)
                {
                    case "Sat": eflag= "n"; break;
                    case "Sun": eflag= "n"; break;
                    case "Mon": eflag= "y"; break;
                    case "Tue": eflag= "y"; break;
                    case "Wed": eflag= "y"; break;
                    case "Thu": eflag= "y"; break;
                    case "Fri": eflag= "y"; break;
                }
                if(eflag=="n")
                return "WeekEndRate";
                else
                return "FlatRate";
            }
            else// now check for early bird, flatrate, nightpark
            {
                TimeSpan NightParkStartTime = TimeSpan.Parse("18:00");
                TimeSpan NightParkEndTime = TimeSpan.Parse("23:59");
                if(stime>NightParkStartTime && stime <= NightParkEndTime)
                 {
                     //if start time falls under nightpark then also check endtime
                     if(etime<TimeSpan.Parse("06:00") || etime>= NightParkStartTime)
                      return "NightPark";
                      else
                      return "FlatRate";
                     
                     
                 }
                 else//non night parking
                 {
                     //return "";
                     //check for early bird parking
                     if(stime>=TimeSpan.Parse("06:00") && stime< TimeSpan.Parse("09:00"))
                     {
                         if(etime>=TimeSpan.Parse("15:30") && etime<=TimeSpan.Parse("23:30"))
                           return "EarlyBird";
                          else
                           return "FlatRate";
                     }
                     else //standard park but check exit time latter
                      return "StandardRate";
                     
                     
                 }
                 
            }
            
            
      
      }
          
}
  
 
  /// <summary>
  /// The 'Command' abstract class
  /// </summary>
  abstract class Command
  {
    public abstract void Execute();
    //To use Command Pattern by Syed if get more time
  }
 
  
  
}