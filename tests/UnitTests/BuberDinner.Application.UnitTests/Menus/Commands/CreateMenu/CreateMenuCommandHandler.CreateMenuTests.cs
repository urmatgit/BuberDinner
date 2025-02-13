using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.Menus.TestUtils.Menus.Extensions;

using FluentAssertions;

using Moq;

namespace BuberDinner.Application.UnitTests.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandlerTests
    {
        private readonly CreateMenuCommandHandler _handler;
        private readonly Mock<IMenuRepository> _mockMenuRepository; 
        public CreateMenuCommandHandlerTests()
        {
            _mockMenuRepository = new Mock<IMenuRepository>();
            _handler =new  CreateMenuCommandHandler(_mockMenuRepository.Object);
        }
        //T1-SUT- logcal component we are testing
        //T2: Scenario what we are testing
        //T3: Expected outcome -what we expect the
        [Theory]
        [MemberData(nameof(ValidCreateMenuCommands))]
        public async Task HandleCreateMenuCommand_WhenMenuIsValid_ShouldCreateAndReturnMenu(CreateMenuCommand createMenuCommand)
        {
            //Arrange
          // var createMenuCommand = CreateMenuCommandUtils.CreateCommand();
            //ACt
            var resulst = await _handler.Handle(createMenuCommand, default);
            //Assert
            resulst.IsError.Should().BeFalse();
            resulst.Value.ValidateCreateFrom(createMenuCommand);
            _mockMenuRepository.Verify(m=>m.AddAsync(resulst.Value), Times.Once());
        }

        public static IEnumerable<object[]> ValidCreateMenuCommands()
        {
            //var createMenuCommand = CreateMenuCommandUtils.CreateCommand();
            yield return new[] { CreateMenuCommandUtils.CreateCommand() };
            yield return new[] { CreateMenuCommandUtils.CreateCommand(sections: CreateMenuCommandUtils.CreateMenuSectionCommand(2))};
            yield return new[] { CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateMenuSectionCommand(
                    3,
                    CreateMenuCommandUtils.CreateMenuItemCommand(3))) };
        }
    }
}
