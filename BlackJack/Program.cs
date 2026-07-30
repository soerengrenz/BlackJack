using GameLogic;

Player? player;
Game? game;

SetupPlayer();
SetupGame();
StartGame();

void StartGame()
{
    game.Start();

    while (game.Status != Game.GameStatus.Lost && game.Status != Game.GameStatus.Won)
    {
        switch (game.Status)
        {
            case Game.GameStatus.Started:
                game.DrawCard();
                break;
            case Game.GameStatus.WaitingForPlayer:
                Console.WriteLine($"\nYou got {game.Player.Cards.Last().CardFace.Item1} of {game.Player.Cards.Last().CardFace.Item2}, and your total card value is {game.Player.CardsValue.ToString()}");
                Console.WriteLine("Press 'B' to bet, 'C' to call or 'H' to hold");
                switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                {
                    case "B":
                        game.DrawLastCard();
                        break;
                    case "C":
                        game.DrawCard();
                        break;
                    case "H":
                        game.Hold();
                        Console.WriteLine($"\nYour current hand is ({string.Join(", ", game.Player.Cards.Select(x => x.ToString()))}) , and your total card value is {game.Player.CardsValue.ToString()}");
                        break;
                    default:
                        SetupGame();
                        break;
                }
                break;
            case Game.GameStatus.Hold:
                var dealerHand = game.DealerDraw();
                if(game.Status == Game.GameStatus.Lost)
                    Console.WriteLine($"\nYou lost, as your total card score was {game.Player.CardsValue}, and dealer had ({string.Join(", ", dealerHand.Cards.Select(x => x.ToString()))}) , with a total card value of  {dealerHand.CardValues}");
                else 
                    Console.WriteLine($"\nYou WON, as your total card score was {game.Player.CardsValue}, and dealer had ({string.Join(", ", dealerHand.Cards.Select(x => x.ToString()))}) , with a total card value of  {dealerHand.CardValues}");
                break;
            case Game.GameStatus.Won:
                Console.WriteLine($"\nYou lost, as your total card score exceeded 21\nYour current hand is ({string.Join(", ", game.Player.Cards.Select(x => x.ToString()))}) , and your total card value is {game.Player.CardsValue.ToString()}");
                break;
        }
    }

    Console.WriteLine("\nGame is over");
    Console.ReadLine();
}

void SetupGame()
{
    Console.Write($"\nWelcome '{player.PlayerName}'. Press 'P' to play");
    switch (Console.ReadKey().KeyChar.ToString().ToUpper())
    {
        case "P":
            game = Game.Create(player);
            break;
        default:
            SetupGame();
            break;
    }
}

void SetupPlayer()
{ 
    Console.WriteLine("Player Name:");
    var playerName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(playerName))
    {
        SetupPlayer();
    }

    player = new Player(playerName);

    Console.WriteLine("Wallet value (leave blank for 100$):");
    var walletValue = Console.ReadLine();

    if (!string.IsNullOrEmpty(walletValue) && decimal.TryParse(walletValue,out decimal money))
    {
        player.AlterWallet(money);
    }
}