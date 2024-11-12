
public static partial class Textures
{
    #region DEAD
    public static void PrintGrave()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(40, 4);
            Console.WriteLine("  _____                        ");
            Console.SetCursorPosition(40, 5);
            Console.WriteLine(" /  .  \\                      ");
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("(  =|=  )                      ");
            Console.SetCursorPosition(40, 7);
            Console.WriteLine(" |  |  |                       ");
            Console.SetCursorPosition(40, 8);
            Console.WriteLine(" |_____|                       ");
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("/_______\\                     ");

            Thread.Sleep(300);

            Console.SetCursorPosition(40, 9);
            Console.WriteLine("/__RIP__\\                     ");

            Thread.Sleep(300);
        }
        Thread.Sleep(1000);
    }


    public static void PrintDeadText() // Likadan fast grön eller liknande till vår gubbe?
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(40, 5);
        Console.WriteLine("____  ____   _   ____  ");
        Console.SetCursorPosition(40, 6);
        Console.WriteLine("|| \\\\ ||    /\\\\  || \\\\ ");
        Console.SetCursorPosition(40, 7);
        Console.WriteLine("||  ||||-- //_\\\\ ||  ||");
        Console.SetCursorPosition(40, 8);
        Console.WriteLine("||_// ||__//   \\\\||_// ");

        Console.SetCursorPosition(40, 9);
        Console.WriteLine("                         "); // För att ta bort gammal text
        Console.SetCursorPosition(40, 10);
        Console.WriteLine("                         "); // --"--

        Thread.Sleep(100);
        Console.SetCursorPosition(40, 9);
        Console.WriteLine("        |          | ");

        Thread.Sleep(200);
        Console.SetCursorPosition(40, 9);
        Console.WriteLine("  |     |          | ");

        Thread.Sleep(250);
        Console.SetCursorPosition(40, 9);
        Console.WriteLine("  | |   | |        | ");

        Thread.Sleep(200);
        Console.SetCursorPosition(40, 10);
        Console.WriteLine("        |            ");

        Thread.Sleep(250);
        Console.SetCursorPosition(40, 10);
        Console.WriteLine("  |     |            ");

        Thread.Sleep(300);
        Console.SetCursorPosition(40, 10);
        Console.WriteLine("  |     |      |     ");

        Thread.Sleep(300);
        Console.SetCursorPosition(40, 11);
        Console.WriteLine("        |            ");
        Console.ResetColor();
    }
    #endregion



    #region LOADING
    public static void PrintLoading()
    {
        string[] loadingBar = new string[10] { "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };

        int percentLoaded = 0;


        for (int i = 0; i < loadingBar.Length; i++) // Loopa 10 gånger
        {
            loadingBar[i] = " |";
            percentLoaded += 10;
            Console.SetCursorPosition(18, 3);
            Console.Write($"Loading...  {percentLoaded}/100%");

            Console.SetCursorPosition(17, 4);
            Console.Write("[");

            for (int j = 0; j < loadingBar.Length; j++)
            {
                if (loadingBar[j] == " |")
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.Write(loadingBar[j]);      //Om det är en player, skriv ut i grönt
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(loadingBar[j]); // Skrivs ut tomma för samma längd
                }

            }

            Console.Write("]");

            Thread.Sleep(500);
        }

        Console.WriteLine();
    }
    #endregion

    public static void PrintFirstScreen()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(30, 4);
        Console.WriteLine("     /\\                                       /\\     ");
        Console.SetCursorPosition(30, 5);
        Console.WriteLine("    /  \\                                     /  \\    ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.SetCursorPosition(30, 6);
        Console.WriteLine("    |  |                                     |  |      ");
        Console.SetCursorPosition(30, 7);
        Console.WriteLine("    |  |                                     |  |      ");
        Console.SetCursorPosition(30, 8);
        Console.WriteLine("    |  |                                     |  |      ");
        Console.SetCursorPosition(30, 9);
        Console.WriteLine("    |  |                                     |  |      ");
        Console.SetCursorPosition(30, 10);
        Console.WriteLine("    <  >                                     <  >      ");
        Console.SetCursorPosition(30, 11);
        Console.WriteLine("{}==|  |=={}                             {}==|  |=={}  ");
        Console.SetCursorPosition(30, 12);
        Console.WriteLine("     []                                       []       ");
        Console.SetCursorPosition(30, 13);
        Console.WriteLine("     []                                       []       ");
        Console.SetCursorPosition(30, 14);
        Console.WriteLine("     ==                                               ");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(44, 5);
        Console.WriteLine("    ____ __  __ ____  ");
        Console.SetCursorPosition(44, 6);
        Console.WriteLine("     ||  ||  || ||    ");
        Console.SetCursorPosition(44, 7);
        Console.WriteLine("     ||  ||__|| ||_   ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(44, 8);
        Console.WriteLine("     ||  ||  || ||__  ");
        Console.SetCursorPosition(44, 9);
        Console.WriteLine("   __   __    __  ____ ");
        Console.SetCursorPosition(44, 10);
        Console.WriteLine("  //|| //||  //\\\\ \\\\  \\\\ ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(44, 11);
        Console.WriteLine(" // |//  || ||__|| \\\\__\\\\  ");
        Console.SetCursorPosition(44, 12);
        Console.WriteLine("//  |/   || ||  ||  \\\\	");
        Console.SetCursorPosition(44, 13);
        Console.WriteLine("  ´´´´´´´´´´´´´´´´´´	");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(32, 14);
        // Console.WriteLine(" ---by---pontrik---");
        Console.WriteLine("by == p0ntr1k                               ==");

        while (!Console.KeyAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(49, 14);
            Console.WriteLine("  PRESS START	");

            Thread.Sleep(300);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(49, 14);
            Console.WriteLine("  PRESS START	");

            Thread.Sleep(300);
        }
    }


}


/*

POTION, HJÄLM, RUSTNING, VAPEN, XP

      _____
     /=====\
    ||     ||
    ||     ||  HJÄLM
     \\_ _// 
      /   \


       ____    POTION
       |  |		
       |  |		
      /----\
     / . ,  \
    (________)	


              ___  ____
             /   \/    \  DRESS
            /_/\     /\_\
               /_____\
              /_______\
             /_________\
            /___________\


        /\  10 RADER
       /  \
       |  |
       |  |
       |  |
       |  |
       <  >
   {}==|  |=={}
        []
        []
        ==


    /\
   {  } 
    ||
    ||  
    ||    Magestaff?
    ||
    ||
    || 
   {  }
    \/ 
     


    _       _
   / \     / \
  /   \___/   \
 (     ___     )  AXE
  \   /| |\   /
   \_/ | | \_/
       | |  
       | |
       |_|
       (_)
       (_)
       (_)

          _       _
   / \     / \
  /   \___/   \
 (     ___     )  AXE
  \   /| |\   /
   \_/ | | \_/
       | |  
       | |
       |_|
       (_)
       (_)
       (_)



   2222     22222222
 22    22   22
22     22   22         2   2  2222
     22     22222222    2 2   2   2
   22              22    2    2222
22          22     22   2 2   2  
222222222    222222    2   2  2

()  ____  ()
 \\/    \//
  | >  < |
 (  xxxx  )
  \______/
   //  \\
  ()    ()
   
    xxxx
 xxxxxxxxxx
xx xxxxxx xx RUSTNING ::S:S:S
xx xxxxxx xx
   xxxxxx
   xxxxxx
   ------

        /\
       / /
      / /
     / / 
    / /
  ~/ /~
  /_/


        /\
       / /
      / /
     / / 
  _ / /_
 (_/ /_)
  /_/
  o
  
  _____       _____
 [xxxxx]     /=====\
[xxOxOxx]   ||     ||
[xxxxxxx]   ||     ||  HJÄLM
 \xxxxx/     \\_ _// 
              /   \

//        
 //       O 		    O 		  O	
 //      ||{}->        ||{}--> 	 ||{} --> 
 //      /\	           /\	     /\		ASSASSIN
 //     /  \	      /  \		/  \
 //
       O  | 		 O   /		O   /*_
      ||--T 		||--/	   ||--/	
      /\		    /\	       /\
     /  \	       /  |	      /  |


	  .		      
       0  | 		 
  [.]-||--T 		
      /\  		
     /  \	       

  \ o	
   ||\
   /\  
  | /

    	
  _ o
   ||_
  | \
  '  '

   \o/
   //
  | \
  '  '
 ____  ____   _   ____
 || \\ ||    /\\  || \\
 ||  ||||-- //_\\ ||  ||
 ||_// ||__//   \\||_//

 ____  ____   _   ____
 || \\ ||    /\\  || \\
 ||  ||||-- //_\\ ||  ||
 ||_// ||__// | \\||_//
   | |   | |      | | 
   |       |      |   
           |

    \  /
     \/
   DE/\D		
    /  \	

       .
      / \	 
      | |
      | |
      [ ]
       I
       o	
  
         
       . 
      / \  	 
      | | 
      | |
      [ ]
       o
       I
		

 //	          ,			
 //       0  |     \   Ö  
 //      ||--T      \--|| MAGE
 //      /\            /\
 //     L  \          /  \
 //
//    |   O   
 //   |--||-0 WARRIOR
 //      /\
 //     /  \
 //
 //   ;  00   
 //   |=-[]-=' ROBOT
 //      /\
 //     d  b


 //       O   
 //      ||-}-->
 //      /\
 //     |  \

  //      O   
 //      ||-}  -->
 //      /\
 //     |  \

   //     O   
 //      ||-}   -->
 //      /\
 //     /  \

 
   //     O   
 //      ||-}     -->
 //      /\
 //     /  \


    ____
    |  |		
    |  |		
   /----\
  / . ,  \
 (________)		
  

   _--~_
  (_____)
  |  $  |
   \___/
    

*/
// TOM MAP-ARRAY
// public static Map Level3(Player player)
// {
//     char[,] gameLevel = new char[,]
//     {  //  1    2    3    4    5    6    7    8    9   10   11   12   13   14   15   16   17   18   19   20   21   22   23      // DISARMA MINOR?!
//         { '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_' },
//         { '=', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
//         { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '@', '|' },
//         { '|', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '|' },//23
//     };