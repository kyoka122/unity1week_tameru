namespace Tameru
{
    public enum SceneName
    {
        None,
        Top,
        Main,
        Ranking,
    }

    public enum SeType
    {
        None,
        Decision,
        Cancel,
        CursorOver,
        Damage,
        HitMagic,
        Charged, // チャージが一段階変化した時
        UseSmallBall,
        UseMediumBall,
        UseLargeBall,
        UseSmallBullet,
        UseMediumBullet,
        UseLargeBullet,
    }

    public enum BgmType
    {
        None,
        Top,
        Main,
        Result,
    }

    public enum GameState
    {
        None,
        Ready,
        Main,
        Over,
        Clear,
    }

    public enum EnemyType
    {
        None,
        Sword,
        Shield,
        GreatSword,
    }
}