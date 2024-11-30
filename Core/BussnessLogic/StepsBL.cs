using NLog;
using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.Core.BussnessLogic
{
    public static class StepsBL
    {
        public static T Step<T>(string stepTitle, Func<T> func) => FuncStep(stepTitle, 0, 0, func);
        public static T Step<T>(string stepTitle, int startSteps, Func<T> func) => FuncStep(stepTitle, startSteps, 0, func);
        public static T Step<T>(string stepTitle, int startSteps, int endSteps, Func<T> func) => FuncStep(stepTitle, startSteps, endSteps, func);
        public static void Step(string stepTitle, Action action) => ActionStep(stepTitle, 0, 0, action);
        public static void Step(string stepTitle, int startSteps, Action action) => ActionStep(stepTitle, startSteps, 0, action);
        public static void Step(string stepTitle, int startSteps, int endSteps, Action action) => ActionStep(stepTitle, startSteps, endSteps, action);

        private static T FuncStep<T>(string stepTitle, int startSteps, int endSteps, Func<T> func)
        {
            using (ScopeContext.PushProperty("RequestId", LoggingBL.Guid))
            {
                GetDescription(stepTitle, startSteps, endSteps);

                T result = func();

                return result;
            }
        }

        private static void ActionStep(string stepTitle, int startSteps, int endSteps, Action action)
        {
            using (ScopeContext.PushProperty("RequestId", LoggingBL.Guid))
            {
                GetDescription(stepTitle, startSteps, endSteps);
                action();
            }
        }

        private static void GetDescription(string stepTitle, int startSteps, int endSteps)
        {
            if (startSteps < 1)
            {
                ScopeContext.PushProperty("StepTitle", $"\"{stepTitle}\"");
            }
            else if (endSteps < 1)
            {
                ScopeContext.PushProperty("StepTitle", $"Step [{startSteps}] \"{stepTitle}\"");
            }
            else
            {
                ScopeContext.PushProperty("StepTitle", $"Step [{startSteps}-{endSteps}] \"{stepTitle}\"");
            }
        }
    }
}
