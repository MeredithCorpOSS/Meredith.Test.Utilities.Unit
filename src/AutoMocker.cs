using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;

namespace Meredith.Test.Utilities.Unit
{
    public class AutoMocker<T>
        where T : class
    {
        private T _theTarget;
        private readonly Dictionary<Type, Mock> _listOfMocks = new Dictionary<Type, Mock>();

        public Mock<TMocked> The<TMocked>()
            where TMocked : class
        {
            var typeName = typeof(TMocked);

            if (!_listOfMocks.TryGetValue(typeName, out var result))
            {
                var mockType = BuildMock<TMocked>();
                result = (Mock)Activator.CreateInstance(mockType);
                _listOfMocks.Add(typeName, result);
            }
            return (Mock<TMocked>)result;
        }

        public T Target => _theTarget ?? (_theTarget = BuildTarget());

        public void Clear()
        {
            _theTarget = null;
            _listOfMocks.Clear();
        }

        public void VerifyAll()
        {
            foreach (var mock in _listOfMocks.Values)
            {
                mock.VerifyAll();
            }
        }

        private T BuildTarget()
        {
            var constructorInfo = GetContructorWithLongestParameterList();
            FillInListOfMocksWithConstructorParams(constructorInfo);

            var parameterList = constructorInfo
                .GetParameters()
                .Join(_listOfMocks, parameterInfo => parameterInfo.ParameterType, mockDictionaryItem => mockDictionaryItem.Key, (parameterInfo, mockDictionaryItem) => mockDictionaryItem.Value.Object)
                .ToArray();
            var target = constructorInfo.Invoke(parameterList) as T;

            return target;
        }

        private void FillInListOfMocksWithConstructorParams(ConstructorInfo constructorInfo)
        {
            foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
            {
                var typeName = parameterInfo.ParameterType;
                if (_listOfMocks.ContainsKey(typeName))
                {
                    continue;
                }

                var parameterType = parameterInfo.ParameterType;
                var mockType = BuildMock(parameterType);
                _listOfMocks.Add(typeName, (Mock)Activator.CreateInstance(mockType));
            }
        }

        private static Type BuildMock<TMocked>()
        {
            return BuildMock(typeof(TMocked));
        }

        private static Type BuildMock(Type typeToMock)
        {
            return typeof(Mock<>).MakeGenericType(typeToMock);
        }

        private static ConstructorInfo GetContructorWithLongestParameterList()
        {
            var type = typeof(T);
            var constructorInfos = type.GetConstructors(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var sortedConstructors = constructorInfos
                .Select(info => new { ctor = info, length = info.GetParameters().Length })
                .OrderByDescending(arg => arg.length);

            var contructorWithLongestParameterList = sortedConstructors.First().ctor;
            var distinctParameters = contructorWithLongestParameterList.GetParameters()
                .Select(param => param.ParameterType)
                .Distinct();

            if (distinctParameters.Count() != contructorWithLongestParameterList.GetParameters().Length)
            {
                throw new ArgumentException("You cannot have more than one parameter of the same type in your constructor (feel free to implement this capability)");
            }

            return contructorWithLongestParameterList;
        }
    }
}