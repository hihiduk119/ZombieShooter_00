namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 저장 및 로드에 필요한 카드 데이터.
    /// Required card data for save and load. 
    /// </summary>
    public interface ICardData
    {
        //현재 레벨
        int Level { get; }
        //남은 최대 내구도
        int Durability { get; }
        //레벨 업그레이드 연구 중이라면 남은 시간
        long RemainResearchTime { get; }
        //연구 중이었다면 해당 슬롯
        int ResearchSlot { get; }
    }
}