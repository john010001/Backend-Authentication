using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication.Application.Features.Login.Commands.Registrar;
using Test.Authentication.Application.Features.Login.Queries.IniciarSesion;
using Test.Authentication.Application.Features.Login.Queries.ObtenerUsuario;
using Test.Authentication.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Test.Authentication.Application.Features.Login.Commands.Eliminar;
using Test.Authentication.Api.Controllers;
using Test.Authentication.Application.Wrappers;
using System.Net;



namespace Test.Authentication.Api.Tests 
{
    [TestClass]
    public class LoginControllerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IMediator> _mockMediator;
        private LoginController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMediator = new Mock<IMediator>();
            _controller = new LoginController(_mockConfiguration.Object, _mockMediator.Object);
        }

        [TestMethod]
        public async Task RegistrarIsValid()
        {
            var command = new RegistrarCommand("test@example.com", "Kh123456", DateTime.Now);
            var expectedResponse = new ResultResponse<bool>(HttpStatusCode.OK, true, "Usuario registrado correctamente.");

            _mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Registrar(command);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "El resultado no es de tipo OkObjectResult.");

           
            var resultResponse = okResult.Value as ResultResponse<bool>;
            Assert.IsNotNull(resultResponse, "El valor devuelto no es del tipo esperado"); 

          
            Assert.AreEqual(HttpStatusCode.OK, resultResponse.Code);
            Assert.AreEqual("Usuario registrado correctamente.", resultResponse.Message);
            Assert.IsTrue(resultResponse.Data);
        }

        [TestMethod]
        public async Task AuthIsValid()
        {
            // Arrange
            var query = new IniciarSesionQuery("test@example.com", "password");
            var expectedResponse = new ResultResponse<IniciarSesionResponse>(
                HttpStatusCode.OK,
                new IniciarSesionResponse { JWToken = "Token de prueba", Correo = "test@example.com" },
                "Inicio de sesión exitoso." 
            );

            // Configura el mock para el Mediator
            _mockMediator.Setup(m => m.Send(query, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Auth(query);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "El resultado no es de tipo Ok Object Result");

            var resultResponse = okResult.Value as ResultResponse<IniciarSesionResponse>;
            Assert.IsNotNull(resultResponse, "El valor devuelto no es del tipo esperado");

            Assert.AreEqual(HttpStatusCode.OK, resultResponse.Code);
            Assert.AreEqual("Inicio de sesión exitoso.", resultResponse.Message);
            Assert.IsNotNull(resultResponse.Data);
            Assert.AreEqual("Token de prueba", resultResponse.Data?.JWToken);
            Assert.AreEqual("test@example.com", resultResponse.Data?.Correo);
        }


        [TestMethod]
        public async Task ObtenerUsuariosIsValid()
        {
            // Arrange
            var usuariosEsperados = new List<Usuario>
            {
                new Usuario { Correo = "usuario1@example.com", Password = "Password1", FechaRegistro = DateTime.Now },
                new Usuario { Correo = "usuario2@example.com", Password = "Password2", FechaRegistro = DateTime.Now }
            };

            // Simula el comportamiento del Mediator para devolver la lista de usuarios esperada
            _mockMediator.Setup(m => m.Send(It.IsAny<ObtenerUsuariosQuery>(), default)).ReturnsAsync(usuariosEsperados);

            var result = await _controller.ObtenerUsuarios();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "El resultado no es de tipo OkObjectResult.");

            var usuariosResult = okResult.Value as IEnumerable<Usuario>;
            Assert.IsNotNull(usuariosResult, "El valor devuelto no es del tipo esperado");

            Assert.AreEqual(usuariosEsperados.Count, usuariosResult.Count(), "El número de usuarios devueltos no es correcto");

            _mockMediator.Verify(m => m.Send(It.IsAny<ObtenerUsuariosQuery>(), default), Times.Once);
        }

        [TestMethod]
        public async Task Eliminar_IsValid()
        {
            // Arrange
            var command = new EliminarUsuarioCommand("test@example.com");
            var expectedResponse = new ResultResponse<bool>(HttpStatusCode.OK, true, "Usuario eliminado correctamente"); 

            // Configura el mock para aceptar cualquier instancia de EliminarUsuarioCommand
            _mockMediator.Setup(m => m.Send(It.IsAny<EliminarUsuarioCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Eliminar(command);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "El resultado no es de tipo OkObjectResult.");

            var resultResponse = okResult.Value as ResultResponse<bool>;
            Assert.IsNotNull(resultResponse, "El valor devuelto no es del tipo esperado");

            Assert.AreEqual(HttpStatusCode.OK, resultResponse.Code);
            Assert.AreEqual("Usuario eliminado correctamente", resultResponse.Message); 
            Assert.IsTrue(resultResponse.Data);

            // Verificar que el Mediator fue llamado
            _mockMediator.Verify(m => m.Send(It.IsAny<EliminarUsuarioCommand>(), default), Times.Once);
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(true);
        }
    }
}
