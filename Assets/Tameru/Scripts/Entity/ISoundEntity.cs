namespace Tameru.Entity
{
    /// <summary>
    /// 音再生に関する関数のみ公開
    /// </summary>
    public interface ISoundEntity
    {
        void SetUpPlayBgm(BgmType type);
        void SetUpPlaySe(SeType type);
    }
}