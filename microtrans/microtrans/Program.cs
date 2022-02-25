using System;

int money = 100;

string[] itemName = {"Pair of Boots", "Helmet", "Potion"};
string[] itemPlural = {"Pair(s) of Boots", "helmet(s)", "potion(s)"};
int[] itemPrice = {50, 100, 25};
int[] itemQuantity = {3, 1, 100};
int chosenOption = -1;
bool outOfStock = false;
bool buying = false;
bool soldOut = false;

while (soldOut == false) {
    chosenOption = -1;
  // Shows your balance & available items
    ItemList(itemName, itemPlural, itemPrice, itemQuantity, chosenOption);
  // Tells you what about the chosen number is invalid or displays info about the item
    
    string option = Console.ReadLine();  
    bool success = int.TryParse(option, out chosenOption);

    if(success == false)
    {   
      for(int i = 0; i < itemName.Length; i++)
      {
        if(option == itemName[i]){
          success = true;
          chosenOption = i;
          option = $"{chosenOption}";
        } 
      }
    } else {
    chosenOption--;
    }

    if (success == false)
    {
      Console.Clear();
      
      Console.WriteLine("Only numbers or the exact name of your desired item.");
    } else if (chosenOption >= 0 && chosenOption <= itemName.Length){
      Console.Clear();
      
      ItemList(itemName, itemPlural, itemPrice, itemQuantity, chosenOption);
      System.Console.WriteLine();
      if (itemQuantity[chosenOption] != 0)
      {
        System.Console.WriteLine($"Those are ${itemPrice[chosenOption]} a piece.");
        System.Console.WriteLine($"And there are {itemQuantity[chosenOption]} {itemPlural[chosenOption]} in stock.");
        outOfStock = false;
      } else {
        Console.Clear();
        
        System.Console.WriteLine($"You already bought all of the {itemPlural[chosenOption]}.");
        outOfStock = true;
      }
      
      if (outOfStock != true)
      {
        buying = AskBuy(money);
      }

      while (buying == true)
      {
        option = "";

        System.Console.WriteLine($"How many would you like? There are {itemQuantity[chosenOption]} {itemPlural[chosenOption]} in stock.");
          option = Console.ReadLine();  
          success = int.TryParse(option, out int chosenQuantity);
          
          if(itemQuantity[chosenOption] != 0)
          {
              if (chosenQuantity < 1 || chosenQuantity > itemQuantity[chosenOption]) 
            {
              Console.Clear();
              if (chosenQuantity >= 0)
              {
                if (chosenQuantity == 0)
                {
                  System.Console.WriteLine($"Numbers from 1 - {itemQuantity[chosenOption]} only!");
                  buying = false;
                } else {
                  Console.WriteLine($"There are only {itemQuantity[chosenOption]} {itemPlural[chosenOption]} left.");
                  buying = false;
                }
              } else
              {
                System.Console.WriteLine("What the hell is that suppposed to mean?");
                buying = false;
              }
            } else if (money - itemPrice[chosenOption]*chosenQuantity > 0)
              {
                money -= itemPrice[chosenOption]*chosenQuantity;
                itemQuantity[chosenOption] -= chosenQuantity;
                Console.Clear();

                System.Console.WriteLine("Transaction completed!");

                buying = false;
                
                } else if (money - itemPrice[chosenOption]*chosenQuantity < 0 && money - itemPrice[chosenOption] < 0 && chosenQuantity < itemQuantity[chosenOption])
                {
                  Console.Clear();

                  System.Console.WriteLine("You can't afford that at all!");
                  System.Console.WriteLine("Stop messing around!");
                  buying = false;
                } else 
                {
                  Console.Clear();
                  
                  System.Console.Write("You can't afford that many! "); System.Console.WriteLine($"You are ${money - (itemPrice[chosenOption]*chosenQuantity)*-1} short.");
                } 
          } else
          {
            System.Console.WriteLine("Changed your mind? No problem, take all the time you'd like.");
            buying = false;
          }
      }

    } else {
      Console.Clear();
      
      System.Console.WriteLine($"Valid options are numbers from 1 to {itemName.Length}"); 
    }
    soldOut = ShopEmpty(itemQuantity);
} 

System.Console.WriteLine("You bought every item in my store! Thank you very much, kind sir.");
Console.ReadKey();

bool AskBuy(int money)
{
  string input = "";
  string inputLower = "";
  while(true)
    { 
    System.Console.WriteLine("Would you like to buy this item? yes/no");
    input = Console.ReadLine();
    inputLower = input.ToLower();
      if(inputLower != "yes" && inputLower != "no")
      {
        System.Console.WriteLine("YES/NO are the only accepted answers!");
      }
      if (inputLower == "yes")
      {
        return true;
      }
      if (inputLower == "no")
      {
        Console.Clear();
        
        return false;
      }
    } 
}

  // Lists status for items

void ItemList(string[] itemName, string[] itemPlural, int[] itemPrice, int[] itemQuantity, int chosenOption){


  System.Console.WriteLine($"You have ${money}");
  System.Console.WriteLine();
  System.Console.WriteLine("Items for sale:");

  for(int i = 0; i < itemName.Length; i++){
           
    if(chosenOption == i && itemQuantity[i] != 0){
    
      System.Console.Write($"{i+1}) ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      System.Console.Write($"{itemName[i]}");
      Console.ResetColor();
      System.Console.WriteLine(" <-");
    } else if (itemQuantity[i] == 0) {
      System.Console.Write($"{i+1}) ");
      Console.ForegroundColor = ConsoleColor.Red;
      System.Console.WriteLine($"{itemName[i]}");
      Console.ResetColor();
    } else
    {
      System.Console.WriteLine($"{i+1}) {itemName[i]}");
    }             
    
    
  }
}

  // Checks if all of the items in the store are sold out
bool ShopEmpty(int[] itemQuantity)
{
  int facts = 0;
  for (int i = 0; i < itemQuantity.Length; i++)
  {
    if (itemQuantity[i] == 0)
    {
      facts++;
    }
  }
  if (facts != 3)
  {
    return false;
  } else 
  { 
    return true;
  }
}

