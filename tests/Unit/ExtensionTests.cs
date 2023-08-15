using System.Collections.Generic;
using ActiveDirectory.Extensions;
using Xunit;

namespace ActiveDirectoryTests.Unit;

public class ExtensionTests
{
    [Fact]
    public void Collection_extension_is_empty_unitialized()
    {
        //Arrange
        var myList = new List<string>();

        //Act
        bool isEmpty = myList.Empty();

        //Assert
        Assert.True(isEmpty);
    }

    [Fact]
    public void Collection_extension_is_empty_initialized_empty_string()
    {
        //Arrange
        var myList = new List<string>() { string.Empty };

        //Act
        bool isEmpty = myList.Empty();

        //Assert
        Assert.True(isEmpty);
    }

    [Fact]
    public void Collection_extension_is_not_empty()
    {
        //Arrange
        var myList = new List<string>() { "Random Value" };

        //Act
        bool isEmpty = myList.Empty();

        //Assert
        Assert.False(isEmpty);
    }

    [Fact]
    public void Type_extensions_is_enumerable_collection()
    {
        //Arrange
        var myType = typeof(List<string>);

        //Act
        bool isIEnumerable = myType.IsIEnumerable();

        //Assert
        Assert.True(isIEnumerable);
    }

    [Fact]
    public void Type_extensions_is_enumerable_primitive()
    {
        //Arrange
        var myType = typeof(char);

        //Act
        bool isIEnumerable = myType.IsIEnumerable();

        //Assert
        Assert.False(isIEnumerable);
    }

    [Fact]
    public void Get_any_element_type_collection()
    {
        //Arrange
        var myType = typeof(List<char>);

        //Act
        var element = myType.GetAnyElementType();

        //Assert
        Assert.Equal(typeof(char), element);
    }

    [Fact]
    public void Get_any_element_type_array()
    {
        //Arrange
        var myType = typeof(int[]);

        //Act
        var element = myType.GetAnyElementType();

        //Assert
        Assert.Equal(typeof(int), element);
    }

    [Fact]
    public void Get_any_element_type_primitive()
    {
        //Arrange
        var myType = typeof(int);

        //Act
        var element = myType.GetAnyElementType();

        //Assert
        Assert.Equal(typeof(int), element);
    }
}
