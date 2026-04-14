using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BadgeCatalog.Tests.Domain;

public class BadgeClassTests
{
    private BadgeClass CreateValidBadgeClass()
    {
        var name = "Backend Developer";
        var description = "Awarded for backend skills";
        var image = new BadgeImage("https://example.com/image.png");
        var criteria = new BadgeCriteria("Complete backend training");

        return new BadgeClass(name, description, image, criteria);
    }

    [Fact]
    public void Should_Create_BadgeClass_When_Data_Is_Valid()
    {
        // Arrange
        var badge = CreateValidBadgeClass();

        // Assert
        badge.Id.Should().NotBeEmpty();
        badge.Name.Should().Be("Backend Developer");
        badge.Description.Should().Be("Awarded for backend skills");
        badge.IsActive.Should().BeTrue();
        badge.Version.Should().Be(1);
    }

    [Fact]
    public void Should_Throw_Exception_When_Name_Is_Empty()
    {
        // Arrange
        var description = "Awarded for backend skills";
        var image = new BadgeImage("https://example.com/image.png");
        var criteria = new BadgeCriteria("Complete backend training");

        // Act
        Action act = () => new BadgeClass("", description, image, criteria);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be empty.");
    }

    [Fact]
    public void Should_Generate_Slug_From_Name()
    {
        // Arrange
        var badge = CreateValidBadgeClass();

        // Assert
        badge.Slug.Should().Be("backend-developer");
    }

    [Fact]
    public void Should_Deactivate_BadgeClass()
    {
        // Arrange
        var badge = CreateValidBadgeClass();
        var previousVersion = badge.Version;
        var previousUpdatedAt = badge.UpdatedAt;

        // Act
        badge.Deactivate();

        // Assert
        badge.IsActive.Should().BeFalse();
        badge.Version.Should().Be(previousVersion + 1);
        badge.UpdatedAt.Should().BeAfter(previousUpdatedAt);
    }

    [Fact]
    public void Should_Active_BadgeClass()
    {
        //Arrange
        var badge = CreateValidBadgeClass();
        badge.Deactivate();

        var previousVersion = badge.Version;
        var previousUpdatedAt = badge.UpdatedAt;

        //Act
        badge.Activate();

        //Assert
        badge.IsActive.Should().BeTrue();
        badge.Version.Should().Be(previousVersion + 1);
        badge.UpdatedAt.Should().BeAfter(previousUpdatedAt);
    }

    [Fact]
    public void Should_Update_BadgeClass()
    {
        //Arrange
        var badge = CreateValidBadgeClass();
        var previousVersion = badge.Version;
        var previousUpdatedAt = badge.UpdatedAt;

        var newName = "Advanced Backend Developer";
        var newDescription = "Awarded for advanced backend skills";

        //Act
        badge.Update(newName, newDescription, "Complete advanced backend training", "https://example.com/advanced-image.png");

        //Assert
        badge.Name.Should().Be(newName);
        badge.Description.Should().Be(newDescription);
        badge.Slug.Should().Be("advanced-backend-developer");
        badge.Version.Should().Be(previousVersion + 1);
        badge.UpdatedAt.Should().BeAfter(previousUpdatedAt);

    }
}