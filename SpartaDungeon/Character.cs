// 캐릭터 클래스
class Character
{
    public string Name = "Chad";
    public string Class = "전사";
    public int Level = 1;
    public int BaseAttack = 10;
    public int BaseDefense = 5;
    public int Health = 100;
    public int Gold = 1500;
    public List<Item> EquippedItems = new List<Item>();

    public void EquipItem(Item item)
    {
        EquippedItems.Add(item);
        item.IsEquipped = true;
    }

    public void UnequipItem(Item item)
    {
        EquippedItems.Remove(item);
        item.IsEquipped = false;
    }

    public int GetStatBonus(string statType)
    {
        int bonus = 0;
        foreach (var item in EquippedItems)
        {
            if (item.StatType == statType)
                bonus += item.StatValue;
        }
        return bonus;
    }

    public string GetFormattedStat(string statType)
    {
        int baseValue = statType == "공격력" ? BaseAttack :
                        statType == "방어력" ? BaseDefense : 0;

        int bonus = GetStatBonus(statType);

        if (bonus != 0)
            return $"{baseValue} ({(bonus > 0 ? "+" : "")}{bonus})";
        else
            return baseValue.ToString();
    }
}