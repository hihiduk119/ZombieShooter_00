using System;

namespace WoosanStudio.Common
{
    /// <summary>
    /// 유저 인풋 전용 인터페이스
    /// </summary>
    public interface IUserInput
    {
        float Horizontal { get; }
        float Vertical { get; }
    }
}
