using QAHackathon.Core.LoggingLogic;

namespace QAHackathon.Core.BussnessLogic
{
    public static class AssertBL
    {
        private static LoggingBL loggingBL = LoggingBL.Instance;

        public static void IsTrue(bool condition)
        {
            try
            {
                NUnit.Framework.Assert.IsTrue(condition);
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Condition is false");
            }
        }

        public static void IsFalse(bool condition)
        {
            try
            {
                NUnit.Framework.Assert.IsFalse(condition);
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Condition is true");
            }
        }

        public static void IsNotEmpty(string obj)
        {
            try
            {
                NUnit.Framework.Assert.IsNotEmpty(obj);
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Object is empty");
            }
        }

        public static void IsEmpty(string obj)
        {
            try
            {
                NUnit.Framework.Assert.IsEmpty(obj);
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Object is not empty");
            }
        }

        public static void IsNotNull(object obj)
        {
            try
            {
                NUnit.Framework.Assert.IsNotNull(obj);
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Object is null");
            }
        }

        public static void AreEqual(object expected, object actual)
        {
            try
            {
                NUnit.Framework.Assert.AreEqual(expected, actual);
                loggingBL.Info($"{expected} is equal to {actual}");
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception($"{expected} is not equal to {actual}");
            }
        }

        public static void AreNotEqual(object obj1, object obj2)
        {
            try
            {
                NUnit.Framework.Assert.AreNotEqual(obj1, obj2);

                loggingBL.Info($"{obj1} is not equal to {obj2}");
            }
            catch (Exception ex)
            {
                loggingBL.Error(ex);

                throw new Exception("Objects are equal");
            }
        }
    }
}