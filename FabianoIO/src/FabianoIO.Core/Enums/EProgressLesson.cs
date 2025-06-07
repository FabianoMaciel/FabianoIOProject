using System.ComponentModel;
using System.Reflection;

namespace FabianoIO.Core.Enums
{
    public enum EProgressLesson
    {
        [Description("Não iniciado")]
        NotStarted = 0,
        [Description("Em progresso")]
        InProgress = 1,
        [Description("Concluído")]
        Completed = 2
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
    }
}
