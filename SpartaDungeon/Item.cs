// 아이템 클래스
class Item
{
    public string Name;
    public string StatType;
    public int StatValue;
    public string Description;
    public bool IsEquipped;

    public Item(string name, string statType, int statValue, string description)
    {
        Name = name;
        StatType = statType;
        StatValue = statValue;
        Description = description;
        IsEquipped = false;
    }
}