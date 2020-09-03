using System.Collections.Generic;
using ActiveDirectory.Extensions;
using Xunit;

namespace ActiveDirectoryTests.Unit
{
    public class ExtensionFixtures
    {
        [Fact]
        public void Collection_Extension_Is_Empty_Unitialized()
        {
            //Arrange
            var myList = new List<string>();

            //Act
            bool isEmpty = myList.Empty();

            //Assert
            Assert.True(isEmpty);
        }

        [Fact]
        public void Collection_Extension_Is_Empty_Initialized_Empty_String()
        {
            //Arrange
            var myList = new List<string>() { "" };

            //Act
            bool isEmpty = myList.Empty();

            //Assert
            Assert.True(isEmpty);
        }

        [Fact]
        public void Collection_Extension_Is_Not_Empty()
        {
            //Arrange
            var myList = new List<string>() { "Random Value" };

            //Act
            bool isEmpty = myList.Empty();

            //Assert
            Assert.False(isEmpty);
        }

        [Fact]
        public void Type_Extensions_Is_Enumerable_Collection()
        {
            //Arrange
            var myType = typeof(List<string>);

            //Act
            bool isIEnumerable = myType.IsIEnumerable();

            //Assert
            Assert.True(isIEnumerable);
        }

        [Fact]
        public void Type_Extensions_Is_Enumerable_Primitive()
        {
            //Arrange
            var myType = typeof(char);

            //Act
            bool isIEnumerable = myType.IsIEnumerable();

            //Assert
            Assert.False(isIEnumerable);
        }

        [Fact]
        public void Get_Any_Element_Type_Collection()
        {
            //Arrange
            var myType = typeof(List<char>);

            //Act
            var element = myType.GetAnyElementType();

            //Assert
            Assert.Equal(typeof(char), element);
        }

        [Fact]
        public void Get_Any_Element_Type_Array()
        {
            //Arrange
            var myType = typeof(int[]);

            //Act
            var element = myType.GetAnyElementType();

            //Assert
            Assert.Equal(typeof(int), element);
        }


        [Fact]
        public void Get_Any_Element_Type_Primitive()
        {
            //Arrange
            var myType = typeof(int);

            //Act
            var element = myType.GetAnyElementType();

            //Assert
            Assert.Equal(typeof(int), element);
        }
    }
}
