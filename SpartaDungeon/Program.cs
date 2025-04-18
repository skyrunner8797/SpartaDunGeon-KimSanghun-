using System;
using System.Collections.Generic;

class Program
{
    static Character player = new Character();
    static List<Item> inventory = new List<Item>()
    {
        new Item("무쇠갑옷", "방어력", 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
        new Item("스파르타의 창", "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다."),
        new Item("낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검 입니다.")
    };

    static void Main(string[] args)
    {
        ShowMainMenu();
    }

    static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowStatus();
                        break;
                    case 2:
                        ShowInventory();
                        break;
                    case 3:
                        ShowShop();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 숫자 1~3 중에서 선택해주세요.");
                        PauseAndReturn();
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                PauseAndReturn();
            }
        }
    }

    static void ShowStatus()
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"{player.Name} ( {player.Class} )");
        Console.WriteLine($"공격력 : {player.GetFormattedStat("공격력")}");
        Console.WriteLine($"방어력 : {player.GetFormattedStat("방어력")}");
        Console.WriteLine($"체 력 : {player.Health}");
        Console.WriteLine($"Gold : {player.Gold} G\n");

        Console.WriteLine("0. 나가기");
        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 메인 메뉴로 돌아갑니다.");
            PauseAndReturn();
        }
    }

    static void ShowInventory()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        Console.WriteLine("[아이템 목록]");

        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("0. 나가기");

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ShowEquipManagement();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("잘못된 입력입니다. 메인 메뉴로 돌아갑니다.");
                PauseAndReturn();
                break;
        }
    }

    static void ShowEquipManagement()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                Item item = inventory[i];
                string equippedMark = item.IsEquipped ? "[E]" : "   ";
                Console.WriteLine($"- {equippedMark} {i + 1} {item.Name,-15} | {item.StatType} +{item.StatValue} | {item.Description}");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0")
                return;

            if (int.TryParse(input, out int index) && index >= 1 && index <= inventory.Count)
            {
                Item selectedItem = inventory[index - 1];

                if (selectedItem.IsEquipped)
                {
                    player.UnequipItem(selectedItem);
                    Console.WriteLine($"\n'{selectedItem.Name}'를 장착 해제했습니다.");
                }
                else
                {
                    player.EquipItem(selectedItem);
                    Console.WriteLine($"\n'{selectedItem.Name}'를 장착했습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            PauseAndReturn();
        }
    }

    static void ShowShop()
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

        Console.WriteLine($"[보유 골드] {player.Gold} G\n");

        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("- 수련자 갑옷    | 방어력 +5  | 수련에 도움을 주는 갑옷입니다.             |  1000 G");
        Console.WriteLine("- 무쇠갑옷      | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.           |  구매완료");
        Console.WriteLine("- 스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.|  3500 G");
        Console.WriteLine("- 낡은 검      | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.            |  600 G");
        Console.WriteLine("- 청동 도끼     | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.        |  1500 G");
        Console.WriteLine("- 스파르타의 창  | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다. |  구매완료");

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("0. 나가기");

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ShowItemPurchaseMenu();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("잘못된 입력입니다. 메인 메뉴로 돌아갑니다.");
                PauseAndReturn();
                break;
        }
    }

    static void ShowItemPurchaseMenu()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("구매할 아이템을 선택해주세요.\n");

        Console.WriteLine($"[보유 골드] {player.Gold} G\n");

        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("- 1. 수련자 갑옷    | 방어력 +5  | 수련에 도움을 주는 갑옷입니다.             |  1000 G");
        Console.WriteLine("- 2. 무쇠갑옷      | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.           |  구매완료");
        Console.WriteLine("- 3. 스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.|  3500 G");
        Console.WriteLine("- 4. 낡은 검      | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.            |  600 G");
        Console.WriteLine("- 5. 청동 도끼     | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.        |  1500 G");
        Console.WriteLine("- 6. 스파르타의 창  | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다. |  구매완료");

        Console.WriteLine("\n0. 나가기");

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                PurchaseItem("수련자 갑옷", "방어력", 5, "수련에 도움을 주는 갑옷입니다.", 1000);
                break;
            case "2":
            case "6":
                Console.WriteLine("이미 구매한 아이템입니다.");
                PauseAndReturn(() => ShowItemPurchaseMenu());
                break;
            case "3":
                PurchaseItem("스파르타의 갑옷", "방어력", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
                break;
            case "4":
                PurchaseItem("낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
                break;
            case "5":
                PurchaseItem("청동 도끼", "공격력", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                PauseAndReturn(() => ShowItemPurchaseMenu());
                break;
        }
    }

    static void PurchaseItem(string name, string statType, int statValue, string description, int price)
    {
        if (player.Gold >= price)
        {
            player.Gold -= price;
            inventory.Add(new Item(name, statType, statValue, description));
            Console.WriteLine($"{name}를 구매 완료했습니다.");
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
        }
        PauseAndReturn(() => ShowItemPurchaseMenu());
    }

    static void PauseAndReturn(Action returnAction = null)
    {
        Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
        Console.ReadKey();
        returnAction?.Invoke();
    }
}






